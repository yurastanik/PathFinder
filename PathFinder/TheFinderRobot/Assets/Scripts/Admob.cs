using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine.UI;

public class Admob : MonoBehaviour
{

    // private string APP_ID;
    // private string ADS_ID;
    private RewardedAd rewardedAd;
    [SerializeField] private Image HintCount;

    void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);
        RewardVideoInit();
//         #if UNITY_IPHONE
//             APP_ID = "ca-app-pub-5173937614720313~6422463612";
//         #elif UNITY_ANDROID
//             APP_ID = "ca-app-pub-5173937614720313~1281817572";
// #endif
//         MobileAds.Initialize(APP_ID);

    }



    void RewardVideoInit() {
        string adUnitId;
        #if UNITY_EDITOR
            adUnitId = "unused";
        #elif UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917"; //!!! VERY IMPORTANT: WE use test id now! Only when game is ready we will use real   id = ca-app-pub-5173937614720313/4387682431
            Debug.Log("UNITY_ANDROID");
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313"; // id = ca-app-pub-5173937614720313/5524314917
            Debug.Log("UNITY_IPHONE");
        #else
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
            Debug.Log("unexpected_platform");
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RewardVideoInit();
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Savegame.sv.hint++;
        HintCount.GetComponentInChildren<Text>().text = Langgame.fills.dict["hint left"] + Savegame.sv.hint;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void UserChoseToWatchAd() {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }
}
