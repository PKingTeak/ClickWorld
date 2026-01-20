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
public class WeaponData : ScriptableObject , IItemData
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private string m_WeaponInfo;
    [SerializeField]
    private WeaponType m_WeaponType;
    [SerializeField]
    private ObjectRank m_WeaponRank;
    [SerializeField]
    private Sprite m_WeaponSprite;
    public string ItemName => m_WeaponName;
    public string ItemInfo => m_WeaponInfo;
    public ObjectRank ItemRank => m_WeaponRank;
    public Sprite ItemSprite => m_WeaponSprite;

    public void Init(string name, string info, WeaponType type, ObjectRank rank, Sprite sprite)
    {
        m_WeaponName = name;
        m_WeaponInfo = info;
        m_WeaponType = type;
        m_WeaponRank = rank;
        m_WeaponSprite = sprite;
    }
}




//펫 나중에 생성