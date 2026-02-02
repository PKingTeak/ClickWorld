using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics.Tracing;

public class PoolingSystem<T> where T : MonoBehaviour
{
    private Queue<T> poolingQueue = new();

    public T Get()
    {
        if (poolingQueue.Count > 0)
        {
            T item = poolingQueue.Dequeue();
            item.gameObject.SetActive(true);
            return item;
        }

        return null;
    }

    


}
