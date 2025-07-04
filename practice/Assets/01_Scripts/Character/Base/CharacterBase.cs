using System;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class CharacterState
{
    //public EActType CurrentAct;
    public BigNum MaxHp;
    public BigNum CurrentHp;

    public bool IsLive = true;


    // ���� �ð�
    public bool Invincible = true;
    public float InvincibleTimer = 0;
    public float InvincibleTime = 2;

    // �ǰ� ��
    public bool Hitable = true;
    public float HitTermTimer = 0;
    public float HitTermTime = 0.5f;

    // ���� ��
    public float AttackTermTimer = 0;
    public float AttackTermTime = 0.5f;

    // ���� �� �ð�
    public float InitTimer = 0;
    public float InitTime = 1f;
    public bool IsInitialized = false;

    // ��� �ð�
    public float DieTimer = 0;
    public float DieTime = 1f;           // ��� ����Ʈ ��� �ð�
    //public float RebirthTime = 3f;

    //��ų ��� �� ���ð�
    public bool IsHaveSkill = false;
    public float SkillTermTimer = 0f;
    public float SkillTermTime = 3f;

    public float Range = 4f;
    public float Speed = 10f;
    public Vector3 Scale;
    public bool NoneAttack;     // ���� ����
    public bool NoneMove;       // Idle���� ����

    public string CurrActName; // ���� �׼� �̸�
}

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] public CharacterBase Target;
    [SerializeField] protected Collider AttackCollider;
    public Transform CenterPoint;

    public CharacterState State = new CharacterState();
    protected bool isEnemy;

    protected void Awake()
    {

    }

    protected void Update()
    {
        State.AttackTermTimer += Time.deltaTime;
        //State.AttackTimer += Time.deltaTime;
        State.HitTermTimer += Time.deltaTime;
        State.InvincibleTimer += Time.deltaTime;
        State.DieTimer += Time.deltaTime;
        if (!State.IsInitialized)
            State.InitTimer += Time.deltaTime;
        //State.TurnTimer += Time.deltaTime;
        //State.SkillWaitTimer += Time.deltaTime;

        SetTimer();
    }

    // ���� Ÿ�̸� ���
    private void SetTimer()
    {
        if (State.HitTermTimer >= State.HitTermTime)
            State.Hitable = true;
        else
            State.Hitable = false;

        if (State.InvincibleTimer >= State.InvincibleTime)
            State.Invincible = false;
        else
            State.Invincible = true;
    }
	public virtual void TakeHit(AttackInfo HitInfo)
	{

	}
	public virtual void Die()
    {
        
    }
    //protected virtual void KnockBack(float knockBackForce, EAttackerType attackerType, int heroIndex) { }

    // Ÿ�� ���� �ٶ󺸱�

    public void LookAtTarget(Vector3 TargetPos)
    {
        //Debug.Log($"{TargetPos}");

        // ���� ��ġ���� ��ǥ ��ġ�� ���ϴ� ���� ���� ���
        Vector3 directionToTarget = (TargetPos - transform.position).normalized;

        // ���� ȸ���� �����ϰ� ���� ���⸸ ��� (y ���� 0����)
        directionToTarget.y = 0;

        // ���� ���Ͱ� ��ȿ�� ��쿡�� ȸ�� ����
        if (directionToTarget != Vector3.zero)
        {
            // ��ǥ ȸ�� ���� ��� (���ȿ��� ������ ��ȯ)
            var _targetRotation = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;

            // �ε巯�� ȸ�� ����

            // ĳ���� ȸ�� ����
            transform.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);
        }
    }


    // ü�� �ʱ�ȭ
    protected virtual void InitHP(float maxHp)
    { 
        State.MaxHp = maxHp;
        State.CurrentHp = maxHp;
    }

    // ü�¹� ����
    protected void UpdateHp()
    {
        if (isEnemy)
            return;
        //if (State.CurrentHp >= State.MaxHp)
        //{
        //    HP_HUD_Fill.SetProgress(1f);
        //    HP_HUD_Fill_After.SetProgress(1f);
        //    HP_HUD_Fill_After.StopAfterSlide();
        //}
        //else if (State.CurrentHp <= 0 || State.CurrentHp * 1000000000 < State.MaxHp)
        //{
        //    HP_HUD_Fill.SetProgress(0f);
        //    HP_HUD_Fill_After.SetProgress_After(0f);
        //}
        //else
        //{
        //    float ratio = (float)(double)(State.CurrentHp / State.MaxHp);
        //    HP_HUD_Fill.SetProgress(ratio);
        //    HP_HUD_Fill_After.SetProgress_After(ratio);
        //}
    }
}
