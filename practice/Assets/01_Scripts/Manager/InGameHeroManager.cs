using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHeroManager : MonoBehaviour
{
    [SerializeField] private Transform CenterPosition;
    public List<HeroControl> HeroList;

    public HeroControl FindNearTarget(Vector3 pos)
    {
        HeroControl select = HeroList[0]; //���� �����
        var distance = Vector2.Distance(pos, HeroList[0].CenterPoint.position);

        HeroControl compare;
        int count = HeroList.Count;
        for (int i = 1; i < count; i++)
        {
            compare = HeroList[i];
            if (compare.IsHeroActive()) //����ִ� ��츸 ��ġ ����
            {
                var HeroDistance = Vector2.Distance(pos, compare.CenterPoint.position);
                if (HeroDistance < distance)
                {
                    distance = HeroDistance;
                    select = compare;
                }
            }
        }
        return select;
    }

    //��ȯó��
    private void SummonHero()
    {
       
    }
}
