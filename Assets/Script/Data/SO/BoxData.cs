using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxData", menuName = "SOData/BoxData")]
public class BoxData : ScriptableObject
{
    public string boxname;

    [Header("등장 가능 등급")]
    public List<int> obtainLevel = new List<int>();
    [Header("확률")]
    public List<float> obtainChance = new List<float>();

    [Header("클릭 관련 정보")]
    public int requireClickMaxLevel;
    public int requireClickNextLevel;
}
