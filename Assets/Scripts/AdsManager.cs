using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    //string appId = "";
    BannerView bannerView;
    string bannerId = "ca-app-pub-3940256099942544/6300978111";

    InterstitialAd fullScreenAd;
    string fullScreenAdId = "ca-app-pub-3940256099942544/8691691433";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this);
        }

    }

    void Start()
    {
        RequestBanner();
    }

    void RequestBanner()
    {
        bannerView = new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

    }

    void ShowBanner()
    {
        bannerView.Show();
    }

    void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdId);
        AdRequest request = new AdRequest.Builder().Build();
        fullScreenAd.LoadAd(request);
    }

    public void showFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
            fullScreenAd.Show();
        }
    }


    //Handling Events for Banner Ads
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //On loaded
        ShowBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //On Failed
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    //Handle Request
    void HandleRequest(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            bannerView.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerView.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerView.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else {
            // Called when an ad request has successfully loaded.
            bannerView.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerView.OnAdOpening -= HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerView.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerView.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }
    }

    void OnEnable()
    {
        HandleRequest(true);
    }

    void OnDisable()
    {
        HandleRequest(false);
    }

}
