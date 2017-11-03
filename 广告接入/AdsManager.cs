using UnityEngine;
using System.Collections;
using System;

public class AdsManager {

    private static AdsManager instance = null;
    public static AdsManager GetInstance()
    {
        if (instance == null)
            instance = new AdsManager();
        return instance;
    }

    /// <summary>
    /// 可跳过广告
    /// </summary>
    public void ShowAds()
    {
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        //先找Unity广告，然后找Vungle广告
        Debug.Log("[AdsManager] ShowAds");
        if (UnityAdsHelper.isInitialized && UnityAdsHelper.IsReady("video"))
        {
            Debug.Log("[AdsManager] ShowAds -- UnityAdsHelper");
            UnityAdsHelper.ShowAd("video", () =>
            {
                GameManage.GetInstance().GotoStartup();
            });
        }
        else if (VungleAdsHelper.GetInstance().IsVideoReady())
        {
            Debug.Log("[AdsManager] ShowAds -- VungleAdsHelper");
            VungleAdsHelper.GetInstance().playVideo();
        }
        else
            GameManage.GetInstance().GotoStartup();
#endif
    }
    
    /// <summary>
    /// 奖励广告
    /// </summary>
    public void ShowRewardAds()
    {
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        //先找Unity广告，然后找Vungle广告
        Debug.Log("[AdsManager] ShowRewardAds");
        if (UnityAdsHelper.isInitialized && UnityAdsHelper.IsReady("rewardedVideo"))
        {
            Debug.Log("[AdsManager] ShowRewardAds -- UnityAdsHelper");
            UnityAdsHelper.ShowAd("rewardedVideo", () =>
            {
                //复活
                GameManage.GetInstance().RelifeGame();
            });
        }
        else if (VungleAdsHelper.GetInstance().IsRewardVideoReady())
        {
            Debug.Log("[AdsManager] ShowRewardAds -- VungleAdsHelper");
            VungleAdsHelper.GetInstance().playRewardVideo();
        }
        else
            GameManage.GetInstance().GotoStartup();
#endif
    }



}
