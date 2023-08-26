using deVoid.UIFramework;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelNotificationLocked : AWindowController
{
    [SerializeField] private Button _buttonClose;

    protected override void AddListeners()
    {
        base.AddListeners();
        _buttonClose.onClick.AddListener(ClosePanel);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _buttonClose.onClick.RemoveListener(ClosePanel);
    }

    private void ClosePanel()
    {
        Signals.Get<UIPanelNotificationLockedEvent>().Dispatch();
    }    
}
