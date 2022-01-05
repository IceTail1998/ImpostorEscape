using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinPanel : BasePanel
{
    public static ShopSkinPanel Instance;
    [SerializeField]
    Sprite Sprite_NormalType;
    [SerializeField]
    Sprite Sprite_SelectedType;
    [SerializeField]
    SwipeShopComponent swipeComponent;
    [SerializeField]
    Image[] listSelectTypeButtonImage;
    [SerializeField]
    GameObject SkinCam;
    Transform rotatePreviewDad;
    public int currentType { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        base.Start_BasePanel();
        currentType = 1;
        rotatePreviewDad = SkinCam.transform.GetChild(0);
    }
    public override void Active()
    {
        SkinCam.SetActive(true);
        base.Active();
        swipeComponent.Setup();
        SoundManage.Instance.PlayDanceMusic();
    }
    public override void Deactive()
    {
        CharacterAndAccessoryManage.Instance.CheckSkinAndPet();
        base.Deactive();
        SkinCam.SetActive(false);

    }
    public void ButtonClose()
    {
        Deactive();
        PlayingPanel.Instance.ActiveHome();
        SoundManage.Instance.Play_ButtonClick();
        SoundManage.Instance.Play_HomeMusic();
        GameController.Instance.ContinueGame();
    }
    public void UpdateShopUI()
    {
        swipeComponent.Setup();
    }
    public void SelectType(int type)
    {
        if (type != currentType)
        {
            listSelectTypeButtonImage[currentType - 1].sprite = null;
            listSelectTypeButtonImage[currentType - 1].sprite = Sprite_NormalType;
            currentType = type;
            listSelectTypeButtonImage[currentType - 1].sprite = null;
            listSelectTypeButtonImage[currentType - 1].sprite = Sprite_SelectedType;
            if (currentType == 1)
            {
                swipeComponent.HideComingSoon();
                swipeComponent.Setup();
            }
            else
            {
                swipeComponent.ShowComingSoon();
            }
        }
        SoundManage.Instance.Play_ButtonClick();
    }
    public void BuyItem(int id, int price)
    {
        SoundManage.Instance.Play_UnlockSkin();
        if (currentType == 1)
        {
            CharacterAndAccessoryManage.Instance.OnPurchaseSkin(id);
        }
        else if (currentType == 2)
        {
            CharacterAndAccessoryManage.Instance.OnPurchasePet(id);
        }
        UpdateShopUI();
    }

}
