using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using deVoid.UIFramework;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

public class UILoadingSceneController : AWindowController
{
    [SerializeField] private Slider _loadingBar;
    private float _loadingProgress = 0.9f;
    private AsyncOperationHandle<SceneInstance> _loadHandle;
    private string key = "LoginScene";

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
        //_loadHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Additive);
    }

    IEnumerator LoadSceneAsync()
    {
        _loadHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Single);

        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LoginScene");
        //asyncLoad.allowSceneActivation = false;
        while (!_loadHandle.IsDone)
        {
            _loadingBar.value = _loadHandle.PercentComplete;
            //if (progress >= _loadingProgress)
            //{
            //    _loadHandle.allowSceneActivation = true;
            //}
            yield return null;
        }
        _loadingBar.value = 1f;
        SceneManager.SetActiveScene(_loadHandle.Result.Scene);
        Debug.Log("Load Done");
    }
}
