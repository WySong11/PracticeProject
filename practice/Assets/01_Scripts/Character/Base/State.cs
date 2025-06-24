using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> where T : CharacterBase
{
    // �ش� ���¸� ������ �� 1ȸ ȣ��
    public virtual void Enter(T entity)
    {
    }

    // �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
    public virtual void Execute(T entity)
    {
    }

    // �ش� ���¸� ������Ʈ�� �� ������� �� ȣ��
    public virtual void FixedExecute(T entity)
    {
    }

    // �ش� ���¸� ������ �� 1ȸ ȣ��
    public virtual void Exit(T entity)
    {
    }
}
