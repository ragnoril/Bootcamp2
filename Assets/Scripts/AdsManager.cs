using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        Advertisement.Initialize("4893721", true, this);

        LoadAds();
    }

    public string InterstitialAd;
    public string RewardedAd;

    public void LoadAds()
    {
        Advertisement.Load(InterstitialAd, this);
        Advertisement.Load(RewardedAd, this);
    }

    public void LoadAd(string placement)
    {
        Advertisement.Load(placement, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(InterstitialAd, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(RewardedAd, this);
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Unity Ads failed to load: " + placementId + " " + error.ToString() + " " + message);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Unity Ads failed to show: " + placementId + " " + error.ToString() + " " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (RewardedAd.Equals(placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

        }

        Advertisement.Load(placementId, this);
    }
}