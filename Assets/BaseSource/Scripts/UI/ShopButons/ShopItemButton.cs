using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public int id;
    [SerializeField]
    Image img;
    [SerializeField]
    GameObject cover;
    [SerializeField]
    GameObject AdsButton;
    [SerializeField]
    GameObject BuyButton;
    [SerializeField]
    GameObject UseButton;
    int price = 0;
    [SerializeField]
    TextMeshProUGUI priceText;
    [SerializeField]
    GameObject SelectedObject;
    [SerializeField]
    GameObject EffectObject;
    Image buttonImage;
    Button thisButton;
    ShopItemPack packDad;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }
    public void OnUnlock()
    {

    }
    public void Setup(int id, Sprite spr)
    {
        this.id = id;
        img.sprite = null;
        img.sprite = spr;
        img.SetNativeSize();
    }
    public void UpdateState(bool isOpenned)
    {
        SelectedObject.SetActive(false);
        if (isOpenned)
        {
            UseButton.SetActive(true);
            //AdsButton.SetActive(false);
            BuyButton.SetActive(false);
        }
        else
        {
            UseButton.SetActive(false);
            //AdsButton.SetActive(true);
            BuyButton.SetActive(true);
        }
    }
    public void SelectItem()
    {
        UseButton.SetActive(false);
        //AdsButton.SetActive(false);
        BuyButton.SetActive(false);
        SelectedObject.SetActive(true);
    }
    public void UpdateSprite(Sprite spr)
    {
        if (buttonImage == null) return;
        buttonImage.sprite = null;
        buttonImage.sprite = spr;
        buttonImage.SetNativeSize();
    }
    public void UpdateData(SkinData data, ShopItemPack pack)
    {
        packDad = pack;
        id = data.id;
        img.sprite = null;
        price = data.price;
        img.sprite = data.icon;
        img.SetNativeSize();
        priceText.text = "<sprite=0> " + data.price;
        if (ShopSkinPanel.Instance.currentType == 1)
        {
            if (id == CharacterAndAccessoryManage.Instance.currentSkinId)
            {
                SelectItem();
            }
            else
            {
                if (CharacterAndAccessoryManage.Instance.IsSkinOpenned(data.id))
                {
                    UpdateState(true);
                }
                else
                {
                    UpdateState(false);
                }
            }
        }
        else if (ShopSkinPanel.Instance.currentType == 2)
        {
            if (CharacterAndAccessoryManage.Instance.IsPetOpenned(data.id))
            {
                UpdateState(true);
            }
            else
            {
                UpdateState(false);
            }
        }
        BuyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        BuyButton.GetComponent<Button>().onClick.AddListener(() => CheckBuyItem());
        UseButton.GetComponent<Button>().onClick.RemoveAllListeners();
        UseButton.GetComponent<Button>().onClick.AddListener(() => CheckSelect());
        if (thisButton == null)
        {
            thisButton = GetComponent<Button>();
        }
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(() => CheckShowPreview());
    }
    public void CheckBuyItem()
    {
        int coin = CoinManage.GetGem();
        if (coin < price)
        {
            //MessageCallBackPopupPanel.INSTACNE.Active("Oops! Not enough coin.");
            OutOfCoinPanel.Instance.Active();
            SoundManage.Instance.Play_ButtonClick();
        }
        else
        {

            ShopSkinPanel.Instance.BuyItem(id, price);
            SoundManage.Instance.Play_UnlockSkin();
            EffectOpen();
            CoinManage.AddGem(-1 * price);
            CoinUI.Instance.UpdateCoin();
        }
    }
    private void CheckShowPreview()
    {
        packDad.SelectButton(id);
        if (ShopSkinPanel.Instance.currentType == 1)
        {
            CharacterAndAccessoryManage.Instance.PreviewSkin(id);
        }
        else if (ShopSkinPanel.Instance.currentType == 2)
        {
            CharacterAndAccessoryManage.Instance.PreviewPet(id);
        }
        SoundManage.Instance.Play_ButtonClick();
    }
    private void CheckSelect()
    {
        EffectOpen();
        if (ShopSkinPanel.Instance.currentType == 1)
        {
            CharacterAndAccessoryManage.Instance.SelectSkin(id);
        }
        else if (ShopSkinPanel.Instance.currentType == 2)
        {
            CharacterAndAccessoryManage.Instance.SelectPet(id);
        }
        else
        {

        }
        SoundManage.Instance.Play_ButtonClick();
        ShopSkinPanel.Instance.UpdateShopUI();
    }
    private void EffectOpen()
    {

        EffectObject.SetActive(true);
        Sequence newSec = DOTween.Sequence();
        newSec.Append(img.transform.parent.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .3f));
        newSec.Append(img.transform.parent.DOScale(new Vector3(1f, 1f, 1f), .2f));
        Invoke("OffEffect", .55f);
    }
    private void OffEffect()
    {
        EffectObject.SetActive(false);
    }
    private void EffectSelect()
    {

    }
    public void SetSprite(Sprite spr)
    {
        img.sprite = null;
        img.sprite = spr;
        img.SetNativeSize();
    }
}
