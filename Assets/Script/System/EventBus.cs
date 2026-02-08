using System;
using UnityEngine;


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

