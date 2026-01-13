using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private WeaponManager M_weapon = new WeaponManager();

    GetchSystem System;

    public WeaponManager WeaponManager => M_weapon;

    private void Awake()
    {
        M_weapon.Init(this);
        System = new GetchSystem();
    
    }


}
