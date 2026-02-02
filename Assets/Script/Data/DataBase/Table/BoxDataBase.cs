using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;


[Serializable]
public class BoxDataSheet
{
    public string boxId;
    public string boxname;
    public string obtainLevel; //문자열로 받아서 구분점을 만들어서 Split을 해서 구분하자
    public string obtainChance;

    public string requireClickMaxLevel;
    public string requireClickNextLevel;
    public string spritePath;

}

[Serializable]
public class BoxWrapper
{ 
    public List<BoxDataSheet> boxDatas;
}


[Serializable]
public class BoxData
{
    public int boxID;
    public string boxname;

    [Header("등장 가능 등급")]
    public List<int> obtainLevel = new List<int>();
    [Header("확률")]
    public List<float> obtainChance = new List<float>();

    [Header("박스 이미지")]
    public string spritePath;
    public Sprite boxsprite;
   

    [Header("클릭 관련 정보")]
    public List<int> requireClickMaxLevel = new List<int>();
    public List<int> requireClickNextLevel = new List<int>();


    [Header("드랍 테이븦")]
    public List<DropItemInfo> dropTable = new List<DropItemInfo>();
 
}

[Serializable]
public class DropItemInfo
{
    public IItemData data;
    public int weight; //가중치 확률
}

public class BoxDataBase
{

    private Dictionary<int, List<BoxData>> boxDic = new Dictionary<int, List<BoxData>>();

  
    public void LoadBoxData(string jsonString, Dictionary<int,IItemData> ItemMap)
    {
        boxDic.Clear();

        BoxWrapper wrapper = JsonUtility.FromJson<BoxWrapper>(jsonString);
        if (wrapper != null || wrapper.boxDatas == null)
        {
            return;
        }

        foreach (var sheet in wrapper.boxDatas)
        {
            BoxData newBox = new BoxData();

            if (int.TryParse(sheet.boxId, out int parseId))
            {
                newBox.boxID = parseId;
            }
            else 
            {
                newBox.boxID = 0;
            }

            newBox.boxname = sheet.boxname;
            newBox.requireClickMaxLevel = ParssingToList(sheet.requireClickMaxLevel);
            newBox.requireClickNextLevel = ParssingToList(sheet.requireClickNextLevel);

            newBox.spritePath = sheet.spritePath;
            newBox.boxsprite = null;


            if (!string.IsNullOrEmpty(sheet.obtainLevel) && !string.IsNullOrEmpty(sheet.obtainChance))
            { 
                string[] ItemsId = sheet.obtainLevel.Split('|');
                string[] chance = sheet.obtainChance.Split('|');
                int count = Mathf.Min(ItemsId.Length, chance.Length);

                for (int i = 0; i < count; i++)
                {
                    if (int.TryParse(ItemsId[i], out int ItemId) && int.TryParse(chance[i], out int weight))
                    {
                        DropItemInfo info = new DropItemInfo();
                        info.data = ItemMap[ItemId];
                        info.weight = weight;
                        newBox.dropTable.Add(info);
                    }
                }
            }

            if (!boxDic.ContainsKey(newBox.boxID))
            {
                boxDic.Add(newBox.boxID, new List<BoxData>());
            }


        }



    }


    private List<int> ParssingToList(string data, char separator = '|') // 시트에서 등장등급 및 레벨 저장할때 파씽용
    {
        List<int> result = new List<int>();
        if (string.IsNullOrEmpty(data))
        {
            return result; //빈깡통
        }

        string[] splits = data.Split(separator);
        foreach (string s in splits)
        {
            if (int.TryParse(s, out int value))
            {
                result.Add(value);
                //int값으로 넣어줘야하니까
            }
                    
        }

        return result;
    }


    //랜덤으로 상자뽑기
    public BoxData GetBoxData(int id)
    {
        if (boxDic.ContainsKey(id) && boxDic[id].Count > 0)
        {
            int randomNum = UnityEngine.Random.Range(0, boxDic.Count);
            return boxDic[id][randomNum];
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
