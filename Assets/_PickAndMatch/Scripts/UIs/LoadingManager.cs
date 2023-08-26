using deVoid.UIFramework;
using PickandMatch.Scripts.Defines;
using UnityEngine;

public sealed class LoadingManager : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameLoadingScene;

    private void Awake()
    {
        _uIFrameLoadingScene = _defaultUISetting.CreateUIInstance();
        
    }

    private void Start()
    {
        _uIFrameLoadingScene.OpenWindow(ScreenIds.UILoadingScene);
    }
   
}
