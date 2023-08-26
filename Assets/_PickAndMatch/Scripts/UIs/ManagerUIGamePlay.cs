using UnityEngine;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine.SceneManagement;
using PickandMatch.Scripts.Defines;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PickandMatch.Scripts.UIs
{
    public sealed class ManagerUIGamePlay : MonoBehaviour
    {
        private UIFrame _uIFrame;
        [SerializeField] private UISettings _defaulUISetting;

        private void Awake()
        {
            _uIFrame = _defaulUISetting.CreateUIInstance();
        }
        private void Start()
        {
            DisplayWindownGame();
            Signals.Get<OpenPausePanel>().AddListener(OpenPausePanel);
            Signals.Get<HidePausePanel>().AddListener(ClosePausePanel);
            Signals.Get<BackToLevelProgess>().AddListener(BackToLevelProgess);
            Signals.Get<NextLevel>().AddListener(HideUIClearLevel);
            Signals.Get<OnRestartLevel>().AddListener(HidePanelLoseGame);
            Signals.Get<StatusGame>().AddListener(HandleGameStatus);
        }
        private void DisplayWindownGame()
        {
            _uIFrame.OpenWindow(ScreenIds.UIGamePlay);
        }

        private void OpenPausePanel()
        {
            _uIFrame.ShowPanel(ScreenIds.UIPauseGame);
           
        }
        private void ClosePausePanel()
        {
            _uIFrame.HidePanel(ScreenIds.UIPauseGame);
        }
        private void BackToLevelProgess()
        {
            AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("MenuLevel", LoadSceneMode.Single);
            Debug.Log("Process loading MenuLevel");
        }
        private void OpenLoseGamePanel()
        {            
            _uIFrame.ShowPanel(ScreenIds.UILoseGame);           
        }
       
        private void OpenClearLevel()
        {
            _uIFrame.ShowPanel(ScreenIds.UIClearLevel);      
        }
        private void HideUIClearLevel()
        {
            _uIFrame.HidePanel(ScreenIds.UIClearLevel);
        }
        private void HidePanelLoseGame()
        {
            _uIFrame.HidePanel(ScreenIds.UILoseGame);
        }
        private void HandleGameStatus(GameStatus status)
        {          
            //if (status == GameStatus.Play)
            //{
                if (status == GameStatus.Win)
                {
                    OpenClearLevel();
                }
                else if (status == GameStatus.Lose)
                {
                    OpenLoseGamePanel();
                }
            //}
           
        }
        private void OnDestroy()
        {
            Signals.Get<OpenPausePanel>().RemoveListener(OpenPausePanel);
            Signals.Get<HidePausePanel>().RemoveListener(ClosePausePanel);
            Signals.Get<BackToLevelProgess>().RemoveListener(BackToLevelProgess);
            Signals.Get<NextLevel>().RemoveListener(HideUIClearLevel);
            Signals.Get<OnRestartLevel>().RemoveListener(HidePanelLoseGame);
            Signals.Get<StatusGame>().RemoveListener(HandleGameStatus);
        }

    }


}
