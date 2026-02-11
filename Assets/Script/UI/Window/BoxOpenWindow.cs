using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoxOpenWindow : UI_PopUp
{

    enum Buttons //이름 동일하게 해줘야 에러가 안생김
    { 
        Btn_Close,
        Btn_Skip
    }
    enum GameObjects
    { 
        Panel_BlockInput
    }
    enum Texts
    { 
        Txt_Title
    }
    
    [Header("연결 요소")]
    [SerializeField] private ClickBoxSystem clickBoxSystem;
    //결과 팝업창도 만들어야함
    public override void Init()
    {
        if (_init)
        {
            return;
        }
        base.Init();

        //자식 프리팹을 스캔해서 이름 기준으로 바인딩
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<Button>((int)Buttons.Btn_Close).onClick.AddListener(OnCloseButtonClicked);

        Get<Button>((int)Buttons.Btn_Close).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Panel_BlockInput).gameObject.SetActive(false);


        ClickBoxSystem.OnSummonEnd -= HandleSummonEnd;
        ClickBoxSystem.OnSummonEnd += HandleSummonEnd;
    }

    private void OnDestroy()
    {
        ClickBoxSystem.OnSummonEnd -= HandleSummonEnd;
    }

    public void OpenBoxWindow(int boxID)
    {
        Init();

        BoxData data = DataManager.Instance.BoxDB.GetBoxData(boxID);
        if (data == null)
        {
            Debug.LogError($"Box ID {boxID} not found.");
            ClosePopupUI();
            return;
        }

        Get<TextMeshProUGUI>((int)Texts.Txt_Title).text = data.boxname;
        Get<Button>((int)Buttons.Btn_Close).gameObject.SetActive(true);
        Get<GameObject>((int)GameObjects.Panel_BlockInput).SetActive(false);

        if (clickBoxSystem != null)
        {
            clickBoxSystem.gameObject.SetActive(true);
            clickBoxSystem.Init(data);
            clickBoxSystem.StartSummon();
        }
    }

  

    private void HandleSummonEnd()
    {
        Debug.Log("소환 종료");
        Get<GameObject>((int)GameObjects.Panel_BlockInput).SetActive(true);

        
    }

    private void ShowCloseButton()
    {
        Get<Button>((int)Buttons.Btn_Close).gameObject.SetActive(true);
        if (clickBoxSystem != null)
        {
            clickBoxSystem.gameObject.SetActive(false);
        }
    }

    private void OnCloseButtonClicked()
    {
        ClosePopupUI();
    }


    

}
