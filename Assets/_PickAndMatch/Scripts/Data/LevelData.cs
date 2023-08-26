using UnityEngine;

namespace PickandMatch.Scripts.Data
{
    [CreateAssetMenu(fileName ="LevelData",menuName ="LevelData")]
    public class LevelData : ScriptableObject
    {
        public float LevelTime;
        public uint LevelIndex;
        [Range(2, 5)]
        public int RowDimension;
        [Range(2, 4)]
        public int ColumnDimension;       
        public int TotalCards;
    }

}
