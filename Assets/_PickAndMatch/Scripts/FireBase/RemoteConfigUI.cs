using Firebase.Extensions;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class RemoteConfigUI : MonoBehaviour
{
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    public static bool _isFetch = false;

    void Start()
    {
        SenderSignal();
        SendEventFetchAuto();
    }
    public void SendEventFetchAuto()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {           
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {          
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
   += ConfigUpdateListenerEventHandler;                
            }
        });
    }
    #region AutoFetch
   
    private async void ConfigUpdateListenerEventHandler(
       object sender, Firebase.RemoteConfig.ConfigUpdateEventArgs args)
    {       
        if (args.Error != Firebase.RemoteConfig.RemoteConfigError.None)
        {           
            return;
        }       
        await Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
        await SenderSignal();        
    }
    private async Task<bool> SenderSignal()
    {
        _isFetch = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
             .GetValue("ShowUIMaintenance").BooleanValue;

        return _isFetch;
    }
   
   

    #endregion



    // [START fetch_async]
    // Start a fetch request.
    // FetchAsync only fetches new data if the current data is older than the provided
    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    // By default the timespan is 12 hours, and for production apps, this is a good
    // number. For this example though, it's set to a timespan of zero, so that
    // changes in the console will always show up immediately.   

    public Task FetchDataAsync()
    {       
       Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    //[END fetch_async]

    void FetchComplete(Task fetchTask)
    {
        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        if (info.LastFetchStatus == Firebase.RemoteConfig.LastFetchStatus.Success)
        {
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
        }       
    }

    //Fetch_AutoMatic

}

