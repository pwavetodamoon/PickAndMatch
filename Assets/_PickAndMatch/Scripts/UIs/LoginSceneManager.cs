using deVoid.UIFramework;
using PickandMatch.Scripts.Defines;
using UnityEngine;

namespace PickandMatch.Scripts.UIs
{
    public sealed class LoginSceneManager : MonoBehaviour
    {
        [SerializeField] private UISettings _defaultUISetting = null;
        private UIFrame _uIFrameLoginScene;

        private void Awake()
        {
            _uIFrameLoginScene = _defaultUISetting.CreateUIInstance();

        }

        private void Start()
        {
            _uIFrameLoginScene.OpenWindow(ScreenIds.UILogin);
        }

    }
}
