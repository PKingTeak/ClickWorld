using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


public class BoxData
{
    public string boxID;
    public string boxname;

    [Header("등장 가능 등급")]
    public List<int> obtainLevel = new List<int>();
    [Header("확률")]
    public List<float> obtainChance = new List<float>();

    [Header("클릭 관련 정보")]
    public int requireClickMaxLevel;
    public int requireClickNextLevel;

    public void initData(string id ,string name, List<int> levels, List<float> chance, int maxLevel, int nextLevel)
    {
        boxID = id;
        boxname = name;
        obtainLevel = levels;
        obtainChance = chance;
        requireClickNextLevel = nextLevel;
        requireClickNextLevel = nextLevel;
    }
}

public class BoxDataBase
{

    private Dictionary<string, List<BoxData>> boxDic = new Dictionary<string, List<BoxData>>();

    public void Init(List<BoxData> list)
    {
        if (list == null)
        {
            return;
        }

        boxDic.Clear();

        foreach (BoxData box in list)
        {
            SetBox(box);
        }
        
    }

    public void SetBox(BoxData box)
    {
        if (box == null)
        {
            Debug.Log("[BoxDataBase] 빈값이 들어왔습니다.");
            return;
        }

        if (!boxDic.ContainsKey(box.boxID))
        {
            boxDic.Add(box.boxID,new List<BoxData>());
        }

        boxDic[box.boxID].Add(box);


    }

    //랜덤으로 상자뽑기
    public BoxData GetRandomBox(string id)
    {
        if (boxDic.TryGetValue(id, out List<BoxData> list) && list.Count>0)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        return null;
    }

    
    //이름으로 탐색
    public BoxData GetBoxTagName(string boxName)
    {
        foreach(var list in boxDic.Values)
        {
            foreach (var box in list)
            {
                if (box.boxname == boxName)
                {
                    return box;  
                }
            }
        }

        Debug.Log($"[BoxDataBase] 해당 {boxName}가 없습니다.");
        return null;
    }
            
}
