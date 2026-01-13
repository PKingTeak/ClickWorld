using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    GameManager gameManger;

    private WeaponDataBase dataTable;

    public WeaponDataBase WeaponTable { get { return dataTable; } }
    
    
    public void Init(GameManager gm)
    {
        gameManger = gm;
        dataTable = new WeaponDataBase();
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
