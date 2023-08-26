using deVoid.UIFramework;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MenuLevelManager : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameMenuLevel;

    private void Awake()
    {
        _uIFrameMenuLevel = _defaultUISetting.CreateUIInstance();
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Start()
    {
        _uIFrameMenuLevel.OpenWindow(ScreenIds.UIMenuLevel);
    }

    private void AddListeners()
    {
        Signals.Get<NodeLevelsSignals>().AddListener(OpenPanelLocked);
        Signals.Get<UIPanelNotificationLockedEvent>().AddListener(BackToMenuLevel);
        Signals.Get<ShowHowTo>().AddListener(ShowHowToUI);
        Signals.Get<HideHowto>().AddListener(HideHowToUI);
    }

    private void RemoveListeners()
    {
        Signals.Get<NodeLevelsSignals>().RemoveListener(OpenPanelLocked);
        Signals.Get<UIPanelNotificationLockedEvent>().RemoveListener(BackToMenuLevel);
        Signals.Get<ShowHowTo>().RemoveListener(ShowHowToUI);
        Signals.Get<HideHowto>().RemoveListener(HideHowToUI);
    }

    private void OpenPanelLocked()
    {
        _uIFrameMenuLevel.OpenWindow(ScreenIds.UIPanelNotificationLocked);
    }

    private void BackToMenuLevel()
    {
        _uIFrameMenuLevel.OpenWindow(ScreenIds.UIMenuLevel);
    }

    private void ShowHowToUI()
    {
        _uIFrameMenuLevel.ShowPanel(ScreenIds.UIHowToPlay);
    }

    private void HideHowToUI()
    {
        _uIFrameMenuLevel.HidePanel(ScreenIds.UIHowToPlay);
    }
}