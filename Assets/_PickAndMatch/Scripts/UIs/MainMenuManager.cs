using deVoid.UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using PickandMatch.Scripts.Defines;
using deVoid.Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PickandMatch.Scripts.UIs
{
    public sealed class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private UISettings _defaultUISetting = null;
        private UIFrame _uIFrame;
        private void Awake()
        {
            _uIFrame = _defaultUISetting.CreateUIInstance();
            AddListeners();
        }
        private void Start()
        {
            _uIFrame.OpenWindow(ScreenIds.UIMainMenu);
            ShowUIMaintenance();
        }
        private void OnDestroy()
        {
            RemoveListeners();
        }
        private void OpenMenuLevel()
        {
           // SceneManager.LoadScene(ScreenIds.MenuLevel);
            AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync(ScreenIds.MenuLevel, LoadSceneMode.Single);
            Debug.Log("Process loading MenuLevel");
        }
        private void BackToMainMenu()
        {
            _uIFrame.OpenWindow(ScreenIds.UIMainMenu);
        }
        #region REMOTECONFIG
        private void ShowUIMaintenance()
        {           
            if (RemoteConfigUI._isFetch == true)
            {
                _uIFrame.ShowPanel(ScreenIds.UIMaintenance);               
            }            
        }
        #endregion
        private void AddListeners()
        {         
            Signals.Get<UIMenuData>().AddListener(OpenMenuLevel);
            Signals.Get<UILevelData>().AddListener(BackToMainMenu);
        }
        private  void RemoveListeners()
        {
            Signals.Get<UIMenuData>().RemoveListener(OpenMenuLevel);
            Signals.Get<UILevelData>().RemoveListener(BackToMainMenu);
        }
    }
}
