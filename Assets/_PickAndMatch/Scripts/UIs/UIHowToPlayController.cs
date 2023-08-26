using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;

public class UIHowToPlayController : APanelController
{
    [SerializeField] private Button _closeButton;
    protected override void AddListeners()
    {
        base.AddListeners();
        _closeButton.onClick.AddListener(HideUIHowToPlay);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        _closeButton.onClick.RemoveListener(HideUIHowToPlay);
    }
    private void HideUIHowToPlay()
    {
        Signals.Get<HideHowto>().Dispatch();
    }

}
