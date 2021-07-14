using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GemsManager gems;
    public GemsUI ui;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded; ;
        this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClose;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnAdLoaded(object sender, System.EventArgs e)
    {
        rewardedAd.Show();
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        
    }

    private void RewardedAd_OnAdClose(object sender, System.EventArgs e)
    {
        gems.AddGems(100);
        ui.UpdateStats();
    }
}

