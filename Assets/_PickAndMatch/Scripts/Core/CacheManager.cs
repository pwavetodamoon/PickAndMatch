using PickandMatch.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using PickandMatch.Scripts.Defines;
using PickandMatch.Scripts.Data;
using Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Skywatch.AssetManagement;

namespace PickandMatch.Scripts.Core
{
    public class CacheManager : ManualSingletonMono<CacheManager>
    {
        [HideInInspector] public Dictionary<Defines.CardType, CardData> DictionaryCardData = new Dictionary<Defines.CardType, CardData>();
        [HideInInspector] public Dictionary<uint, LevelData> DataLevels = new Dictionary<uint, LevelData>();
        private const string cardDataPath = "DataCards";
        private const string levelDataPath = "DataLevels";
        private CardController _prefabsCard;


        public override void Awake()
        {
            base.Awake();
            AddDataLevel();
            AddDataCard(); 
            LoadCardAdreesable();
            DontDestroyOnLoad(gameObject);
        }

        #region CARD_DATA
        private void AddDataCard()
        {
            AsyncOperationHandle loader = AssetManager.LoadAssetsByLabelAsync("DataCard");
            loader.Completed += op =>
            {
                List<object> results = op.Result as List<object>;
                foreach (object obj in results)
                {
                    CardData itemConfig = obj as CardData;
                    if (itemConfig == null)
                        continue;
                    DictionaryCardData.Add(itemConfig.Type, itemConfig);
                }
            };
            Debug.Log("AddDataCard ");

        }

        public Sprite GetCardValue(CardType cardType)
        {
            if (DictionaryCardData.ContainsKey(cardType))
            {
                return DictionaryCardData[cardType].Sprite;
            }
            return null;

        }
        #endregion
        #region LOAD_CARD_PREFAB
        private void LoadCardAdreesable()
        {
            AsyncOperationHandle<GameObject> cardControl = Addressables.LoadAssetAsync<GameObject>("Prefabs");
            cardControl.Completed += op =>
            {
                if (cardControl.Result != null)
                {
                    _prefabsCard = cardControl.Result.GetComponent<CardController>();
                    Debug.Log("LoadCardAdreesable Done");
                }
            };
        }
        public CardController GetCardPrefab()
        {
            return _prefabsCard;
        }

        #endregion
        #region LEVEL_DATA
        private void AddDataLevel()
        {
            AsyncOperationHandle loader = AssetManager.LoadAssetsByLabelAsync("DataLevel");
            loader.Completed += op =>

            {
                List<object> results = op.Result as List<object>;
                foreach (object obj in results)
                {
                    LevelData itemConfig = obj as LevelData;
                    if (itemConfig == null)
                        continue;

                    DataLevels.Add(itemConfig.LevelIndex, itemConfig);
                }
            };
            Debug.Log("AddDataLevel ");
        }
        public LevelData GetDataLevel(uint levelIndex)
        {
            Debug.Log("GetDataLevel ");

            if (DataLevels != null && DataLevels.ContainsKey(levelIndex))
            {
                Debug.Log("GetDataLevel  # null");

                return DataLevels[levelIndex];
            }
            Debug.Log("GetDataLevel  null");

            return null;
        }
    }
    #endregion

}
