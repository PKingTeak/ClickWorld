using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase
{
    private Dictionary<ObjectRank, List<WeaponData>> weaponDic = new Dictionary<ObjectRank, List<WeaponData>>();

    [SerializeField] private List<WeaponData> allWeapons;
    public WeaponDataBase()
    {
     
    
    }

    public void InitData()
    {
        if (allWeapons == null)
        {
            Debug.Log("[WeaponDataBase] 아무것도 들어오지 않았습니다. ");
            return;
        }

        foreach (var weapon in allWeapons)
        {
            ObjectRank rank = weapon.ItemRank;

            if (!weaponDic.ContainsKey(rank))
            {
                weaponDic[rank] = new List<WeaponData>();
            }
            weaponDic[rank].Add(weapon);

        }
    }


    public WeaponData GetRandomWeaponByRank(ObjectRank rank)
    {
        if (weaponDic.ContainsKey(rank) && weaponDic[rank].Count > 0)
        {
            int randomIndex = Random.Range(0, weaponDic[rank].Count);
            return weaponDic[rank][randomIndex];
        
        }

        Debug.Log("[WeaponManager]해당 무기가 존재 하지 않습니다.");
        return null;
        
    }


    public void SettingWeaponData(List<WeaponData> list)
    {
        if (list.Count <= 0)
        {
            return;
        }
        allWeapons = list;
        //테스트용 로더가 생기면 없어질 메서드
    }


}
