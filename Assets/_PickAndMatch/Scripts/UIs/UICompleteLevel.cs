using deVoid.UIFramework;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;
using TMPro;
using UnityEngine;
using PickandMatch.Scripts.DataScripts;
namespace PickandMatch.Scripts.UIs
{
    public class UICompleteLevel : APanelController
    {      
        [SerializeField] private TextMeshProUGUI _textTotalCoin;
        [SerializeField] private GameObject[] _starts;

        protected override void Awake()
        {
            base.Awake();
            Signals.Get<StatusGame>().AddListener(DisPlayCoin);
            Signals.Get<CheckCount>().AddListener(DisPlayStar);

        }
        public void OnButtonNextLevel()
        {
            Signals.Get<NextLevel>().Dispatch();
            Signals.Get<ShowLevel>().Dispatch();
        }
        public void DisPlayCoin(GameStatus status)
        {
            _textTotalCoin.text = PlayerData.Instance.GetCoin().ToString();
        }
        public void DisPlayStar(int countStart)
        {
            for (int index = 0; index < _starts.Length; index++)
            {
                if (index < countStart)
                {
                    _starts[index].SetActive(true);
                }
                else
                    _starts[index].SetActive(false);
            }
        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            Signals.Get<StatusGame>().RemoveListener(DisPlayCoin);
            Signals.Get<CheckCount>().RemoveListener(DisPlayStar);
        }
    }

}
