using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PickandMatch.Scripts.Defines;
using PickandMatch.Scripts.DataScripts;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using deVoid.Utils;
using PickandMatch.Scripts.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PickandMatch.Scripts.UIs
{
    public sealed class SpawnNodeController : MonoBehaviour
    {
        [SerializeField] private Transform _nodeBasePositions;
        [SerializeField] private NodesLevelController _nodeLevelPrefab;
        [SerializeField] private List<Transform> _spawnPositions;
        [SerializeField] private List<NodesLevelController> _nodesLevels;
        private int CurLevel;

        public uint indexLevel;

        private void Start()
        {
            FillSpawnPosition();
            SpawnNotesEvent();
            ChangeLevelEvents();
        }

        private void FillSpawnPosition()
        {
            _spawnPositions.Clear();

            foreach (Transform child in _nodeBasePositions)
            {
                _spawnPositions.Add(child);
            }
        }

        private void SpawnNotesEvent()
        {
            for (int index = 0; index < _spawnPositions.Count; index++)
            {
                // Spawn note and set name
                var _nodesTemp = Instantiate(_nodeLevelPrefab, _spawnPositions[index]);
                _nodesTemp.SetNumberNodeLevel((uint)index + indexLevel * 10 + 1);
                _nodesLevels.Add(_nodesTemp);
            }
            UpdateNodeUnlockStatus();
        }

        private void ChangeLevelEvents()
        {
            for (int index = 0; index < _nodesLevels.Count; index++)
            {
                _nodesLevels[index].SetLoadGame(() =>
                {
                    AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("GamePlay", LoadSceneMode.Single);
                    Debug.Log("Process loading GamePlay");
                });
            }
        }

        private void UpdateNodeUnlockStatus()
        {
            int currentIndexLevels = PlayerData.Instance.GetCurrentIndexLevels();
            for (int index = 0; index < _nodesLevels.Count; index++)
            {
                if ((uint)index + indexLevel * 10 + 1 > currentIndexLevels)
                {
                    continue;
                }
                _nodesLevels[index].SetUnlockLevelValue(true); // Mở khóa nút tại vị trí index
            }
        }
    }
}