using UnityEngine;
using System.Collections.Generic;
using PickandMatch.Scripts.Defines;
using deVoid.Utils;
using System.Collections;
using PickandMatch.Scripts.DataScripts;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
namespace PickandMatch.Scripts.Core
{
    public sealed class GridManager : MonoBehaviour
    {
        [Range(2, 4)]
        [SerializeField] private int _column;
        [Range(2, 5)]
        [SerializeField] private int _row;
        [SerializeField] private float _distance;
        private CardController _prefabsCard;
        [HideInInspector] public float TotalWidth;
        [HideInInspector] public float TotalHeight;
        private CardType _cardType = CardType.unknow;
        private List<CardController> _gridCard = new List<CardController>();
        private List<CardController> _coupleCards = new List<CardController>();
        private int _coupleNumber = 1;
        public static uint CurLevel;
        public static int TotalCards;

        //InitialGrid
        public void NextLevel()
        {
            CurLevel++;
            SetUpGrid(CurLevel);
        }
        public void RestartLevel()
        {
            StartCoroutine(ReSetupGrid());
        }
        public void LoadLevelIndex()
        {
            CurLevel = (uint)PlayerPrefs.GetInt("Key_Level_Select");

            Debug.Log("CurLevel " + CurLevel);
            SetUpGrid(CurLevel);
        }

        private void SetUpGrid(uint levelIndex)
        {
            Debug.Log("Star SetUpGrid ");
            var lvConfig = CacheManager.Instance.GetDataLevel(levelIndex);
            Debug.Log("LvConfig time " + lvConfig.LevelTime);
            _column = lvConfig.ColumnDimension;
            _row = lvConfig.RowDimension;
            TotalCards = lvConfig.TotalCards;
            InitGrid(_column, _row);
            TotalWidth = _row * _distance;
            TotalHeight = _column * _distance;
            Vector3 initialPos = Vector3.zero;
            transform.Translate(initialPos - GetVectorOffest());

            Signals.Get<StartTimeLevel>().Dispatch(lvConfig.LevelTime);
        }
       
        private IEnumerator ReSetupGrid()
        {
            yield return WaitDestroyCard();
            SetUpGrid(CurLevel);

        }
        private void InitGrid(int column, int row)
        {
            _prefabsCard = CacheManager.Instance.GetCardPrefab();
            for (int colIndex = 0; colIndex < column; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < row; rowIndex++)
                {
                    CardController gO;
                    if (_prefabsCard != null)
                    {
                        gO = Instantiate(_prefabsCard, transform);
                        gO.transform.position = new Vector3(colIndex * _distance, rowIndex * _distance, 0);
                        _gridCard.Add(gO);
                    }
                    else
                    {
                        Debug.Log("Null Prefabs card");
                    }
                  
                }
            }
            SetCoupleCards();
            SetUpCoupleKeyCard();
        }
        IEnumerator WaitDestroyCard()
        {
            int totalCard = _coupleCards.Count;

            while (totalCard > 0)
            {
                totalCard--;
                if (_coupleCards[totalCard] != null)
                {
                    _coupleCards[totalCard].Kill();
                }
            }
            _coupleCards.Clear();
            yield return new WaitUntil(() => totalCard <= 0);
        }
        public void RemoveCardFromList(CardController card)
        {
            if (card != null)
            {
                _coupleCards.Remove(card);
            }
        }
        private void SetCoupleCards()
        {
            int totalCards = _gridCard.Count;
            while (totalCards >= _coupleNumber)
            {
                CreateRandomCardList(ref totalCards);
            }
        }
        private void CreateRandomCardList(ref int total)
        {
            int index = Random.Range(0, total);
            _coupleCards.Add(_gridCard[index]);
            _gridCard.RemoveAt(index);
            total--;
        }
        private void SetUpCoupleKeyCard()
        {

            for (int indexCouple = 0; indexCouple < _coupleCards.Count - 1; indexCouple += 2)
            {
                CardType card = GetKeyCard();
                _coupleCards[indexCouple].SetUp(card);
                _coupleCards[indexCouple + 1].SetUp(card);
            }
        }

        private CardType GetKeyCard()
        {
            int begin = (int)CardType.animal01;
            int end = (int)CardType.animal20;
            return (CardType)Random.Range(begin, end);
        }

        //Get vector Offset in order to move this transform to center
        private Vector3 GetVectorOffest()
        {
            float totalWidth = (_row - 1) * _distance;
            float totalHeight = (_column - 1) * _distance;
            return new Vector3(totalHeight / 2, totalWidth / 2, 0);
        }
    }
}
