using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace PickandMatch.Scripts.DataScripts
{
    public sealed class PlayerData : AutoSingletonMono<PlayerData>
    {
        public string PlayerID { get; set; }

        private DatabaseReference _dbreference;
        private DataPlayer _dataPlayer;

        public override void Awake()
        {
            base.Awake();
            PlayerID = SystemInfo.deviceUniqueIdentifier; // Tạo một định danh
            _dbreference = FirebaseDatabase.DefaultInstance.RootReference; // Bắt đầu truy xuất từ node đầu tiên
            _dataPlayer = new DataPlayer(1, 0, 0);
            DontDestroyOnLoad(this);
        }

        private IEnumerator CreatePlayerCoroutine()
        {
            var playerData = _dbreference.Child(PlayerID).GetValueAsync();
            yield return new WaitUntil(() => playerData.IsCompleted);
            if (playerData.Result == null || !playerData.Result.Exists)
            {
                string json = JsonUtility.ToJson(_dataPlayer);
                var task = _dbreference.Child(PlayerID).SetRawJsonValueAsync(json);
                yield return new WaitUntil(() => task.IsCompleted);

                if (task.IsCompleted)
                {                   
                    GetPlayerData();
                }
                else if (task.IsFaulted)
                {
                    //todo
                }
            }
            else
            {
                GetPlayerData();
            }

            if (playerData.Exception != null)
            {
                yield break;
            }
        }



        private IEnumerator GetDataPlayerCoroutine()
        {
            var playerData = _dbreference.Child(PlayerID).GetValueAsync();
            yield return new WaitUntil(() => playerData.IsCompleted);

            if (playerData.Exception != null)
            {
                yield break;
            }

            if (playerData.Result != null)
            {
                DataSnapshot snapshot = playerData.Result;
                string json = snapshot.GetRawJsonValue();
                _dataPlayer = JsonUtility.FromJson<DataPlayer>(json);

                 SetCurrentIndexLevels(_dataPlayer.CurrentIndexLevels);
                 SetStar( _dataPlayer.Star);
                 SetCoin(_dataPlayer.Coin);
            }
              
        }
        private IEnumerator UpdatePlayerDataCoroutine(int newCurrentIndexLevels, int newStar, int newCoin)
        {
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "CurrentIndexLevels", newCurrentIndexLevels },
                {  "Star", newStar },
                { "Coin", newCoin }
            };

            var task = _dbreference.Child(PlayerID).UpdateChildrenAsync(updates);
            yield return new WaitUntil(() => task.IsCompleted);
        }

        public void CreatePlayerData()
        {

            StartCoroutine(CreatePlayerCoroutine());
        }

        public void GetPlayerData()
        {
            StartCoroutine(GetDataPlayerCoroutine());

        }

        public void UpdatePlayerData(int newCurrentIndexLevels, int newStar, int newCoin)
        {
            StartCoroutine(UpdatePlayerDataCoroutine(newCurrentIndexLevels, newStar, newCoin));
        }
        public int GetCurrentIndexLevels()
        {
            return _dataPlayer.CurrentIndexLevels;
        }

        public void SetCurrentIndexLevels(int value)
        {
            _dataPlayer.CurrentIndexLevels = value;
        }


        public int GetStar()
        {
            return _dataPlayer.Star;
        }

        public void SetStar(int value)
        {
            _dataPlayer.Star = value;
        }

        public int GetCoin()
        {
            return _dataPlayer.Coin;
        }

        public void SetCoin(int value)
        {
            _dataPlayer.Coin = value;
        }

    }
}
