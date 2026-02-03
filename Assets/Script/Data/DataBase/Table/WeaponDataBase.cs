using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase
{
    private Dictionary<ObjectRank, List<IItemData>> weaponDic = new Dictionary<ObjectRank, List<IItemData>>();

    public WeaponDataBase()
    {
     
    
    }

    public void InitData(List<IItemData> list)
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
                weaponDic[rank] = new List<IItemData>();
            }
            weaponDic[rank].Add(weapon);

        }
    }


    public IItemData GetRandomWeaponByRank(ObjectRank rank)
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
