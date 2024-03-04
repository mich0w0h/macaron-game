using UnityEngine;
using UnityEngine.Advertisements;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

 
public class AdsInitializer : SingletonMonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = false;
    private string _gameId;

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnStart()
    {
        InitializeAds();
    }
 
    public void InitializeAds()
    {
    #if UNITY_IOS
        // send the result of ATT permission request before initialization 
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED) {
            MetaData gdprMetaData = new MetaData("gdpr");
            gdprMetaData.Set("consent", "true");
            Advertisement.SetMetaData(gdprMetaData);
        } else {
            MetaData gdprMetaData = new MetaData("gdpr");
            gdprMetaData.Set("consent", "false");
            Advertisement.SetMetaData(gdprMetaData);
        }
    #endif

    #if UNITY_IOS
            _gameId = _iOSGameId;
    #elif UNITY_ANDROID
            _gameId = _androidGameId;
    #endif

    #if DEVELOPMENT_BUILD || UNITY_EDITOR
        _testMode = true;
    #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}