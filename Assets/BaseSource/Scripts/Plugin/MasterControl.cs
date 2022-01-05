using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;

public class MasterControl : MonoBehaviour
{
    public static MasterControl Instance;
    public MasterAdsController adsController;
    //public Purchaser purchaser;
    [SerializeField]
    private string[] timeServer;
    bool isInit = false;

    public bool isConnected = false;
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            adsController = GetComponentInChildren<MasterAdsController>();
            adsController.masterControl = this;
            //purchaser = GetComponentInChildren<Purchaser>();
            //purchaser.masterControl = this;
#if !UNITY_IOS
            StartCoroutine(DoFindBestHost());
#endif
        }
        else
        {
            Destroy(gameObject);
        }
        Application.lowMemory += OnLowMemory;
    }

    private void OnLowMemory()
    {
        System.GC.Collect();
    }

    void Start()
    {
        Init();

        CheckInternet();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Debug.Log("---------------------- FOCUS: CHECK INTERNET ");
            CheckInternet();
            IsShowingAds = false;
        }
    }
    public void Init()
    {
        if (Instance.isInit) return;
        Instance.isInit = true;
        adsController.Init();
        //purchaser.Init();
        IsShowingAds = false;
    }


    public bool CheckAppInstallation(string bundleId)
    {
#if UNITY_EDITOR
        return false;
#elif UNITY_ANDROID
        bool installed = false;
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = curActivity.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
            if (launchIntent == null)
                installed = false;

            else
                installed = true;
        }

        catch (System.Exception e)
        {
            installed = false;
        }
        return installed;

#elif UNITY_IOS
        return false;
#else
        return false;
