using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public abstract class BaseUI : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objectDic = new Dictionary<Type, UnityEngine.Object[]>(); //다른 Button,기본 제공 컴포넌트들을 저장해야하니까

    protected bool _init = false;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        if (_init)
        {
            return;
        }
        _init = true;
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];


        if (objectDic.ContainsKey(typeof(T)))
        {
            objectDic[typeof(T)] = objects;
        }
        else
        {
            objectDic.Add(typeof(T), objects);
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = FindChild(gameObject, names[i], true);

            }

            else
            { 
                objects[i] = FindChild<T>(gameObject, names[i], true);
            }
        }

    }


    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (!objectDic.TryGetValue(typeof(T), out objects))
        {
            return null;
        }

        return objects[index] as T; 


    }

    protected TextMeshPro GetText(int index)
    {
        return Get<TextMeshPro>(index);
    }
    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }

    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }

    protected GameObject GetObject(int index ) 
    {
        return Get<GameObject>(index); 
    }

    public static T FindChild<T>(GameObject _gameObject, string name, bool recursive = false) where T : UnityEngine.Object
    { 
        if (_gameObject == null)
        {
            return null;
        }

        if (recursive == false)
        {
            for (int i = 0; i < _gameObject.transform.childCount; i++) //자식 컴포넌트들을 전부 탐색
            {
                Transform transform = _gameObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }

            }

        }
        else
        {
            foreach (T component in _gameObject.GetComponentsInChildren<T>())
            {

                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }

            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        { 
        return null;
        }


        return transform.gameObject;
    }

}
