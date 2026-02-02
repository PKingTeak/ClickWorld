using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;


public class ItemWrapper
{
    public string ItemName;
    public string ItemInfo;
    public ItemCategory ItemCategory;
    public ObjectRank ItemRank;
    public string SpritePath;

    public int DetailType;    
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


