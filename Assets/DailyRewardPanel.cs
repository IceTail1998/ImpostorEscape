using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPanel : BasePanel
{
    public static DailyRewardPanel Instance;
    [SerializeField]
    DailyRewardButton[] listButton;
    [SerializeField]
    GameObject NormalSelectingImage;
    [SerializeField]
    GameObject FinalSelectingImage;
    [SerializeField]
    internal Sprite NormalButtonSprite;
    [SerializeField]
    internal Sprite LockedButtonSprite;
    [SerializeField]
    internal Sprite NormalLastDayButtonSprite;
    [SerializeField]
    internal Sprite LockedLastDayButtonSprite;
    [SerializeField]
    internal Sprite ClaimAvailableSprite;
    [SerializeField]
    internal Sprite ClaiNotAvailableSprite;
    [SerializeField]
    Image ClaimButtonImage;
    [SerializeField]
    Button ClaimButton;
    [SerializeField]
    Image iconImageSkin;
    [SerializeField]
    Transform CoinDay7Pos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Init();
    }
    public override void Active()
    {
        UpdateButton();
        UpdateButtonClaim();
        base.Active();
    }
    public void ButtonClose()
    {
        Deactive();
        GameController.Instance.ContinueGame();
        PlayingPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
    }
    private void Init()
    {
        for (int i = 0; i < listButton.Length; i++)
        {
            DailyRewardButton button = listButton[i];
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() => ClickGetReward(button.id));
        }
    }
    private void UpdateButton()
    {
        bool canGetRw = DailyRewardController.CanGetReward();
        int GetRwCount = DailyRewardController.GetRewardedCount();
        NormalSelectingImage.SetActive(false);
        FinalSelectingImage.SetActive(false);
        for (int i = 0; i < listButton.Length; i++)
        {
            if (i < GetRwCount)
            {
                listButton[i].UpdateData(true, false);
            }
            else if (i == GetRwCount)
            {
                if (canGetRw)
                {
                    listButton[i].UpdateData(false, true);
                    if (i == listButton.Length - 1)
                    {
                        FinalSelectingImage.SetActive(true);
                        FinalSelectingImage.transform.position = listButton[i].transform.position;
                    }
                    else
                    {
                        NormalSelectingImage.SetActive(true);
                        NormalSelectingImage.transform.position = listButton[i].transform.position;
                    }
                }
                else
                {
                    listButton[i].UpdateData(false, false);
                    //if (DailyRewardController.IsGetRewardToday())
                    //{
                    //}
                    //else
                    //{
                    //    listButton[i].UpdateData(true);
                    //}
                }
            }
            else
            {
                listButton[i].UpdateData(false, false);
            }
        }

    }
    public void ButtonClaim()
    {
        if (!DailyRewardController.CanGetReward())
        {
            return;
        }
        int currentIndex = DailyRewardController.GetRewardedCount();
        ClickGetReward(currentIndex);
    }
    public void UpdateButtonClaim()
    {
        if (!DailyRewardController.CanGetReward())
        {
            ClaimButton.interactable = false;
            ClaimButtonImage.sprite = null;
            ClaimButtonImage.sprite = ClaiNotAvailableSprite;
            ClaimButtonImage.SetNativeSize();
        }
        else
        {
            ClaimButton.interactable = true;
            ClaimButtonImage.sprite = null;
            ClaimButtonImage.sprite = ClaimAvailableSprite;
            ClaimButtonImage.SetNativeSize();
        }
    }
    bool justGetRW = false;
    public void ClickGetReward(int id)
    {
        Debug.Log("Click reward " + id);
        if (!DailyRewardController.CanGetReward())
        {
            return;
        }
        int currentIndex = DailyRewardController.GetRewardedCount();
        if (id != currentIndex)
        {
            return;
        }
        else
        {
            DailyRewardController.GetReward();
            justGetRW = true;
        }
        switch (id)
        {
            case 0:
                listButton[0].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[0].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(100);
                break;
            case 1:
                listButton[1].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[1].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(200);
                break;
            case 2:
                listButton[2].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[2].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(300);
                break;
            case 3:
                listButton[3].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[3].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(400);
                break;
            case 4:
                listButton[4].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[4].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(500);
                break;
            case 5:
                listButton[5].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(listButton[5].transform.position, CoinUI.Instance.CoinPos);
                SoundManage.Instance.Play_CoinGain();
                CoinManage.AddGem(600);
                break;
            case 6:
                listButton[6].UpdateData(true, false);
                CoinAddEffectUI.Instance.Show(CoinDay7Pos.position, CoinUI.Instance.CoinPos);
                List<int> listRand = CharacterAndAccessoryManage.Instance.GetListIdSkinLock();
                CoinManage.AddGem(1000);
                if (listRand.Count > 0)
                {
                    int rand = Random.Range(0, listRand.Count);
                    CharacterAndAccessoryManage.Instance.OnPurchaseSkin(listRand[rand]);
                    iconImageSkin.sprite = null;
                    iconImageSkin.sprite = CharacterAndAccessoryManage.Instance.GetSkinData(listRand[rand]).icon;
                    SoundManage.Instance.Play_CoinGain();
                    iconImageSkin.SetNativeSize();
                    Sequence newSec = DOTween.Sequence();
                    newSec.Append(iconImageSkin.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .3f));
                    newSec.Append(iconImageSkin.transform.DOScale(new Vector3(1f, 1f, 1f), .2f));
                }
                else
                {
                    iconImageSkin.sprite = null;
                    iconImageSkin.sprite = CharacterAndAccessoryManage.Instance.GetRandomSkinData().icon;
                    SoundManage.Instance.Play_CoinGain();
                    iconImageSkin.SetNativeSize();
                    Sequence newSec = DOTween.Sequence();
                    newSec.Append(iconImageSkin.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), .3f));
                    newSec.Append(iconImageSkin.transform.DOScale(new Vector3(1f, 1f, 1f), .2f));

                }
                break;
        }
        CoinUI.Instance.UpdateCoin();
        UpdateButton();
        UpdateButtonClaim();
    }
}
