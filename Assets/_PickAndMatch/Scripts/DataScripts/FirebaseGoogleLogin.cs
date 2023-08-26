using UnityEngine;
using Firebase;
using Firebase.Auth;
using Google;
using System.Collections;
using UnityEngine.SceneManagement;
using PickandMatch.Scripts.Defines;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
namespace PickandMatch.Scripts.DataScripts
{
    public sealed class FirebaseGoogleLogin : MonoBehaviour
    {
        public string webClientId = "1039807892346-1n2rb5dst1o44ulpq25kc0e004iib1f0.apps.googleusercontent.com";

        private FirebaseAuth auth;
        private FirebaseUser user;
        private  void Awake()
        {
            InitializeFirebase();
            Debug.Log("change scene Login Awake");

        }

        private void InitializeFirebase()
        {
                Debug.Log("InitializeFirebase");
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {

                if (task.IsCompleted && task.Result == DependencyStatus.Available)
                {
                    Debug.Log("IsCompleted" + DependencyStatus.Available);

                    auth = FirebaseAuth.DefaultInstance;

                    // Kiểm tra người dùng đã đăng nhập bằng Google hay chưa
                    if (auth.CurrentUser != null && auth.CurrentUser.ProviderId == "google.com" && !string.IsNullOrEmpty(auth.CurrentUser.UserId))
                    {
                        // Người dùng đã đăng nhập bằng Google và có UserID trên Firebase, chuyển đến MainMenu
                        //SceneManager.LoadScene(ScreenIds.MainMenu);
                        AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
                        Debug.Log("change scene to main menu");
                    }
                }
            });
        }
        private IEnumerator GoogleLoginCoroutine()
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                WebClientId = webClientId,
                RequestIdToken = true
            };
            GoogleSignIn.Configuration.UseGameSignIn = false;
            var signInTask = GoogleSignIn.DefaultInstance.SignIn();
            yield return new WaitUntil(() => signInTask.IsCompleted);

            if (signInTask.IsCanceled)
            {
                yield break;
            }

            if (signInTask.IsFaulted)
            {
                yield break;
            }

            var credential = GoogleAuthProvider.GetCredential(signInTask.Result.IdToken, null);
            var authTask = auth.SignInWithCredentialAsync(credential);
            yield return new WaitUntil(() => authTask.IsCompleted);

            if (authTask.IsCanceled || authTask.IsFaulted)
            {
                yield break;
            }
           
            user = authTask.Result;  
            PlayerData.Instance.PlayerID = user.UserId;
            PlayerData.Instance.CreatePlayerData();
            //SceneManager.LoadScene(ScreenIds.MainMenu);
            AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            while (!_loadHandle.IsDone)
            {
                Debug.Log("wating loading scenes");
                yield return null;
            }

        }

        public void GoogleButtonLoginOnClick()
        {
            StartCoroutine(GoogleLoginCoroutine());
        }
        public void Logout()
        {
            auth.SignOut();
            GoogleSignIn.DefaultInstance.SignOut();
            // SceneManager.LoadScene(ScreenIds.LoginScene);
            AsyncOperationHandle<SceneInstance> _loadHandle = Addressables.LoadSceneAsync("LoginScene", LoadSceneMode.Single);
        }
    }
}
