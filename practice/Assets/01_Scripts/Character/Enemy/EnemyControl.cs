using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public class EnemyInfo
{
    public EEnemyType EnemyType;
    public BigNum AttackPower = 0;
    public BigNum MaxHp = 1;
    public BigNum DropGold = 0;
    public int Stage;
}

public class EnemyControl : CharacterBase
{
    [SerializeField] private AudioPlayerSingle DieSound;
    [SerializeField] private AudioPlayerSingle HitSound;
    [SerializeField] public AudioPlayerSingle AttackSound_Boss;
    [SerializeField] public AudioPlayerSingle AttackSound_Monster;


    public EnemyInfo Info = new EnemyInfo();
    public Vector3 StayTarget = Vector3.zero;

    private State<EnemyControl>[] States;
    private State<EnemyControl> CurrentState;
    private CapsuleCollider MonsterCollider;

    private PlayerData Player => DataManager.GetPlayerData();
    private InitialData Inital => DataManager.GetInitialData();

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected new void Awake()
    {
        base.Awake();
        MonsterCollider = this.GetComponent<CapsuleCollider>();
        Setup();
    }
    private void Setup()
    {
        States = new State<EnemyControl>[6];

        States[(int)EActType.Init] = new EnemyInit();
        States[(int)EActType.Idle] = new EnemyIdle();
        States[(int)EActType.Attack] = new EnemyAttack();
        States[(int)EActType.Skill] = new EnemySkill();
        States[(int)EActType.Die] = new EnemyDie();
        States[(int)EActType.Move] = new EnemyMove();

        isEnemy = true;
    }

    public void Init()
    {

    }


    //����� �÷��̾� Ž��
    public CharacterBase NearPlayer()
    {
        if (InGameHeroManager.IsHeroActive())
        {
            return InGameHeroManager.FindNearTarget(this.CenterPoint.position);
        }
        else
            return null;
    }

    public new void TakeHit(AttackInfo HitInfo)
    {
        // ���� �Ǵ� �ǰݰ��� �Ǵ� ���� üũ
        if (State.Invincible == true || State.Hitable == false || State.IsLive == false)
            return;

        base.TakeHit(HitInfo);

        HitSound.Play();

        if (HitInfo.HitCount == 1)
        {
            State.CurrentHp -= HitInfo.Damage;
            FXPoolManager.Instance.PopDamageText(this.transform.position + new Vector3(0, 1f, 0), HitInfo);
            FXPoolManager.Instance.Pop(HitInfo.EffectType, new Vector3(this.transform.position.x, this.transform.position.y + 2f, 0));

            UpdateHp();

            if (State.CurrentHp <= 0) { Die(); } // ��� ó��
        }
        else
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(MultiHit(HitInfo));
            }
        }
    }


    // Ÿ�� ����
    private IEnumerator MultiHit(AttackInfo HitInfo)
    {
        for (int i = 0; i < HitInfo.HitCount; i++)
        {
            if (State.IsLive == false) { yield break; } //Ÿ���߿� �ٸ�Ÿ������ ����� �ߴ�

            State.CurrentHp -= HitInfo.Damage;
            FXPoolManager.Instance.PopDamageText(this.transform.position + new Vector3(0, 0.5f + (i * 0.5f), 0), HitInfo);
            FXPoolManager.Instance.Pop(HitInfo.EffectType, new Vector3(this.transform.position.x, this.transform.position.y + 2f, 0));

            UpdateHp();

            if (State.CurrentHp <= 0) { Die(); yield break; } // ��� ó��

            yield return new WaitForSeconds(0.1f);
        }
    }

    // ���� ����
    public void ChangeState(EActType NewState)
    {
        // �ٲٷ��� ���°� ����ִ� ���
        if (States[(int)NewState] == null)
            return;

        // ���� ������� ���°� �����ϸ� ���� ���� ����
        if (CurrentState != null)
        {
            CurrentState.Exit(this);
        }

        // ���ο� ���·� �����ϰ�, ���� �ٲ� ������ Enter() �޼ҵ� ȣ��
        CurrentState = States[(int)NewState];
        CurrentState.Enter(this);
    }

    public void StopAttackWhenMainHeroDie()
    {
        Target = null;
        ChangeState(EActType.Idle);
    }

    protected override void HandleEvent(string eventName)
    {
    }

    protected override void Finish(EActType ActType)
    {

    }

    private void LateUpdate()
    {
        base.Update();

        if (CurrentState != null)
        {
            CurrentState.Execute(this);
        }

        // ����(������)
        if (State.NoneMove == true)
        {
            return;
            /*
            if (CurrentState == States[(int)EActType.Move] || CurrentState == States[(int)EActType.Stay] || CurrentState == States[(int)EActType.Attack])
                ChangeState(EActType.Idle);

            return;
            */
        }
    }


    public override void Die()
    {
        base.Die();
        // ����ִ� ���� ó��(�Ϲݰ���/��Ƽ���� �ߺ� Die üũ ����)
        if (State.IsLive == false) return;

        // �ǰ� ����Ʈ
        //FXPoolManager.Instance.Pop(EFXPoolType.DestroyEnemy, new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0));


        DieSound.Play();
        ChangeState(EActType.Die);
        MonsterCollider.enabled = false;
        State.IsLive = false;
        EnemyManager.Instance.EnemyDeath(this);

        // 1. ��尪�� ���⼭ �����ϵ���(����������)
        // 2. ����� ����(�������ʹ� �����������Ϳ� ������� �ٸ�)
    }

    // ��� �ִϸ��̼��� ���� �ð� �ʿ�
    public void DieDisable()
    {
        EnemyPoolManager.Instance.Push(gameObject, EPoolType.Enemy);
    }
 private void RangeAttack()
    {
        if (Target != null)
        {
            //var MonsterRangeAttack = FXPoolManager.Instance.Pop(EFXPoolType.MonsterRangeAttack, CenterPoint.position);
            //MonsterRangeAttack.GetComponent<MonsterRangeAttackObject>().Init(this, Target);
        }
    }
}
