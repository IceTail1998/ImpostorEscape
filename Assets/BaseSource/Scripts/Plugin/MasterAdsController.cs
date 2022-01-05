using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MasterAdsController : MonoBehaviour
{
    public MasterControl masterControl;
    [SerializeField]
    private List<IAdsController> adsControllers;

    [SerializeField]
    float interstitialTimer = 0;
    float timeDelayBanner = 4f;
    public bool isInterNull = true;
    public IAdsController currentLoadedBannerCtr, currentLoadedInterstitialCtr, currentLoadedRewardCtr;
    #region init
    public void Init()
    {
        timeDelayBanner = 4f;
        PlayerPrefs.DeleteKey("TimeToShowAds");
        PlayerPrefs.SetString("TimeToShowAds", System.DateTime.Now.ToString());
        Debug.Log("INIT ADS CONTROLLER");
        adsControllers = new List<IAdsController>();
        foreach (Transform ac in transform)
        {
            if (!ac.gameObject.activeInHierarchy) continue;
            IAdsController ia = ac.GetComponent<IAdsController>();
            if (adsControllers.Count > 0)
            {
                adsControllers[adsControllers.Count - 1].SetNext(ia);
            }
            adsControllers.Add(ia);
            ia.Init(this, OnInitComnpleted);
        }


        //adsControllers[0].LoadBanner();


        try
        {
            LoadRule();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
    public void OnInitComnpleted()
    {
        isInit = true;
    }

    public void SetOnTop(string adNetworkName)
    {
        List<IAdsController> list = new List<IAdsController>();
        list.AddRange(adsControllers);
        Debug.Log("TOTAL LIST: " + list.Count);
        adsControllers.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].ToString().Contains(adNetworkName))
            {
                adsControllers.Insert(0, list[i]);
                list.RemoveAt(i);
                break;
            }
        }
        adsControllers.AddRange(list);
        for (int i = 0; i < adsControllers.Count - 1; i++)
        {
            adsControllers[i].SetNext(adsControllers[i + 1]);
            Debug.Log("NEW" + i + " " + adsControllers[i].ToString());
        }

    }
    public bool CanPlayInterstitialAd()
    {
        return interstitialTimer <= 0;
    }
    public float GetInterstitialTimer()
    {
        return this.interstitialTimer;
    }
    public void SetInterstitialTimer(float time)
    {
        Debug.Log("SET TIMER:" + time);
        this.interstitialTimer = time;
    }
    public float rewardedAdWaitTime = 0f, bannerAdWaitTime = 0, delayRewardTimer = 0f;
    bool canShow = false;
    bool isInit = false;
    void Update()
    {
        if (isInit)
        {
            Debug.Log("ADMOB ON COMPLETED");
            adsControllers[0].LoadInterstitial();
            adsControllers[0].LoadRewardedVideo();

            if (PrefInfo.IsUsingAd())
            {
                ShowBanner();
            }
            else
            {
                HideBanner();
            }
            isInit = false;
        }
        //if (canShow)
        //{
        //    canShow = false;
        //    if (currentPanel != null)
        //    {
        //        currentPanel.ShowAfterAd();
        //    }
        //    currentPanel = null;
        //}
        if (interstitialTimer > 0)
        {
            interstitialTimer -= Time.deltaTime;
        }
        //banner
        if (showBanner)
        {
            if (bannerAdWaitTime < timeDelayBanner)
            {
                bannerAdWaitTime += Time.deltaTime;
            }
            else
            {
                Debug.Log("Check show banner");
                bannerAdWaitTime = 0;
                if (timeDelayBanner < 10)
                    timeDelayBanner += 2f;

                if (currentLoadedBannerCtr != null)
                {
                    if (currentLoadedBannerCtr.ShowBanner())
                    {
                        showBanner = false;
                    }
                }
                else
                {
                    LoadBanner();
                }
            }

        }

        // interstitial
        if (currentLoadedInterstitialCtr != null && showInterstitial)
        {

            currentLoadedInterstitialCtr.ShowInterstitial();
            showInterstitial = false;
            currentLoadedInterstitialCtr = null;
        }
        else if (showInterstitial && currentLoadedInterstitialCtr == null)
        {
            LoadInterstitial();
            showInterstitial = false;
        }

        //rewarded ad



        if (delayRewardTimer > 0)
        {
            delayRewardTimer -= Time.deltaTime;
        }
        if (currentLoadedRewardCtr != null && showReward)
        {
            int count = 0;
            for (int i = 0; i < adsControllers.Count; i++)
            {
                if (adsControllers[i] == currentLoadedRewardCtr)
                {
                    count = i;
                    break;
                }
            }
            currentLoadedRewardCtr.ShowRewardedVideo();
            delayRewardTimer = 5;
            showReward = false;
            SetRewardUser();
            currentLoadedRewardCtr = null;
        }
        else if (showReward && currentLoadedRewardCtr == null)
        {
            if (rewardedAdWaitTime > 0.5f)
            {
                rewardedAdWaitTime = 0;
                showReward = false;
                // thông báo ko có AD

                MessageCallBackPopupPanel.INSTACNE.Active("There is no ad available, please try again later");
                OnFail();
                //}

            }
            else
            {
                rewardedAdWaitTime += Time.deltaTime;
            }
        }
    }
    #endregion
    #region banner

    public bool showBanner, showInterstitial, showReward;
    public void LoadBanner()
    {
        Debug.Log("LOAD BANNER MAC");
        adsControllers[0].LoadBanner();

    }
    public void HideBanner()
    {
        Debug.Log("HIDE BANNER MAC");
        adsControllers[0].HideBanner();
        //if(AdPanel.Instance!=null)
        //AdPanel.Instance.gameObject.SetActive(false);

    }
    public bool ShowBanner()
    {
        if (PrefInfo.IsUsingAd())
        {
            Debug.Log("SHOW BANNER MAC");
            showBanner = true;
            if (currentLoadedBannerCtr == null)
            {
                LoadBanner();
            }
            return true;
        }
        return false;
    }
    #endregion
    //
    #region interstitial
    public void LoadInterstitial()
    {
        Debug.Log("LOAD INTER MAC");
        if (currentLoadedInterstitialCtr == null)
        {
            adsControllers[0].LoadInterstitial();
        }
    }
    public bool IsInterstitialReady()
    {
        return adsControllers[0].IsInterstitialReady();
    }
    public bool ShowInterstitial()
    {
        Debug.Log("-------------------------SHOW INTERSTITIAL AD: !!!!--------------------");
        if (!isAllowShowInter(false))
        {
            Debug.Log("-<<<<<<<<<<<<<<<<<<<<NOT ALLOW SHOW INTERSTITIAL AD: !!!!");
            return false;
        }
        if (!PrefInfo.IsUsingAd()) return false;
        if (currentLoadedInterstitialCtr == null)
        {
            LoadInterstitial();
        }
        else
        {
            PlayerPrefs.DeleteKey("TimeToShowAds");
            Debug.Log("->>>>>>>>>>>>>>>>>>>>>>>>>>>>ALLOW SHOW INTERSTITIAL AD: !!!!");
            showInterstitial = true;
        }
        return true;
    }
    void LoadInterstitialEvents()
    {
        adsControllers[0].LoadInterstitialEvents();
    }

    bool showInterstitialSuccess = false, notShowInterstitialSuccess = false;
    private void FixedUpdate()
    {
        if (showInterstitialSuccess)
        {
            PlayerPrefs.SetString("TimeToShowAds", System.DateTime.Now.ToString());
            showInterstitialSuccess = false;
            ResetCountGame();
        }
        if (notShowInterstitialSuccess)
        {
            PlayerPrefs.SetString("TimeToShowAds", System.DateTime.Now.ToString());
            ResetCountGame();
            notShowInterstitialSuccess = false;
            //AdInterPanel.Instance.SetUp();
        }
    }
    public void InterstitialCallback(bool showResult)
    {
        Debug.Log("CALL BACK " + showResult);
        if (showResult)
        {

            showInterstitialSuccess = true;
            isReward = false;

        }
        else
        {
            notShowInterstitialSuccess = true;
            isReward = false;
        }
    }

    #endregion

    //

    #region reward

    int currentNetworkForReward = 0;
    public void LoadRewardedVideo()
    {
        if (currentLoadedRewardCtr == null)
        {
            Debug.Log("LOAD REWARD MAC");
            adsControllers[0].LoadRewardedVideo();
            currentNetworkForReward = 0;
        }
    }
    public void ShowRewardedVideo()
    {
        Debug.Log("SHOW REWARD MAC");
        showReward = true;
        if (currentLoadedRewardCtr == null)
        {
            LoadRewardedVideo();
            Debug.Log("MAC SHOW REWARD FALSE: NO Controller ");
        }
        else
        {
            SetRewardUser();
        }
    }
    public void OnRewarded()
    {
        Debug.Log("ON REWARD MAC");
        masterControl.OnRewardedAd();
        LoadRewardedVideo();
    }

    public void OnFail()
    {
        masterControl.OnFail();
        LoadRewardedVideo();
    }

    //
    #endregion

    //

    #region logicads
    string adSetting_time_iap = "";
    string adSetting_time_normal = "";
    string adSetting_play = "";
    string adSetting_level = "";

    int adSetting_time_reward = 30;
    int[] adSetting_time_iap_list;
    int[] adSetting_time_normal_list;
    int[] adSetting_play_list;
    int[] adSetting_level_list;
    public bool isReward, isIAP;
    public int countGame = 0;
    public void LoadRule()
    {
        adSetting_level = PlayerPrefs.GetString("AdSetting_level", "2,15,35,60,100");
        Debug.Log("ADADAD:" + adSetting_level);
        adSetting_level_list = Array.ConvertAll(adSetting_level.Split(','), int.Parse);

        adSetting_play = PlayerPrefs.GetString("AdSetting_play", "2,2,2,2,2");
        adSetting_play_list = Array.ConvertAll(adSetting_play.Split(','), int.Parse);

        adSetting_time_normal = PlayerPrefs.GetString("AdSetting_time_normal", "30,30,30,30,30");
        adSetting_time_normal_list = Array.ConvertAll(adSetting_time_normal.Split(','), int.Parse);

        adSetting_time_reward = PlayerPrefs.GetInt("AdSetting_time_reward", 15);

        adSetting_time_iap = PlayerPrefs.GetString("AdSetting_time_iap", "65,60,55,50,45");
        adSetting_time_iap_list = Array.ConvertAll(adSetting_time_iap.Split(','), int.Parse);

        isIAP = (PlayerPrefs.GetInt("UserIAP", 0) == 1);
    }

    public void ResetCountGame()
    {
        countGame = 0;
        PlayerPrefs.SetInt("CountGameToShowAds", 0);
    }

    public void SetRewardUser()
    {
        isReward = true;
        PlayerPrefs.SetString("TimeToShowAds", System.DateTime.Now.AddSeconds(adSetting_time_reward).ToString());

    }

    public bool isAllowShowInter(bool overwrite)
    {
        try
        {
            if (!PrefInfo.IsUsingAd()) return false;

            if (overwrite)
            {
                return true;
            }
            int index = 0;
            int a = LevelManage.Instance.GetMaxLevelCanPlay(0) - 1;
            //int a = CanvasManager.Instance.GetMaxLevelCanPlay() - 1;

            int level = a;
            isIAP = PlayerPrefs.GetInt("UserIAP", 0) == 1;

            DateTime timeOld = DateTime.Parse(PlayerPrefs.GetString("TimeToShowAds", new DateTime(1990, 1, 1).ToString()));
            TimeSpan tp = DateTime.Now - timeOld;
            double time = tp.TotalSeconds;
            Debug.Log(("<><><><>LEVEL :" + level) + " || " + "TIME AD :" + time);
            if (level < adSetting_level_list[0])
            {
                return false;
            }
            else if (level >= adSetting_level_list[0] && level < adSetting_level_list[1])
            {
                index = 1;
            }
            else if (level >= adSetting_level_list[1] && level < adSetting_level_list[2])
            {
                index = 2;
            }
            else if (level >= adSetting_level_list[2] && level < adSetting_level_list[3])
            {
                index = 3;
            }
            else
            {
                index = 4;
            }


            float timeRequire = adSetting_time_normal_list[index - 1];
            if (isIAP)
            {
                timeRequire = adSetting_time_iap_list[index - 1];
            }
            else
            {
                if (isReward)
                {
                    timeRequire = adSetting_time_normal_list[index - 1] + adSetting_time_reward;
                }
                else
                {
                    timeRequire = adSetting_time_normal_list[index - 1];
                }
            }

            Debug.Log("CHECK TIME : count: " + countGame + " \ttime: " + timeRequire);
            if (countGame >= adSetting_play_list[index - 1] || time >= timeRequire)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e);
            return false;
        }

    }
    #endregion
}

public interface IAdsController
{
    void Init(MasterAdsController ctr, System.Action callback);
    void LoadBanner();
    bool ShowBanner();
    void HideBanner();
    //
    void LoadInterstitial();
    bool ShowInterstitial();
    void LoadInterstitialEvents();
    bool IsInterstitialReady();
    //
    void LoadRewardedVideo();
    bool ShowRewardedVideo();
    void OnRewarded();
    //
    void LoadNativeAd();
    void ShowNativeAd();
    void HideNativeAd();

    void SetNext(IAdsController ctr);
}