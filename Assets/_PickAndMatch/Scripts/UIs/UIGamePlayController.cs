using deVoid.UIFramework;
using UnityEngine;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;
using PickandMatch.Scripts.Core;
using PickandMatch.Scripts.DataScripts;
using TMPro;
namespace PickandMatch.Scripts.UIs
{
    public class UIGamePlayController : AWindowController
    {
        [SerializeField] public TimeBar _timeBar;
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private int _levelIndex;
        [SerializeField] private int _coins;
        [SerializeField] private int _stars;
        [SerializeField] private NodesLevelController _nodesLevelController;
        protected override void Awake()
        {
            base.Awake();
            Signals.Get<StatusGame>().AddListener(UpdateCointPoint);
            Signals.Get<ShowLevel>().AddListener(UpdateLevelInfor);
            Signals.Get<StartTimeLevel>().AddListener(_timeBar.StartTimeLevel);
            Signals.Get<StatusGame>().AddListener(_timeBar.TimeToggle);
            Signals.Get<StarWinGame>().AddListener(DisPacthEvent);
        }
        private void Start()
        {
            SetLevel();
        }
        public void DisPacthEvent()
        {
            _timeBar.DisPatchStarCount(this);
        }
        private void SetLevel()
        {
            _levelIndex = PlayerData.Instance.GetCurrentIndexLevels();
            _coins = PlayerData.Instance.GetCoin();
            _levelText.text = _levelIndex.ToString();
            _coinText.text = _coins.ToString();
        }
        private void UpdateLevelInfor()
        {
            _levelIndex++;
            _coins = _coins + 5;
            PlayerData.Instance.UpdatePlayerData(_levelIndex, 0, _coins);
            _levelText.text = _levelIndex.ToString();
            _coinText.text = _coins.ToString();
        }
        public void OnOpenPausePanelButton()
        {
            Signals.Get<OpenPausePanel>().Dispatch();
            Signals.Get<StatusGame>().Dispatch(GameStatus.Pause);
            TimeController.SetPauseGame();
        }

        private void UpdateCointPoint(GameStatus status)
        {
            if (status == GameStatus.Win)
            {
                PlayerData.Instance.SetCurrentIndexLevels(_levelIndex);
                PlayerData.Instance.SetCoin(_coins);
            }
        }
        public void SendStarEvent(int starCount)
        {
            Signals.Get<CheckCount>().Dispatch(starCount);
        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            Signals.Get<StatusGame>().RemoveListener(UpdateCointPoint);
            Signals.Get<ShowLevel>().RemoveListener(UpdateLevelInfor);
            Signals.Get<StartTimeLevel>().RemoveListener(_timeBar.StartTimeLevel);
            Signals.Get<StatusGame>().RemoveListener(_timeBar.TimeToggle);
            Signals.Get<StarWinGame>().RemoveListener(DisPacthEvent);
        }
    }

}
