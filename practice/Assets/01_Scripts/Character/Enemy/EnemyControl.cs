using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections;

public class EnemyControl : CharacterBase
{
    public EEnemyType EnemyType;

    private State<EnemyControl>[] States;
    private State<EnemyControl> CurrentState;

    public void InitEnemy()
	{

	}

    // State ����
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

    public new void TakeHit(AttackInfo HitInfo)
    {
        // ���� �Ǵ� �ǰݰ��� �Ǵ� ���� üũ
        if (State.Invincible == true || State.Hitable == false || State.IsLive == false)
            return;

        base.TakeHit(HitInfo);

        if (HitInfo.HitCount == 1)
        {
            State.CurrentHp -= HitInfo.Damage;
            FXPoolManager.Instance.PopDamageText(this.transform.position + new Vector3(0, 1f, 0), HitInfo);
            FXPoolManager.Instance.Pop(HitInfo.EffectType, new Vector3(this.transform.position.x, this.transform.position.y + 2f, 0));

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

            if (State.CurrentHp <= 0) { Die(); yield break; } // ��� ó��

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void HandleEvent(string eventName)
    {

    }

    // ��� ��
    protected override void Finish(EActType ActType)
    {

    }
}
