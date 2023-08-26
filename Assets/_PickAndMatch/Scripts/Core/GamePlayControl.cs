using System.Collections.Generic;
using UnityEngine;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;

namespace PickandMatch.Scripts.Core
{
    public sealed class GamePlayControl : MonoBehaviour
    {
        private List<CardController> _listCardSelected = new List<CardController>();
        public GridManager GridManager;
        private int _checkCardNum = 2;
        private void Awake()
        {
            Signals.Get<SelectCard>().AddListener(AddCardToList);
            Signals.Get<NextLevel>().AddListener(GridManager.NextLevel);
            Signals.Get<OnRestartLevel>().AddListener(GridManager.RestartLevel);
            Signals.Get<StatusGame>().Dispatch(GameStatus.Play);

        }
        private void Start()
        {
            GridManager.LoadLevelIndex();
            TimeController.SetResumeGame();
        }
        private void AddCardToList(CardController cardController)
        {
            _listCardSelected.Add(cardController);
            bool isFinalCard = _listCardSelected.Count >= _checkCardNum; //Check card, maybe it's final card is added?
            StartCoroutine(cardController.WaitCardFlip(HandleCardCompare, isFinalCard));
        }

        private void HandleCardCompare()
        {
            bool isSame = true;
            for (int cardIndex = 0; cardIndex < _listCardSelected.Count - 1; cardIndex++)
            {
                isSame = IsCompareCard(_listCardSelected[cardIndex].CardType, _listCardSelected[cardIndex + 1].CardType);
                if (isSame == false)
                    break;
            }

            if (!isSame)
            {
                foreach (var card in _listCardSelected)
                {
                    card.BackDefaultCard();
                }
                ClearListCard();
                return;
            }
            foreach (var card in _listCardSelected)
            {
                GridManager.RemoveCardFromList(card);
                card.Kill();
            }
            ClearListCard();
            GridManager.TotalCards -= 2;
            if (GridManager.TotalCards <= 0)
            {
                Signals.Get<StatusGame>().Dispatch(GameStatus.Win);
                Signals.Get<StarWinGame>().Dispatch();
            }
        }
        private void ClearListCard()
        {
            _listCardSelected.Clear();
        }

        private bool IsCompareCard(CardType cardType1, CardType cardType2)
        {
            return (cardType1 == cardType2);
        }
        private void OnDestroy()
        {
            Signals.Get<SelectCard>().RemoveListener(AddCardToList);
            Signals.Get<NextLevel>().RemoveListener(GridManager.NextLevel);
            Signals.Get<OnRestartLevel>().RemoveListener(GridManager.RestartLevel);
        }
    }

}
