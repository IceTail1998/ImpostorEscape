using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    public static ShopPanel Instance;
    [SerializeField]
    Button buyButton;
    [SerializeField]
    Button adsButton;
    [SerializeField]
    ShopItemButton prefabButtonItem;
    [SerializeField]
    List<ShopItemButton> listItemButton;
    [SerializeField]
    Transform buttonsDad;
    [SerializeField]
    int currentType = 0;
    [SerializeField]
    Sprite LockSpriteItemButton;
    [SerializeField]
    TextMeshProUGUI coinText;
    [SerializeField]
    SelectTypeButton CubeSelectButton;
    [SerializeField]
    SelectTypeButton CharSelectButotn;
    [SerializeField]
    GameObject SelectImage;
    [SerializeField]
    GameObject SelectedImage;
    [SerializeField]
    Transform AdsCoinPos;
    [SerializeField]
    Transform Pack1CoinPos;
    [SerializeField]
    Transform Pack2CoinPos;
    [SerializeField]
    Transform Pack3CoinPos;
    [SerializeField]
    GameObject PremiumBought;
    [SerializeField]
    GameObject NoAdsBought;
    [SerializeField]
    Image premiumButtonImage;
    [SerializeField]
    Image noAdsButtonImage;
    [SerializeField]
    Sprite Sprite_NormalIAP;
    [SerializeField]
    Sprite Sprite_BoughtIAP;
    [SerializeField]
    Button NoAdsButton;
    [SerializeField]
    Button PremiumButton;

    private void Start()
    {
        base.Start_BasePanel();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void Active()
    {
        adsButton.interactable = true;
        CheckPack();
        base.Active();
    }
    public void CheckPack()
    {
        if (PrefInfo.IsBoughtPremium())
        {
            PremiumBought.SetActive(true);
            premiumButtonImage.sprite = null;
            premiumButtonImage.sprite = Sprite_BoughtIAP;
            PremiumButton.interactable = false;

        }
        else
        {
            PremiumBought.SetActive(false);
            premiumButtonImage.sprite = null;
            premiumButtonImage.sprite = Sprite_NormalIAP;
            PremiumButton.interactable = true;
        }
        if (!PrefInfo.IsUsingAd())
        {
            NoAdsBought.SetActive(true);
            noAdsButtonImage.sprite = null;
            noAdsButtonImage.sprite = Sprite_BoughtIAP;
            NoAdsButton.interactable = false;
        }
        else
        {
            NoAdsButton.interactable = true;
            NoAdsBought.SetActive(false);
            noAdsButtonImage.sprite = null;
            noAdsButtonImage.sprite = Sprite_NormalIAP;
        }
    }
    public void ButtonClose()
    {
        //HomePanel.Instance.Active();
        Deactive();
        PlayingPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
        //UIManager.Instance.PreviousUIActive = UIManager.UiActivce.Shop;
        //UIManager.Instance.CurrentUIActive = UIManager.UiActivce.Home;
    }
   
    //public void ButtonBuy()
    //{
    //    if (isRandoming) return;
    //    StartCoroutine(BuyIE());
    //}
    //bool isRandoming = false;
    //private IEnumerator BuyIE()
    //{
    //    // Bat Dau
    //    adsButton.interactable = false;
    //    buyButton.interactable = false;
    //    isRandoming = true;
    //    List<ShopItemButton> listLockButton = new List<ShopItemButton>();
    //    for (int i = 0; i < listItemButton.Count; i++)
    //    {
    //        if (listItemButton[i].gameObject.activeSelf)
    //        {
    //            //if (currentType == 0)
    //            //{
    //            //    if (CharacterAndCubeManage.Instance.IsCubeLock(listItemButton[i].id))
    //            //    {
    //            //        listLockButton.Add(listItemButton[i]);
    //            //    }
    //            //}
    //            //else if (currentType == 1)
    //            //{
    //            //    if (CharacterAndCubeManage.Instance.IsCharacterLock(listItemButton[i].id))
    //            //    {
    //            //        listLockButton.Add(listItemButton[i]);
    //            //    }
    //            //}
    //        }
    //    }
    //    // Co cai nao chua unlock thi check
    //    if (listLockButton.Count > 1)
    //    {
    //        float randTimeCount = 0f;
    //        float timeWait = 0.1f;
    //        int randResult = Random.Range(0, listLockButton.Count);
    //        int randTemp;
    //        while (randTimeCount < 2.5f)
    //        {

    //            randTemp = Random.Range(0, listLockButton.Count);
    //            if (randTemp != randResult)
    //            {
    //                randResult = randTemp;
    //            }
    //            else
    //            {
    //                randResult++;
    //                randResult = Mathf.Clamp(randResult, 0, listLockButton.Count - 1);
    //            }
    //            SelectImage.transform.SetParent(listLockButton[randResult].transform);
    //            SelectImage.transform.localPosition = Vector3.zero;
    //            SelectImage.SetActive(true);
    //            timeWait += 0.05f;
    //            randTimeCount += timeWait;
    //            yield return new WaitForSeconds(timeWait);
    //        }
    //        yield return new WaitForSeconds(.2f);
    //        SelectImage.SetActive(false);
    //        listLockButton[randResult].OnUnlock();
    //        //if (currentType == 0)
    //        //{
    //        //    CharacterAndCubeManage.Instance.OpenMat(listLockButton[randResult].id);
    //        //}
    //        //else if (currentType == 1)
    //        //{
    //        //    CharacterAndCubeManage.Instance.OpenCharacter(listLockButton[randResult].id);
    //        //}
    //    }
    //    else if (listLockButton.Count == 1)
    //    {
    //        //if (currentType == 0)
    //        //{
    //        //    CharacterAndCubeManage.Instance.OpenMat(listLockButton[0].id);
    //        //}
    //        //else if (currentType == 1)
    //        //{
    //        //    CharacterAndCubeManage.Instance.OpenCharacter(listLockButton[0].id);
    //        //}
    //        listLockButton[0].OnUnlock();
    //    }
    //    adsButton.interactable = true;
    //    buyButton.interactable = true;
    //    isRandoming = false;
    //}
    [SerializeField]
    internal TMP_SpriteAsset spriteAsset;
    public void OnPurchaseSuccess(int pack)
    {
        switch (pack)
        {
            case 3:
                CoinManage.AddGem(2000);
                CoinUI.Instance.UpdateCoin();
                CoinAddEffectUI.Instance.Show(Pack1CoinPos.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                break;
            case 4:
                CoinManage.AddGem(7000);
                CoinUI.Instance.UpdateCoin();
                CoinAddEffectUI.Instance.Show(Pack2CoinPos.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                break;
            case 5:
                CoinManage.AddGem(13000);
                CoinUI.Instance.UpdateCoin();
                CoinAddEffectUI.Instance.Show(Pack3CoinPos.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                break;
            case 1:
                SoundManage.Instance.Play_BuyPack();
                MessageCallBackPopupPanel.INSTACNE.SetSpriteAsset(spriteAsset);
                MessageCallBackPopupPanel.INSTACNE.Active("Ads have been removed <sprite=2>\nThanks for your purchasing");
                CheckPack();
                break;
            case 2:
                SoundManage.Instance.Play_BuyPack();
                CoinManage.AddGem(10000);
                CharacterAndAccessoryManage.Instance.IAPReward();
                CoinUI.Instance.UpdateCoin();
                PrefInfo.BuyPremium();
                MessageCallBackPopupPanel.INSTACNE.SetSpriteAsset(spriteAsset);
                MessageCallBackPopupPanel.INSTACNE.Active("You earned:No ads <sprite=2>\n+10000 <sprite=0>\nNew skin <sprite=1>\nThanks for your purchasing");
                CheckPack();
                break;
            default:
                break;
        }
    }
    //public void OnPurchaseFail(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason reason)
    //{
    //    MasterControl.Instance.OnFailedToPurchase(product, reason);
    //}
}
