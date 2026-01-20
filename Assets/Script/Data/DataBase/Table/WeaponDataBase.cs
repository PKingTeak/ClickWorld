using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : MonoBehaviour
{
    private Dictionary<ObjectRank, List<WeaponData>> weaponDic = new Dictionary<ObjectRank, List<WeaponData>>();

    [SerializeField] private List<WeaponData> allWeapons;

    private void Awake()
    {
        InitData();
    }


    private void InitData()
    {

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





}
