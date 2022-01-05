using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftPanel : BasePanel
{
    public static GiftPanel Instance;
    [SerializeField]
    GameObject GetGiftFreeButton;
    [SerializeField]
    GameObject GetGiftAdsButton;
    [SerializeField]
    Image GiftImage;
    //[SerializeField]
    //Animator GiftBoxAnimator;
    [SerializeField]
    Sprite GiftCoinSprite;
    [SerializeField]
    GameObject CoinRewardOb;
    [SerializeField]
    TextMeshProUGUI textButtonNext;
    [SerializeField]
    GameObject[] listSkinPreview;
    int idSkinToOpen = -1;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public override void Active()
    {
        UpdateRandomSkin();
        base.Active();
        isClicked = false;
        textButtonNext.text = "No thanks!";
        if (!ProgressComponent.Instance.DidGetFirstReward())
        {
            GetGiftAdsButton.SetActive(false);
            GetGiftFreeButton.SetActive(true);
        }
        else
        {
            GetGiftAdsButton.SetActive(true);
            GetGiftFreeButton.SetActive(false);
        }
        ProgressComponent.Instance.OnProgressComplete();
        SoundManage.Instance.PlayDanceMusic();
    }
    private void UpdateRandomSkin()
    {
        List<int> listRand = CharacterAndAccessoryManage.Instance.GetListIdSkinLock();
        if (listRand.Count > 0)
        {
            int rand = Random.Range(0, listRand.Count);
            idSkinToOpen = listRand[rand];
            CharacterAndAccessoryManage.Instance.PreviewSkin(idSkinToOpen);
            CoinRewardOb.SetActive(false);
            for (int i = 0; i < listSkinPreview.Length; i++)
            {
                listSkinPreview[i].SetActive(true);
            }
        }
        else
        {
            GiftImage.sprite = null;
            GiftImage.sprite = GiftCoinSprite;
            GiftImage.SetNativeSize();
            CoinRewardOb.SetActive(true);
            for (int i = 0; i < listSkinPreview.Length; i++)
            {
                listSkinPreview[i].SetActive(false);
            }
        }
    }
    public void ButtonAds()
    {
        SoundManage.Instance.Play_ButtonClick();
        //MasterControl.Instance.ShowRewardedAd(r =>
        //{
        //    if (r)
        //    {
        //        if (FirstOpenController.instance.IsOpenFirst)
        //        {
        //            FireBaseManager.Instance.LogEvent("REWARDED_FIRST_SESSION ");
        //        }
        //        FireBaseManager.Instance.LogEvent("REWARDED_GIFT_ENDGAME");
        //        GetButtonAction();
        //    }
        //});
    }
    public void GetButtonAction()
    {
        GetGiftAdsButton.SetActive(false);
        GetGiftFreeButton.SetActive(false);
        textButtonNext.text = "Close";
        ProgressComponent.Instance.OnProgressComplete();
        if (idSkinToOpen != -1)
        {
            CharacterAndAccessoryManage.Instance.OnPurchaseSkin(idSkinToOpen);
            SoundManage.Instance.Play_UnlockSkin();
            Sequence newSec = DOTween.Sequence();
            newSec.Append(listSkinPreview[0].transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .3f));
            newSec.Append(listSkinPreview[0].transform.DOScale(new Vector3(1f, 1f, 1f), .2f));
        }
        else
        {
            CoinManage.AddGem(1000);
            CoinAddEffectUI.Instance.Show(CoinRewardOb.transform.position, CoinUI.Instance.CoinPos);
            CoinUI.Instance.UpdateCoin();
            SoundManage.Instance.Play_CoinGain();
        }
        //GiftBoxAnimator.Play("open");
    }
    bool isClicked = false;
    public void ButtonNext()
    {
        if (!isClicked)
        {
            ProgressComponent.Instance.OnProgressComplete();
            isClicked = true;
            EndGamePanel.Instance.Active_Edited(true);
            //MasterControl.Instance.ShowInterstitialAd();
            //GameController.Instance.StopRotateObject();
            //SoundManage.Instance.StopFireWork();
            //if (LevelManage.Instance.IsCompleteAllLevel)
            //{
            //    LevelManage.Instance.LoadRandomLevel();
            //}
            //else
            //{
            //    LevelManage.Instance.LoadCurrentLevel();
            //}
            //SoundManage.Instance.Play_ButtonClick();
            Deactive();
            //PlayingPanel.Instance.ActiveHome();
            SoundManage.Instance.Play_HomeMusic();
            //EffectManage.Instance.TurnOffExplore();
        }
    }
}
