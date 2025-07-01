using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : BasicSingleton<DataManager>
{
    private PlayerData _playerData = new();
    private InitialData _initialData = new();
    private Dictionary<int, HeroData> heroDict = new Dictionary<int, HeroData>();

    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
        // ���� ������ �ε� �� Dictionary�� ����
        LoadAllHeroData();
    }

    private void LoadAllHeroData()
    {
        // �� �޼ҵ�� Awake���� ȣ��Ǿ� ���� �����͸� �ʱ�ȭ�մϴ�.
        foreach (var hero in DataTable.����.����List)
        {
            heroDict[hero.����_id] = new HeroData(hero);
        }
    }

    public static PlayerData GetPlayerData()
    {
        return Instance._playerData;
    }

    public static InitialData GetInitialData()
    {
        return Instance._initialData;
    }
    public static HeroData GetHeroData(int id)
    {
        if (Instance.heroDict.TryGetValue(id, out HeroData heroData))
        {
            return heroData;
        }
        else
        {
            Debug.LogError($"Hero with ID {id} not found.");
            return null;
        }
    }
    // Uncomment the following lines to debug the data loading

    //foreach (var value in DefaultTable.Data.DataList)
    //       {
    //           Debug.Log(value.index + "," + value.intValue + "," + value.strValue);
    //       }
    //var dataFromMap = DefaultTable.Data.DataMap[0];
    //Debug.Log("dataFromMap : " + dataFromMap.index + ", " + dataFromMap.intValue + "," + dataFromMap.strValue);

}
