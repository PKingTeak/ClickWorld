using System;
using UnityEngine;

public interface IItemData
{ 
    string ItemName { get; }

    string ItemInfo { get; }

    ItemCategory ItemType { get; }
    
    ObjectRank ItemRank { get; }

    Sprite ItemSprite { get; }
}


public static class EventBus
{
    public static event Action<IItemData> OnItemObtained;
    public static void PublishItem(IItemData item)
    {
        if (item == null)
        {
            return; 
        }

        OnItemObtained?.Invoke(item);
    }
}

