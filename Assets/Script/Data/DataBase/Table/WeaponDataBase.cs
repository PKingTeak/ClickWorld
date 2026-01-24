using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase
{
    private Dictionary<ObjectRank, List<WeaponData>> weaponDic = new Dictionary<ObjectRank, List<WeaponData>>();

    public WeaponDataBase()
    {
     
    
    }

    public void InitData(List<WeaponData> list)
    {
        if (list == null)
        {
            Debug.Log("[WeaponDataBase] 아무것도 들어오지 않았습니다. ");
            return;
        }

        foreach (var weapon in list)
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


    

}
