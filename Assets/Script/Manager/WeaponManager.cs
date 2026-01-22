using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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
        dataTable = new WeaponDataBase();//생성과 동시에 Init()하니까 어쩌피
        dataTable.SettingWeaponData(TestTable);
        dataTable.InitData();
        

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
