using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private WeaponManager M_weapon;

    GetchSystem System_Getch;

    public GetchSystem GetchSystem => System_Getch;

    public WeaponManager WeaponManager => M_weapon;

    protected override void Awake()
    {
        base.Awake();        
        M_weapon.Init(this);
        System_Getch = new GetchSystem();
    }



}
