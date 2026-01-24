using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/*
 * 일단 테이블 데이터를 결국에 넣어서 가져와야 한다. Json에서 데이터 매니저를 생성해서 
 * 모든 데이터를 해당 매니저를 통해서 가져올 수 있도록 해야할듯 함. 
 */

public class WeaponManager : MonoBehaviour
{
    private WeaponManager instance;
    public WeaponManager Instance 
    {
        get
        { 
            if (instance == null)
            {
                instance = new WeaponManager();
            }
            return instance;
        }
    }



    GameManager gameManger;

    private WeaponDataBase dataTable;
    [SerializeField]
    private List<WeaponData> TestTable = new();

    public WeaponDataBase WeaponTable { get { return dataTable; } }
    
    
    public void Init(GameManager gm)
    {
        gameManger = gm;
    }
     

    public void RegisterWeapon(WeaponData data)
    {
        if (data == null)
        {
            Debug.Log("[WeaponManager] 데이터가 없습니다.");
            return;
        }

        EventBus.PublishItem(data);

    }


}
