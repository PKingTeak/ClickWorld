using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class LightEffectSystem : MonoBehaviour
{
    #region 색상 설정
    //원하는 색상을 넣어주기
    [System.Serializable] // 인스펙터에 노출하기 위해 필수
    public struct RankColorData
    {
        public string rankName; // 등급 이름 (예: "Normal", "Rare", "Legend")
        [ColorUsage(true,true)]
        public Color effectColor; // 유니티 컬러 피커 사용
    }

    [SerializeField]
    List<RankColorData> rankColors;
    #endregion

    //쉐이더 설정
    [SerializeField] private string amountPropName = "_EffectAmount";
    [SerializeField] private string baseColor = "_BaseColor";


    //머티리얼 설정
    Renderer renderer;
    MaterialPropertyBlock materialBox;

    //박스 
    int maxRequire;
    int nextRequire;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        materialBox = new MaterialPropertyBlock(); //컴포넌트가 아님
    }

    public void InitBox(int maxNum , int _requireNum)
    { 
        maxRequire = maxNum; 
        nextRequire = _requireNum; //다음 레벨 요구치
    }
    


    public void UpdateVisual(int currentTotalClicks) //박스 클릭시 변화 및 이펙트 변경
    {

        if (renderer == null) return;

        // 1. [MAX LEVEL CHECK] 최대 클릭 수 도달 시
        if (currentTotalClicks >= maxRequire)
        {
            // 마지막 설정된 색상 사용
            Color maxColor = GetColorByIndex(rankColors.Count - 1);
            ApplyShader(1.0f, maxColor); // 이펙트 꽉 채움(1.0)
            return;
        }

        // 2. [CALCULATE] 수학적 계산 (나눗셈 활용)
        // 현재 레벨 인덱스 (몫) : 0 ~ 9번 클릭 -> 0레벨, 10~19번 클릭 -> 1레벨 ...
        int currentLevelIndex = currentTotalClicks / nextRequire;

        // 현재 구간 내 진행률 (나머지) : 15번 클릭이고 구간이 10이라면 -> 5만큼 진행됨 -> 0.5(50%)
        int currentStepProgress = currentTotalClicks % nextRequire;
        float relativeRatio = (float)currentStepProgress / nextRequire;

        // 3. [COLOR] 색상 결정
        Color targetColor = GetColorByIndex(currentLevelIndex);

        // 4. [APPLY] 적용
        ApplyShader(relativeRatio, targetColor);
    }


    private Color GetColorByIndex(int _index)
    {
        if (rankColors == null || rankColors.Count == 0)
        {
        return Color.white;
        }

        
        Color curColor =  rankColors[_index].effectColor;
        return curColor;
    }

    private void ApplyShader(float ratio, Color color)
    {
        renderer.GetPropertyBlock(materialBox);
        materialBox.SetFloat(amountPropName,ratio);
        materialBox.SetColor(baseColor, color);
        renderer.SetPropertyBlock(materialBox);
    }

}
