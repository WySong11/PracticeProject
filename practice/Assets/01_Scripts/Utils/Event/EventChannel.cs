using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public abstract class EventChannelSO<T> : ScriptableObject
{
    [HideInInspector] public UnityEvent<T> OnEventRaisedInspector;

    // C# ��������Ʈ (�ڵ忡�� ����/�߻���)
    private event UnityAction<T> onEventRaised;

    // �̺�Ʈ�� �����ϴ� �޼���
    public void AddListener(UnityAction<T> action)
    {
        onEventRaised += action;
    }

    // �̺�Ʈ ������ �����ϴ� �޼���
    public void RemoveListener(UnityAction<T> action)
    {
        onEventRaised -= action;
    }

    // �̺�Ʈ�� �߻���Ű�� �޼���
    public void TriggerEvent(T value)
    {
        // C# ��������Ʈ�� ���� �̺�Ʈ�� �߻���ŵ�ϴ�.
        onEventRaised?.Invoke(value);

        // UnityEvent�� �Բ� �߻����� �ν����� ����� �Լ��� ȣ��ǵ��� �մϴ�.
        // �ʿ信 ���� �� �� �ϳ��� ����ϰų�, UnityEvent ��� C# �̺�Ʈ�� ����ȭ �Ұ����� �ʵ�� ����ϰ�
        // �ν����Ϳ��� �������� �ʴ� ������� ������ ���� �ֽ��ϴ�.
        OnEventRaisedInspector?.Invoke(value);

        Debug.Log($"Event raised: {this.name} with value: {value}");
    }
}

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : EventChannelSO<int>
{
    // �߰����� �����̳� �ʵ带 �ʿ信 ���� ���⿡ ������ �� �ֽ��ϴ�.
}


[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannelSO : EventChannelSO<string>
{
    // �߰����� �����̳� �ʵ带 �ʿ信 ���� ���⿡ ������ �� �ֽ��ϴ�.
}

[CreateAssetMenu(menuName = "Events/Dictionary Event Channel")]
public class DictionaryEventChannelSO : EventChannelSO<Dictionary<string, object>>
{
    // �߰����� �����̳� �ʵ带 �ʿ信 ���� ���⿡ ������ �� �ֽ��ϴ�.
}


[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    // C# ��������Ʈ
    private event UnityAction onEventRaised;

    // �̺�Ʈ�� �����ϴ� �޼���
    public void AddListener(UnityAction action)
    {
        onEventRaised += action;
    }

    // �̺�Ʈ ������ �����ϴ� �޼���
    public void RemoveListener(UnityAction action)
    {
        onEventRaised -= action;
    }

    // �̺�Ʈ�� �߻���Ű�� �޼���
    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
        Debug.Log($"Event raised: {this.name}");
    }
}