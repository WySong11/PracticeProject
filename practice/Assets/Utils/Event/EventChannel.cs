using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public abstract class EventChannelSO<T> : ScriptableObject
{
    // �ν����Ϳ��� �����ʸ� ����ϱ� ���� UnityEvent
    public UnityEvent<T> onEventRaised;

    // �ڵ� ������� �����ʸ� ����ϱ� ���� C# Action (���� ���)
    private UnityAction<T> _onEventRaised;

    public void AddListener(UnityAction<T> action)
    {
        _onEventRaised += action;
    }

    public void RemoveListener(UnityAction<T> action)
    {
        _onEventRaised -= action;
    }

    public void RaiseEvent(T value)
    {
        // 1. �ν����Ϳ� ����� �����ʵ� ȣ��
        onEventRaised?.Invoke(value);

        // 2. �ڵ�� ������ �����ʵ� ȣ��
        _onEventRaised?.Invoke(value);
    }
}