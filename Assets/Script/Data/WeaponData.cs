using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{ 
    Sword,
    Axe,

}

public enum ObjectRank
{ 
    Normal,
    Rare,
    Epic,
    Unique,
    Legendary

}

[CreateAssetMenu(fileName = "Data" , menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private WeaponType m_WeaponType;
    [SerializeField]
    private ObjectRank m_WeaponRank;
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;
}


//펫 나중에 생성