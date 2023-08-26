using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PickandMatch.Scripts.Defines;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

namespace PickandMatch.Scripts.UIs
{
    public class UILevelController : AWindowController
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _howtoPlayButton;

        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _panelPrefabs;
        [SerializeField] private Transform _content;
        [SerializeField] private int _sizeOfPanel;
    
        private float _startPosition = 0f;

        private void Start()
        {
            SpawnPanel();
        }
        protected override void AddListeners()
        {
            base.AddListeners();
            _backButton.onClick.AddListener(BackSceneEvent);
            _howtoPlayButton.onClick.AddListener(ShowUIHowToPlay);

        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            _backButton.onClick.RemoveListener(BackSceneEvent);
            _howtoPlayButton.onClick.RemoveListener(ShowUIHowToPlay);     

        }
        private void BackSceneEvent()
        {
           // SceneManager.LoadScene("MainMenu");
            AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("MenuLevel", LoadSceneMode.Single);
            Debug.Log("Process loading MenuLevel");
        }
        private void ShowUIHowToPlay()
        {
            Signals.Get<ShowHowTo>().Dispatch();         
        }
        private void SetStartScrollRectPosition()
        {
            if (_scrollRect != null)
            {
                // Xác định vị trí ban đầu dựa trên giá trị normalized position
                Vector2 normalizedPosition = new Vector2(_scrollRect.normalizedPosition.y, _startPosition);
                _scrollRect.normalizedPosition = normalizedPosition;
            }
        }

        private void SpawnPanel()
        {
            for (int i = 0; i < _sizeOfPanel; i++)
            {
                var tempPanelLevelProject = Instantiate(_panelPrefabs, _content);
                var _spawnNoteController = tempPanelLevelProject.GetComponent<SpawnNodeController>();
                _spawnNoteController.indexLevel = (uint)(_sizeOfPanel - i - 1);
            }
            SetStartScrollRectPosition();
        }

    }

}
