using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{ 
    Sword,
    Axe,
    TwoHandsSword

}



public class WeaponData :  IItemData
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private string m_WeaponInfo;
    [SerializeField]
    private ItemCategory m_ItemType;
    [SerializeField]
    private WeaponType m_WeaponType;
    [SerializeField]
    private ObjectRank m_WeaponRank;
    [SerializeField]
    private Sprite m_WeaponSprite;


    public string ItemName => m_WeaponName;
    public string ItemInfo => m_WeaponInfo;
    public ItemCategory ItemType => m_ItemType;

    public WeaponType WeaponType => m_WeaponType;
    public ObjectRank ItemRank => m_WeaponRank;
    public Sprite ItemSprite => m_WeaponSprite;

  


    public void Init(string name, string info, int weaponType, ObjectRank rank, Sprite sprite)
    {
        m_WeaponName = name;
        m_WeaponInfo = info;
        m_ItemType = ItemCategory.WeaponItem; //어쩌피 무기니까
        m_WeaponType = (WeaponType)weaponType;
        m_WeaponRank = rank;
        m_WeaponSprite = sprite;
    
    }
}




//펫 나중에 생성