using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;



public class GetchSystem
{
    [SerializeField]
    const int MAXChanceNum = 10000;
    [SerializeField]
    private const float clickbonus = 0.05f;


    public ObjectRank ExecuteGetch(BoxData curboxData, int curClickCount)
    {

        //최대로 클릭 증가 카운트가 몇이 증가 가능한지.. 

        //뽑고
        int randomNum = UnityEngine.Random.Range(0, MAXChanceNum);
        ObjectRank rank = ObjectRank.Normal;
                                                //클릭 증가 퍼센트


        int cumulativeChance = 0;

        //보스터 점수 
        int totalbonus = UnityEngine.Mathf.RoundToInt(curClickCount*clickbonus* 100); //반올림
        //일단 확률을 가지고 있어야함

        for (int i = 0; i < curboxData.obtainChance.Count; i++)
        {

            ObjectRank curRank = (ObjectRank)curboxData.obtainLevel[i];
            if (curRank == ObjectRank.Normal) continue;
            
            int baseChance = UnityEngine.Mathf.RoundToInt(curboxData.obtainChance[i] * 100f);

            int finalChance = baseChance + totalbonus;

            cumulativeChance += finalChance;

            if (randomNum <= cumulativeChance)
            {
                rank = curRank;
                break;
            }
        }

        var testweapon = GameManager.Instance.WeaponManager.WeaponTable.GetRandomWeaponByRank(rank);
        
        Debug.Log($"{testweapon.ItemName},{rank}");

        EventBus.PublishItem(testweapon);

        return rank;


    }

}
