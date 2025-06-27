using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFloating : MonoBehaviour
{
    private Animator Anim;
    [SerializeField] private TextMeshPro DamageText;
    [SerializeField] private Material enemyMt;
    [SerializeField] private Material normalMt;
    [SerializeField] private Material criticalMt;
    [SerializeField] private Material strikeMt;

    private void OnEnable()
    {
        this.Anim = this.GetComponent<Animator>();
        Anim.Play("DamageText");
    }

    // ������ �� ������ Ÿ�Կ� ���� �ؽ�Ʈ ����
    public void SetDamage(AttackInfo attackInfo)
    {
        switch (attackInfo.AttackType)
        {
            case EAttackType.Enemy:
                DamageText.fontSharedMaterial = enemyMt;
                break;
            case EAttackType.Normal:
                DamageText.fontSharedMaterial = normalMt;
                break;
            case EAttackType.Critical:
                DamageText.fontSharedMaterial = criticalMt;
                break;
        }

        if (attackInfo.IsMiss == true)
            DamageText.text = $"Miss";
        else
            DamageText.text = $"{attackInfo.Damage.ToABC()}";
    }

    // �ִϸ��̼� ������ �κп��� ȣ��
    // �÷��� �ִϸ��̼� ������ Ǯ���� ����
    // �ش� ��ũ��Ʈ�� ȣ�� ��ġ�� �������� �߰��̹Ƿ� �θ��� ��ġ�� �����ϵ��� �Ѵ�.
    public void AnimtionEnd()
    {
        FXPoolManager.Instance.Push(gameObject, EFXPoolType.DamageText);
    }
}
