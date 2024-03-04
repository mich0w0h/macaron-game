using UnityEngine;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class AttPermissionRequest : MonoBehaviour
{
    void Awake()
    {
#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }
}