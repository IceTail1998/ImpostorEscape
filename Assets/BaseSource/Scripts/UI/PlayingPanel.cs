using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanel : BasePanel
{
    public static PlayingPanel Instance;
    //[SerializeField]
    //private Image progressFillImg;
    //[SerializeField]
    //Transform coinPosTrans;
    [SerializeField]
    Sprite NodeLockSprite;
    [SerializeField]
    Sprite NodePassedSprite;
    [SerializeField]
    Sprite NodeCurrentSprite;
    [SerializeField]
    GameObject[] taptoplay;
    [SerializeField]
    GameObject ShopButton;
    [SerializeField]
    Animator SkipButonAnimator;
    [SerializeField]
    Node_Progress[] listProgressLevelNode;
    //[SerializeField]
    //Transform selectingLevelTransform;
    //[SerializeField]
    //Image fillProgressLevel;

    [SerializeField]
    GameObject GiftIcon;
    [SerializeField]
    GameObject GiftIconOff;
    [SerializeField]
    Taptoplay tapToPlayCheck;
    [SerializeField]
    GameObject notificationDailyRW;
    [SerializeField]
    GameObject notificationShopSkin;
    [SerializeField]
    DynamicJoystick joystick;
    //public Vector3 CoinPos { get { return coinPosTrans.position; } }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        base.Start_BasePanel();
        //UpdateNode();
        //UpdateCoinText();
        //UpdateButtonHint();
        //UpdateUpgradeButton();
        UpdateNode();
        CoinUI.Instance.TurnOnButton();
        //CheckShowNotifications();
        //ActiveHome();
    }
    public override void Active()
    {
        //CheckShowNotifications();
        //HomePart.SetActive(false);
        //GamePlayPart.SetActive(true);
        //UpdateButtonHint();
        //UpdateCoinText();
        UpdateNode();
        CoinUI.Instance.TurnOnButton();
        //UpdateLevelText();
        //Tung.UIManager.Instance.SetPlayOnTop(true);
        tapToPlayCheck.CheckShowButton();
        base.Active();
    }
    public void ActiveHome()
    {
        UpdateNode();
        for (int i = 0; i < taptoplay.Length; i++)
        {
            taptoplay[i].SetActive(true);
        }
        //taptoplay[0].SetActive(true);
        CoinUI.Instance.TurnOnButton();
        tapToPlayCheck.CheckShowButton();
        joystick.TurnOffStart();
        base.Active();
    }
    //public void CheckShowNotifications()
    //{
    //    if (DailyRewardController.CanGetReward())
    //    {
    //        notificationDailyRW.SetActive(true);
    //    }
    //    else
    //    {
    //        notificationDailyRW.SetActive(false);
    //    }
    //    int coin = CoinManage.GetGem();
    //    int lowestPrice = CharacterAndAccessoryManage.Instance.GetLowestPrice();
    //    if (coin >= lowestPrice)
    //    {
    //        notificationShopSkin.SetActive(true);
    //    }
    //    else
    //    {
    //        notificationShopSkin.SetActive(false);
    //    }
    //}
    public override void Deactive()
    {
        //Tung.UIManager.Instance.SetPlayOnTop(false);
        base.Deactive();
        CoinUI.Instance.TurnOffButton();

    }
    public void UpdateLevelText()
    {
        UpdateNode();
        //if (levelText != null)
        //{
        //    levelText.text = "LEVEL " + LevelManage.Instance.currentLevel;
        //}
    }

    public void UpdateNode()
    {
        int cur = LevelManage.Instance.currentLevel;
        cur = Mathf.Clamp(cur, 1, LevelManage.Instance.numberOfLevels[0]);

        int max = 0;
        if (cur % 5 > 0)
        {
            max = 5 * ((cur / 5) + 1);
        }
        else
        {
            max = 5 * (cur / 5);
        }
        max = Mathf.Clamp(max, 1, LevelManage.Instance.numberOfLevels[0]);
        int curIndex = 2;
        if (cur % 5 < 3 && cur % 5 > 0)
        {
            curIndex = cur % 5 - 1;
        }
        else if (cur % 5 == 0)
        {
            curIndex = 4;
        }
        if (cur % 5 >= 3)
        {
            curIndex = 4 - (5 - cur % 5);
        }
        if (cur == max - 2)
        {
            curIndex = 2;
        }
        if (cur == max - 1)
        {
            curIndex = 3;
        }
        if (cur == max)
        {
            curIndex = 4;
        }
        for (int i = 0; i < listProgressLevelNode.Length; i++)
        {
            if (i < curIndex)
            {
                listProgressLevelNode[i].UpdateNodeData(String.Empty + (cur - curIndex + i), NodePassedSprite);
            }
            else if (i == curIndex)
            {
                listProgressLevelNode[i].UpdateNodeData(String.Empty + cur, NodeCurrentSprite);
            }
            else
            {
                listProgressLevelNode[i].UpdateNodeData(String.Empty + (cur + i - curIndex), NodeLockSprite);
            }
        }
        //fillProgressLevel.fillAmount = (float)cur / 5;
        if (ProgressComponent.Instance != null)
        {
            int val = ProgressComponent.Instance.GetProgressValue();
            int tol = ProgressComponent.Instance.GetProgressRequire();
            if (tol - val <= 5 && max % 5 == 0)
            {
                GiftIcon.SetActive(true);
                GiftIconOff.SetActive(false);
            }
            else
            {
                GiftIcon.SetActive(false);
                GiftIconOff.SetActive(true);
            }
        }
        else
        {
            GiftIcon.SetActive(true);
            GiftIconOff.SetActive(false);
        }
    }
    public void ButtonSetting()
    {
        Deactive();
        SoundManage.Instance.Play_ButtonClick();
        SettingPanel.Instance.Active();
        GameController.Instance.PauseGame();
    }
    public void ButtonDailyReward()
    {
        Deactive();
        GameController.Instance.PauseGame();
        DailyRewardPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
    }
    public void ButtunSkip()
    {
        SkipAction();
    }
    public void SkipButtonAnimationOn()
    {
        //SkipButonAnimator?.Play("loop");
    }
    public void SkipButtonAnimationOff()
    {
        //SkipButonAnimator?.Play("idle");
    }
    private void SkipAction()
    {
        //HintUIManage.Instance.OffHint();
        SoundManage.Instance.Play_ButtonClick();
        LevelManage.Instance.SkipCurrentLevel();
        LevelManage.Instance.LoadCurrentLevel();
    }
    public void ButtonShopCoin()
    {
        if (!IsActive) return;
        Deactive();
        ShopPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
    }
    public void ButtonShopSkin()
    {
        Deactive();
        ShopSkinPanel.Instance.Active();
        GameController.Instance.PauseGame();
        SoundManage.Instance.Play_ButtonClick();
    }
    public void ButtonPause()
    {
        //PausePanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
        Deactive();
        //UIManager.Instance.PreviousUIActive = UIManager.UiActivce.Playing;
        //UIManager.Instance.CurrentUIActive = UIManager.UiActivce.Pause;
    }

    public void ButtonQuit()
    {
        SoundManage.Instance.Play_ButtonClick();
        //LevelManage.Instance.QuitLevel();
        //GameController.Instance.QuitLevel();
    }
    public void ButtonPLay()
    {
        Active();
        LevelManage.Instance.currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
        SoundManage.Instance.Play_ButtonClick();
    }
    public void ButtonSelectLevel()
    {
        Deactive();
        SelectLevelPanel.instance.Active();
        GameController.Instance.PauseGame();
        SoundManage.Instance.Play_ButtonClick();
    }

    public void ButtonNoAdsFail()
    {
        MessageCallBackPopupPanel.INSTACNE.Active("Purchase failed!");
    }
    //public void ButtonHome()
    //{
    //    if (isClickHome) return;
    //    isClickHome = true;
    //    //HintUIManage.Instance.OffHint();
    //    LevelManage.Instance.LoadCurrentLevel();
    //    //ActiveHome();
    //    SoundManage.Instance.Play_ButtonClick();
    //    //if (GameController.Instance.IsPlaying)
    //    //{
    //    //    MasterControl.Instance.ShowInterstitialAd();
    //    //    BlackScreenEffect.instance.On(timeBlackScreen, ToHomeAction);
    //    //    SoundManage.Instance.Play_ClickClose();
    //    //}
    //}
    public void ButtonTapToPlay()
    {
        //Player.Instance.TurnOnSplineFolower();
        //ShopButton.SetActive(false);
        //CameraParent.Instance.TurnOnSplimeFolower();
        for (int i = 0; i < taptoplay.Length; i++)
        {
            taptoplay[i].SetActive(false);
        }
        SoundManage.Instance.Play_GameMusic();
        //GameManager.instance.StartGame();
    }
    private void ToHomeAction()
    {
        //SpawnerV2.Instance.OffAll();
        //HomePanel.Instance.Active();
        //UIManager.Instance.OnChangeToUI();
        //Deactive();
        //UIManager.Instance.PreviousUIActive = UIManager.UiActivce.Playing;
        //UIManager.Instance.CurrentUIActive = UIManager.UiActivce.Home;
        //LevelManage.Instance.DestroyCurrentLevel();

    }
    public void ButtonReplay()
    {
        //MasterControl.Instance.ShowInterstitialAd();
        SoundManage.Instance.Play_ButtonClick();
        //HintUIManage.Instance.OffHint();
        LevelManage.Instance.Replay();
        //GameController.Instance.Replay();
        //SoundManage.Instance.Play_ButtonClick();
    }
    public void ButtonPreviousLevel()
    {
        LevelManage.Instance.LoadPreviousLevel();
    }
    public void UpdateButtonHint()
    {
        //hintCountText.text = String.Empty + HintManage.GetHint();
    }
    public void ButtonHint()
    {
        //if (HintUIManage.Instance.IsShowing) return;
        //SoundManage.Instance.Play_ButtonClick();
        //SkipButtonAnimationOff();
        //if (HintManage.GetHint() > 0)
        //{
        //    FireBaseManager.Instance.LogEvent("Hint_Level_", LevelManage.Instance.currentLevel);
        //    HintManage.AddHint(-1);
        //    HintAction();
        //    UpdateButtonHint();
        //}
        //else
        //{
        //    Debug.Log("Out of hint show!");
        //    OutOfHintPanel.Instance.Active();
        //}
    }
    public void ButtonCollection()
    {
        Deactive();
        LevelManage.Instance.currentLevelMap.GetComponent<LevelMap>().TurnOffLevel();
        //CollectionPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
    }

}