#endif
    }



    public void CheckInternet()
    {
#if !UNITY_IOS
        CheckInternet(res => { }, false);
#endif
    }
    public void CheckInternet(Action<bool> action, bool useWaitingUI = false)
    {
#if !UNITY_IOS
        StartCoroutine(DoCheckInternet(action, useWaitingUI));
#endif
    }
    [SerializeField]
    private string[] hosts = { "1.1.1.1", "8.8.8.8", "180.76.76.76" };
    [SerializeField]
    private string bestHost = "1.1.1.1";
    IEnumerator DoFindBestHost()
    {
        int pingTime = int.MaxValue;
        for (int i = 0; i < hosts.Length; i++)
        {
            Ping ping = new Ping(hosts[i]);

            float timeOutCooldown = 0;
            while (!ping.isDone && timeOutCooldown < 5)
            {
                yield return null;
                timeOutCooldown += Time.unscaledDeltaTime;
            }

            if (ping.isDone && pingTime > ping.time)
            {
                pingTime = ping.time;
                bestHost = hosts[i];
            }
        }
        Debug.Log("Ping Host: " + bestHost);
    }
    IEnumerator DoCheckInternet(Action<bool> action, bool useWaitingUI = false)
    {
        yield return new WaitForSecondsRealtime(2);
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            isConnected = false;
            action(false);
            yield break;
        }
        Ping ping = new Ping(bestHost);

        float timeOutCooldown = 0;
        bool check = false;
        while (!ping.isDone && timeOutCooldown < 5)
        {
            yield return null;
            timeOutCooldown += Time.unscaledDeltaTime;
            if (useWaitingUI)
            {
                if (!check && timeOutCooldown > 0.3f)
                {
                    check = true;
                    Tung.UIManager.Instance.PleaseWaitPanelOn();
                }
            }
        }
        Debug.Log("IS DONE: " + ping.isDone + " " + ping.time + " " + bestHost);
        if (ping.isDone)
        {
            //isConnected = true;
            StartCoroutine(checkInternetConnection(action, useWaitingUI));
            //action(true);
        }
        else
        {
            isConnected = false;
            action(false);
        }
    }
    public IEnumerator checkInternetConnection(Action<bool> action, bool useWaitingUI)
    {
        WWW www = new WWW("http://google.com");

        bool check = false;
        float timeOut = 5;
        while (!www.isDone && timeOut > 0)
        {
            if (www.bytesDownloaded >= 2)
            {
                break;
            }
            if (!check && useWaitingUI && timeOut < 4.7f)
            {
                check = true;
                Tung.UIManager.Instance.PleaseWaitPanelOn();
            }

            timeOut -= Time.deltaTime;
            yield return null;
        }

        //yield return www;
        Debug.Log("1: " + www.bytesDownloaded);
        if (www.bytesDownloaded < 1)
        {
            WWW www2 = new WWW("https://www.baidu.com/");
            timeOut = 5;
            while (!www2.isDone && timeOut > 0)
            {
                if (www2.bytesDownloaded >= 2)
                {
                    break;
                }

                timeOut -= Time.deltaTime;
                yield return null;
            }
            Debug.Log("2 : " + www2.bytesDownloaded);

            if (www2.bytesDownloaded < 1)
            {
                WWW www3 = new WWW("https://www.baidu.com/");
                yield return www3;
                Debug.Log("3 : " + www3.bytesDownloaded);

                if (www3.bytesDownloaded < 1)
                {
                    WWW www4 = new WWW("http://worldclockapi.com/api/json/utc/now");
                    yield return www4;
                    Debug.Log("4 : " + www4.bytesDownloaded);

                    if (www4.bytesDownloaded < 1)
                    {
                        action(false);
                    }
                    else
                    {
                        action(true);
                    }
                }
                else
                {
                    action(true);
                }
            }
            else
            {
                action(true);
            }
        }
        else
        {
            action(true);
        }
    }
    #region IAP
    public string[] productKeys = {
        "herorescue_remove_ads",
        "herorescue_unlimited"

    };
    private string[] priceTemplates = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    private string[] prices = { "2.99$", "4.99$", "0.99$", "0.99$", "0.99$", "1.99$", "2.99$", "4.99$", "7.99$", "3.99$", "9.99$", };

    public void OnPurchased(UnityEngine.Purchasing.Product product)
    {
        OnPurchased(product.definition.id);
        Debug.Log("PURCHASED: " + product.definition.id);
    }
    public void OnFailedToPurchase(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason reason)
    {
        //Debug.Log("FAILED TO PURCHASED: " + product.definition.id + " " + reason.ToString());

        //if (reason.Equals(UnityEngine.Purchasing.PurchaseFailureReason.DuplicateTransaction))
        //{
        //    if (product != null)
        //    {
        //        if (product.definition.id.Equals(productKeys[0]) && Purchaser.Instance.HasReceipt(product.definition.id))
        //        {
        //            OnRestore(product.definition.id);
        //        }
        //        if (product.definition.id.Equals(productKeys[1]) && Purchaser.Instance.HasReceipt(product.definition.id))
        //        {
        //            if (PrefInfo.IsBoughtPremium())
        //                OnRestore(product.definition.id);
        //            else
        //                OnPurchased(product.definition.id);
        //        }
        //    }
        //}
        if (reason.Equals(UnityEngine.Purchasing.PurchaseFailureReason.PurchasingUnavailable))
        {
            MessageCallBackPopupPanel.INSTACNE.Active("Purchase failed!\nTry again later.");
        }
    }
    //public void CheckRestore()
    //{
    //    Debug.Log("MasterControl: check restore");
    //    purchaser.CheckRestore();
    //}
    public void OnPurchased(string item)
    {
        Debug.Log("MASTERCONTROLL: ONPURCHASED " + item);
        PlayerPrefs.SetInt("UserIAP", 1);
        //Controller.Instance.pleaseWaitPanel.SetActive(false);
        if (item.Equals(productKeys[0]))
        {
            ShopPanel.Instance.OnPurchaseSuccess(1);
        }
        if (item.Equals(productKeys[1]))
        {
            ShopPanel.Instance.OnPurchaseSuccess(2);
        }
        if (item.Equals(productKeys[2]))
        {
            ShopPanel.Instance.OnPurchaseSuccess(3);
        }
        if (item.Equals(productKeys[3]))
        {
            ShopPanel.Instance.OnPurchaseSuccess(4);
        }
        if (item.Equals(productKeys[4]))
        {
            ShopPanel.Instance.OnPurchaseSuccess(5);
        }
        if (item.Equals(productKeys[5]))
        {
            OutOfCoinPanel.Instance.IAPButtonSuccess();
        }
    }
    public void OnRestore(string item)
    {
        if (item.Equals(productKeys[0]))
        {
            MasterControl.Instance.TurnOffAds();
            SoundManage.Instance.Play_BuyPack();
            MessageCallBackPopupPanel.INSTACNE.SetSpriteAsset(ShopPanel.Instance.spriteAsset);
            MessageCallBackPopupPanel.INSTACNE.Active("Ads have been removed <sprite=2>\nThanks for your purchasing");
            ShopPanel.Instance.CheckPack();
        }
        if (item.Equals(productKeys[1]))
        {
            MasterControl.Instance.TurnOffAds();
            SoundManage.Instance.Play_BuyPack();
            CharacterAndAccessoryManage.Instance.IAPReward();
            CoinUI.Instance.UpdateCoin();
            PrefInfo.BuyPremium();
            MessageCallBackPopupPanel.INSTACNE.SetSpriteAsset(ShopPanel.Instance.spriteAsset);
            MessageCallBackPopupPanel.INSTACNE.Active("You earned:No ads <sprite=2>\nNew skin <sprite=1>\nThanks for your purchasing");
            ShopPanel.Instance.CheckPack();
        }
    }


    public string GetPrice(int id)
    {
        string price = prices[id];
        try
        {
            if (priceTemplates[id] == "0")
            {
                try
                {
                    //priceTemplates[id] = purchaser.GetPrices()[id];
                    price = priceTemplates[id];

                }
                catch
                {

                    priceTemplates[id] = "0";
                    return prices[id];

                }
            }
            else
            {
                price = priceTemplates[id];
            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        return price;

    }

    public decimal GetItemPrice(int id)
    {
        try
        {
            //return purchaser.GetPrice(id);
            return 0;
        }
        catch (Exception)
        {
            return decimal.Parse("0");
        }
    }
    #endregion


    public void OpenURL(string link)
    {
        Application.OpenURL(link);

    }
    public void ToStore()
    {
    }

    #region AD
    public bool IsShowingAds = false;
    private Action<bool> rewardCallBack;
    public void ShowRewardedAd(Action<bool> rewardCallBack)
    {
        this.rewardCallBack = rewardCallBack;
#if UNITY_EDITOR
        OnRewardedAd();
        //return;
#endif
        adsController.ShowRewardedVideo();

    }
    public void OnRewardedAd()
    {
        if (rewardCallBack != null)
        {
            Debug.Log("ON REWARD CALLBACK");
            rewardCallBack(true);
            IsShowingAds = false;
            rewardCallBack = null;
        }
    }

    public void OnFail()
    {
        if (rewardCallBack != null)
        {
            IsShowingAds = false;
            rewardCallBack(false);
        }
    }
    public bool ShowInterstitialAd(/*Panel panel=null*/)
    {
        //interShowSucceedEvent = action;
        bool res = adsController.ShowInterstitial(/*panel*/);
        if (res)
        {
            IsShowingAds = true;
        }
        return res;
    }
    public void HideBanner()
    {
        adsController.HideBanner();
    }
    public void ShowBanner()
    {
        adsController.ShowBanner();
    }
    public void TurnOffAds()
    {
        PrefInfo.SetAd(false);
        HideBanner();
    }


    #endregion





}

