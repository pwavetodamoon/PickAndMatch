using System;
namespace PickandMatch.Scripts.DataScripts
{
    [Serializable]
    public class DataPlayer
    {
        public int CurrentIndexLevels;
        public int Star;
        public int Coin;
        public DataPlayer(int currentIndexLevels, int star, int coin)
        {
            CurrentIndexLevels = currentIndexLevels;
            Star = star;
            Coin = coin;
        }
    }
}
