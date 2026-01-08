using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    GameManager gameManger;
    
    
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
    


    }

}
