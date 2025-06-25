using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.UIElements;

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
}
