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

[CreateAssetMenu(fileName = "SOData" , menuName = "SOData/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private WeaponType m_WeaponType;
    [SerializeField]
    private ObjectRank m_WeaponRank;
    [SerializeField]
    private Sprite m_WeaponSprite;

    public string WeaponName => m_WeaponName;
    public ObjectRank WeaponRank => m_WeaponRank;
    public Sprite WeaponSprite => m_WeaponSprite;
}


//펫 나중에 생성