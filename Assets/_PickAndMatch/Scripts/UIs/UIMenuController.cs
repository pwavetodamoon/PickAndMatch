using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;

namespace PickandMatch.Scripts.UIs
{
    public class UIMenuController : AWindowController
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _volumeButton;
        
        protected override void AddListeners()
        {
            base.AddListeners();
            _startButton.onClick.AddListener(StartSceneEvent);
            _volumeButton.onClick.AddListener(ChangeVolumeEvent);
        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            _startButton.onClick.RemoveListener(StartSceneEvent);
            _volumeButton.onClick.RemoveListener(ChangeVolumeEvent);
        }
        private void StartSceneEvent()
        {
            Signals.Get<UIMenuData>().Dispatch();
        }
        private void ChangeVolumeEvent()
        {
            //TODO : Change volume
        }
    }
}

