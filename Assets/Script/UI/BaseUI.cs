using UnityEngine;
using System;
using System.Collections.Generic;


public abstract class BaseUI : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected bool _init = false;
    

    protected virtual void Init()
    {
        if (_init)
        {
            return;
        }
        _init = true;
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    { 
        
    
    }



}
