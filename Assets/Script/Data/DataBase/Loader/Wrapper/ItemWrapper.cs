using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;


public class ItemWrapper
{
    public string ItemName;
    public string ItemInfo;
    public ItemCategory ItemCategory;
    public ObjectRank ItemRank;
    public string SpritePath;

    public int DetailType;
    
}

public class BoxWrpper
{
    public string boxId;
    public string boxname;
    public string obtainLevel; //문자열로 받아서 구분점을 만들어서 Split을 해서 구분하자
    public string obtainChance;

    public int requireClickMaxLevel;
    public int requireClickNextLevel;
    
}


public class WeaponWrapper
{
    public string weaponName;
    public string weaponInfo;
    public WeaponType weaponType;
    public ObjectRank weaponRank;
    public string spritePath;
}

public class PetWrapper
{
    public string petName;
    public string petInfo;
    public int petType;
    public int petRank;
    public string spritePath;
}


