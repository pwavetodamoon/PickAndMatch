using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

using Object = UnityEngine.Object;

namespace Skywatch.AssetManagement
{
    //Special thanks to TextusGames for their forum post: https://forum.unity.com/threads/how-to-get-asset-and-its-guid-from-known-lable.756560/
    public class UnLoadAssetsByLabelOperation : AsyncOperationBase<IList<IResourceLocation>>
    {
        string _label;
        Dictionary<object, AsyncOperationHandle> _loadedDictionary;
        Dictionary<object, AsyncOperationHandle> _loadingDictionary;
        Action<object, AsyncOperationHandle> _loadedCallback;

        public UnLoadAssetsByLabelOperation(Dictionary<object, AsyncOperationHandle> loadedDictionary, Dictionary<object, AsyncOperationHandle> loadingDictionary,
            string label, Action<object, AsyncOperationHandle> loadedCallback)
        {
            _loadedDictionary = loadedDictionary;
            if (_loadedDictionary == null)
                _loadedDictionary = new Dictionary<object, AsyncOperationHandle>();
            _loadingDictionary = loadingDictionary;
            if (_loadingDictionary == null)
                _loadingDictionary = new Dictionary<object, AsyncOperationHandle>();

            _loadedCallback = loadedCallback;

            _label = label;
        }

        protected override void Execute()
        {
            #pragma warning disable CS4014
            DoTask();
            #pragma warning restore CS4014
        }

        public async UniTask DoTask()
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync(_label);
            var locations = await locationsHandle.ToUniTask();

            Complete(locations, true, string.Empty);
        }

        bool TryGetKeyLocationID(IResourceLocator locator, object key, out string internalID)
        {
            internalID = string.Empty;
            var hasLocation = locator.Locate(key, typeof(Object), out var keyLocations);
            if (!hasLocation)
                return false;
            if (keyLocations.Count == 0)
                return false;
            if (keyLocations.Count > 1)
                return false;

            internalID = keyLocations[0].InternalId;
            return true;
        }
    }
}