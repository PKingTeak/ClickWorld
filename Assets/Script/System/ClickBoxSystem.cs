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

    private void Start()
    {
        getchSystem = GameManager.Instance.GetchSystem;
    }

    [Header("Setting")]
    [SerializeField] private float SummonDuration;

    private int curClickCount;
    private bool isSummoning = false;
    private Coroutine SummonCoroutine;

    //박스도 등급에 따라서 시간을 정해야될듯

    [Header("박스 정보")]
    [SerializeField] private BoxData testboxdata; //나중에 리스트로 변경 예정 지금 


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
        getchSystem.ExecuteGetch(testboxdata, curClickCount); //매니저를 통해서만 호출
        OnSummonEnd?.Invoke();
       
    }
     
    private void VisualDot()
    {
        transform.DOKill(); //애니메이션 중첩 방지
        transform.DOPunchPosition(Vector2.one * 0.2f, 0.1f, 10, 1);


    }
}
