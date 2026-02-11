using UnityEngine;
using System;
using System.Collections.Generic;

using Unity.VisualScripting;
public class UIManager : MonoSingleton<UIManager>
{
  private Stack<UI_PopUp> popupStack = new Stack<UI_PopUp>();

    private int order = 10;

    public T ShowPopupUI<T>(string name = null) where T : UI_PopUp
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{name}");
        if (go == null)
        {
            Debug.LogError($"[UIManager] UI 프리팹을 찾을 수 없습니다: {name}");
            return null;
        }

        GameObject popup = Instantiate(go);
        T popupScript = popup.GetComponent<T>();

        popupStack.Push(popupScript);
        popupScript.Init();

        return popupScript;
    }


    public void ClosePopupUI(UI_PopUp _popup)
    {
        if (popupStack.Count == 0)
        {
            return;
        }

        if (popupStack.Peek() != _popup)
        {
            Debug.LogWarning("닫으려는 팝업이 최상단이 아닙니다. (순서 꼬임 주의)");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
        {
            return;
        }

        UI_PopUp popup = popupStack.Pop();
        Destroy(popup.gameObject); // 혹은 비활성화
        order--; // 오더 순서 복구
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
}
