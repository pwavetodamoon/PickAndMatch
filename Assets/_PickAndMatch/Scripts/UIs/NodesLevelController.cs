using deVoid.Utils;
using PickandMatch.Scripts.DataScripts;
using PickandMatch.Scripts.Defines;
using System;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace PickandMatch.Scripts.UIs
{
    public sealed class NodesLevelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textSetLevelUi;
        [SerializeField] private Button _nodeButton;
        [SerializeField] private bool _isUnlockLevel;
        [SerializeField] private uint _levelGame;

        public void SetNumberNodeLevel(uint valueLevel) // lấy giá trị từ 0 (uint)
        {
            _levelGame = valueLevel;
            _textSetLevelUi.text = valueLevel.ToString();
        }
        public void SetLoadGame(Action actionJointLevel)
        {
            _nodeButton.onClick.AddListener(() =>
            {
                if (!GetUnlockLevelValue())
                {
                    Signals.Get<NodeLevelsSignals>().Dispatch();
                    return;
                }
                actionJointLevel.Invoke();
                PlayerPrefs.SetInt("Key_Level_Select", (int)_levelGame);
            });
        }
        public void SetUnlockLevelValue(bool value)
        {
            _isUnlockLevel = value;
        }

        public bool GetUnlockLevelValue()
        {
            return _isUnlockLevel;
        }  
    }

}
