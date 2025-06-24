using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public abstract class EventChannelSO<T> : ScriptableObject
{
    // C# ��������Ʈ (�ڵ忡�� ����/�߻���)
    private event UnityAction<T> onEventTriggered;

    // �̺�Ʈ�� �����ϴ� �޼���
    public void AddListener(UnityAction<T> action)
    {
        onEventTriggered += action;
    }

    // �̺�Ʈ ������ �����ϴ� �޼���
    public void RemoveListener(UnityAction<T> action)
    {
        onEventTriggered -= action;
    }

    // �̺�Ʈ�� �߻���Ű�� �޼���
    public void TriggerEvent(T value)
    {
        // C# ��������Ʈ�� ���� �̺�Ʈ�� �߻���ŵ�ϴ�.
        onEventTriggered?.Invoke(value);
    }
}