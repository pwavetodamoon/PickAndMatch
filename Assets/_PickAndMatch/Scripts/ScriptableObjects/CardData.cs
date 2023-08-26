using UnityEngine;
using PickandMatch.Scripts.Defines;

namespace PickandMatch.Scripts.ScriptableObjects
{

    [CreateAssetMenu(fileName = "CardData", menuName = "CardData")]
    public class CardData : ScriptableObject
    {
        public CardType Type = CardType.unknow;
        public Sprite Sprite;
    }
}
