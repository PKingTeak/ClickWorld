using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public class UI_PopUp : BaseUI
{
    public override void Init()
    {
        base.Init();
        UIManager.Instance.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
    
}
