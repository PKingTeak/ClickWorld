using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickBoxSystem : MonoBehaviour
{
    public static event Action<int> OnBoxClicked;
    public static event Action OnSummonEnd;


    GetchSystem getchSystem; //동적 할당 불가능
    [Header("Setting")]
    [SerializeField] private float SummonDuration;

    private int curClickCount;
    private bool isSummoning = false;
    private Coroutine SummonCoroutine;

    //박스도 등급에 따라서 시간을 정해야될듯

    [Header("박스 정보")]
    [SerializeField]
    private BoxData boxData;

    private int maxClickCount;
    //  [SerializeField] private BoxData testboxdata; //나중에 리스트로 변경 예정 지금 


    [Header("연결 시스템")]
    [SerializeField] private LightEffectSystem lightEffect;
    [SerializeField] private BoxParticleSystem boxParticle;
    private SpriteRenderer spriteRenderer;


    //박스 정보를 가져오는 기능이 없음. 


    private void Awake()
    {
        getchSystem = GameManager.Instance.GetchSystem;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void Init(BoxData data) // 상자 클릭 부분으로 넘어갈때? init하기?
    {
        if (data == null)
        {
            Debug.Log("[ClickBoxSystem] 데이터가 비어있습니다.");
            return;
        }
        boxData = data;
        maxClickCount = data.rankRatio.Count * data.requireClickNextLevel; 

        if (spriteRenderer != null && data.boxsprite != null)
        {
            spriteRenderer.sprite = data.boxsprite;
        }

        if (boxParticle != null)
        {
            boxParticle.gameObject.SetActive(true);
            boxParticle.UpdateParticle(0, Color.white);
        }

        Debug.Log($"[{data.boxname}] 상자가 세팅되었습니다. (목표 클릭: {maxClickCount})");
    }

    [ContextMenu("Test Start Summon")]
    public void StartSummon()
    {
       
        if (isSummoning)
        {
            return;
        }
        
        curClickCount = 0;
        isSummoning = true;

        if (SummonCoroutine != null)
        {
            StopCoroutine(SummonCoroutine);
        }
        SummonCoroutine = StartCoroutine(SummonTimerRoutine());
        Debug.Log("클릭이 활성화 되었습니다. 클릭을 해주세요");
    }
    private void OnMouseDown()
    {
       ExecuteClick();
    }

    private void ExecuteClick()
    {
        Debug.Log("오브젝트 클릭 감지됨!");

        if (!isSummoning)
        {
            Debug.Log("현재 소환 중이 아닙니다. StartSummon을 먼저 실행하세요.");
            return;
        }

        curClickCount++;

        Color currentRankColor = Color.white; //추후 상자 데이터와 연동해서 각 등급별로 색상을 다르게 지정할예정
        float ratio =  SettingRatio(curClickCount, maxClickCount);
        lightEffect.UpdateVisual(curClickCount);

        //추후 상자의 현재 클릭수 /최대 클릭 가능수 값을 넘겨 받아서 lerp할 예정
        boxParticle.UpdateParticle(curClickCount, currentRankColor);

        VisualDot();
        OnBoxClicked?.Invoke(curClickCount);
    }

    
    public IEnumerator SummonTimerRoutine()
    {

        float timer = SummonDuration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //UI에게 남은 시간 전달 
            yield return null;
        }

        isSummoning = false; //소환 종료
        Debug.Log($"{curClickCount}");
        getchSystem.ExecuteGetch(boxData, curClickCount); //매니저를 통해서만 호출
        OnSummonEnd?.Invoke();
       
    }

    private float SettingRatio(int _curClick, int maxRequire)
    {
        float value = Mathf.Clamp01((float)_curClick / (float)maxRequire);

        return value;
    }
     
    private void VisualDot()
    {
        transform.DOKill(); //애니메이션 중첩 방지
        transform.DOPunchPosition(Vector2.one * 0.2f, 0.1f, 10, 1);


    }
}
