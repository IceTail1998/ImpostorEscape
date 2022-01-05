using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : BasePanel
{
    public static EndGamePanel Instance;
    [SerializeField]
    GameObject WinObject;
    [SerializeField]
    GameObject LoseObject;
    bool isWinz;
    [SerializeField]
    float timeBlackScreen = 1f;
    [SerializeField]
    Image fillChestProgress;
    [SerializeField]
    TextMeshProUGUI progressText;
    [SerializeField]
    Transform coinPosTrans;
    [SerializeField]
    TextMeshProUGUI coinRewardText;
    [SerializeField]
    TextMeshProUGUI coinGetText;
    [SerializeField]
    GameObject X3CoinButton;
    [SerializeField]
    Transform CoinStartPos;
    [SerializeField]
    Transform CoinAdsStartPos;
    [SerializeField]
    TextMeshProUGUI mulTxt;
    [SerializeField]
    TextMeshProUGUI levelText;
    [SerializeField]
    TextMeshProUGUI levelText1;
    [SerializeField]
    GameObject ADSButton;
    [SerializeField]
    GameObject NextButton;
    [SerializeField]
    GameObject ReplayButton;
    [SerializeField]
    TextMeshProUGUI nextButtonText;
    [SerializeField]
    Animator giftBoxAnimator;
    [SerializeField]
    GameObject SkipProgressButton;
    [SerializeField]
    TextMeshProUGUI titleText;
    [SerializeField]
    [TextArea]
    List<string> listTitle;
    [SerializeField]
    GameObject LoseFloatingObject;
    [SerializeField]
    GameObject Ground;
    [SerializeField]
    Animator animDoor;
    Transform rotatePlayerObject;
    [SerializeField]
    AdsButton Button_ads;

    bool isClicked = false;
    public Vector3 CoinPos { get { return coinPosTrans.position; } }
    float blackScreenTime = 1.5f;
    int reward = 50;

    public override void Active()
    {
        //ADSButton?.SetActive(true);
        //AddCoinEffectAnimator?.Play("idle");
        //SkipProgressButton?.SetActive(true);
        //if (levelText != null)
        //{
        //    levelText.text = PlayingPanel.Instance.GetLevelText();
        //}
        //UpdateData();
        base.Active();
        //Tung.UIManager.Instance.SetPlayOnTop(false);
        isClicked = false;
    }
    public override void Deactive()
    {
        animDoor.Play("out");
        SoundManage.Instance.Play_HomeMusic();
        base.Deactive();
    }
    private IEnumerator DeactiveIE()
    {
        yield return new WaitForSeconds(5f / 6);
        base.Deactive();
    }
    public bool CheckShowRate()
    {
        if (FirstOpenController.DidPlayerRate()) return false;
        int levelRate = PlayerPrefs.GetInt("level_show_rate");
        Debug.Log("Check show rate: " + levelRate + ", current: " + LevelManage.Instance.currentLevel);
        if (LevelManage.Instance.currentLevel - 1 == levelRate)
        {
#if UNITY_ANDROID || UNITY_EDITOR
            RatePanel.Instance.Active();
            PlayerPrefs.SetInt("Show_Rate_" + levelRate, 1);
#elif UNITY_IOS
                StartCoroutine(WaitShowRateIOS(levelRate));
#endif
            return true;
        }
        return false;
    }
    internal void ShowNextButton()
    {
        NextButton.gameObject.SetActive(true);
    }

    internal void ShowReplayButton()
    {
        ReplayButton.SetActive(true);
        SoundManage.Instance.Play_LoseGameMusic();
        Player.Instance.SetupLose(rotatePlayerObject);
        Ground.SetActive(false);
        LoseFloatingObject.SetActive(true);
        LevelManage.Instance.DestroyCurrentLevel();

    }

    public void ShowNextRandomButton()
    {
        //nextButtonText.text = "NEXT RANDOM";
    }
    public void Active_Edited(bool isWin)
    {
        Button_ads.SetAvalable(true);
        isWinz = isWin;
        isClicked = false;
        ReplayButton.SetActive(false);
        NextButton.SetActive(false);
        UpdateData();
        //giftBoxAnimator.Play("idle");

        if (anim != null)
        {
            if (isWin)
            {
                anim.Play("inWin");
                StartCoroutine(ActiveIE());
            }
            else
            {
                anim.Play("inLose");
            }
        }
        else
        {
            if (panelObj == null)
            {
                panelObj = transform.GetChild(0).gameObject;
            }
            panelObj.SetActive(true);
            if (isWin)
            {
                //UpdateData();
                WinObject.SetActive(true);
                LoseObject.SetActive(false);
                StartCoroutine(ActiveIE());
            }
            else
            {

                WinObject.SetActive(false);
                LoseObject.SetActive(true);
            }
        }
    }
    IEnumerator ActiveIE()
    {
        yield return new WaitForSeconds(1f);
        CoinAddEffectUI.Instance.Show(CoinStartPos.position, coinPosTrans.position);
        CoinManage.AddGem(reward);
        SoundManage.Instance.Play_CoinGain();
        CoinUI.Instance.UpdateCoin();
        yield return new WaitForSeconds(1f);
        NextButton.SetActive(true);
    }
    private void UpdateData()
    {
        ADSButton.SetActive(true);
        if (LevelManage.Instance.isLastLevelPassedBeforePlay)
        {
            coinGetText.text = "<sprite=0> 5";
            //coinRewardText.text = "<size=81>Get <br><size=67>15 <sprite=0>";
            reward = 5;
        }
        else
        {
            coinGetText.text = "<sprite=0> 50";
            //coinRewardText.text = "<size=81>Get <br><size=67>150 <sprite=0>";
            reward = 50;
            //if (Random.value <= 0.2f)
            //{
            //}
            //else
            //{
            //    coinRewardText.text = "<size=81>Get <size=67>100 <sprite=0>";
            //    reward = 100;
            //}
        }
        levelText.text = "LEVEL " + LevelManage.Instance.currentLevel;
        levelText1.text = "LEVEL " + LevelManage.Instance.currentLevel;
        //UpdateProgress();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UpdateProgressDeactive();
        base.Start_BasePanel();
        rotatePlayerObject = LoseFloatingObject.transform.GetChild(1);
        reward = 50;
    }
    public void ButtonGetCoin()
    {
        SoundManage.Instance.Play_ButtonClick();
       
    }
    public void ButtonNext()
    {
        if (!isClicked)
        {
            if (EndGamePanel.Instance.CheckShowRate())
            {

            }
            else
            {
                isClicked = true;
                GameController.Instance.StopRotateObject();
                SoundManage.Instance.StopFireWork();
                //if (LevelManage.Instance.IsCompleteAllLevel)
                //{
                //    LevelManage.Instance.LoadRandomLevel();
                //}
                //else
                //{
                //}

                LevelManage.Instance.LoadCurrentLevel();
                SoundManage.Instance.Play_ButtonClick();
                Deactive();
                PlayingPanel.Instance.ActiveHome();
            }

            //EffectManage.Instance.TurnOffExplore();
        }
    }
    public void AfterRate()
    {
        isClicked = true;
        GameController.Instance.StopRotateObject();
        SoundManage.Instance.StopFireWork();
        //if (LevelManage.Instance.IsCompleteAllLevel)
        //{
        //    LevelManage.Instance.LoadRandomLevel();
        //}
        //else
        //{
        //}

        LevelManage.Instance.LoadCurrentLevel();
        SoundManage.Instance.Play_ButtonClick();
        Deactive();
        PlayingPanel.Instance.ActiveHome();
    }
    private IEnumerator NextIE()
    {
        yield return new WaitForSeconds(.28f);
        LevelManage.Instance.LoadCurrentLevel();
    }
    public void ButtonGetFreeHint()
    {
        //MasterControl.Instance.ShowRewardedAd(r =>
        //   {
        //       if (r)
        //       {
        //           if (FirstOpenController.instance.IsOpenFirst)
        //           {
        //               FireBaseManager.Instance.LogEvent("REWARDED_FIRST_SESSION ");
        //           }
        //           FireBaseManager.Instance.LogEvent("REWARDED_HINT_ENDGAME");
        //           HintManage.AddHint(1);
        //           PlayingPanel.Instance.UpdateButtonHint();
        //           ADSButton.SetActive(false);
        //       }
        //   });
    }
    public void ButtonSkipProgress()
    {
        //    MasterControl.Instance.ShowRewardedAd(r =>
        //{
        //    if (r)
        //    {
        //        if (FirstOpenController.instance.IsOpenFirst)
        //        {
        //            FireBaseManager.Instance.LogEvent("REWARDED_FIRST_SESSION ");
        //        }
        //        FireBaseManager.Instance.LogEvent("REWARDED_OPENNOW_ENDGAME");
        //        SkipProgressButton.SetActive(false);
        //        GainGiftBoxAction();
        //    }
        //});

    }
    private void SkipProgressAction()
    {
        //Deactive_Edited();
        //FireBaseManager.Instance.LogEvent("LEVEL_SKIP_", LevelManage.Instance.currentLevel);
        //UIManager.Instance.PreviousUIActive = UIManager.UiActivce.EndGame;
        //UIManager.Instance.CurrentUIActive = UIManager.UiActivce.Playing;
        //LevelManage.Instance.PlayNextLevel();
        //PlayingPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
    }
    public void UpdateProgressDeactive()
    {
        //float progress = ProgressComponent.Instance.GetProgress();
        //fillChestProgress.fillAmount = progress;
        //if (progress >= 1.0f)
        //{
        //    SkipProgressButton.SetActive(false);
        //}
    }
    public void UpdateProgress()
    {
        StartCoroutine(UpdateProgressIE());
    }
    public void ButtonGainGiftBox()
    {
        //float progress = ProgressComponent.Instance.GetProgress();
        //if (progress >= 1.0f)
        //{
        //    GainGiftBoxAction();
        //    SoundManage.Instance.Play_GainHint();
        //    giftBoxAnimator.Play("in");
        //    SkipProgressButton?.SetActive(false);
        //}
    }
    private void GainGiftBoxAction()
    {
        //fillChestProgress.fillAmount = 0f;
        //HintManage.AddHint(2);
        //ProgressComponent.Instance.OnProgressComplete();
        //giftBoxAnimator.Play("in");
    }
    public IEnumerator UpdateProgressIE()
    {
        yield return new WaitForSeconds(.8f);
        //float progress = ProgressComponent.Instance.GetProgress();
        ////CoinAddEffectUI.Instance.ShowEffect(20, CoinStartPos.position, coinPosTrans.position);
        //CoinAddEffectUI.Instance.Show(CoinStartPos.position, coinPosTrans.position);
        //if (progress >= 1.0f)
        //{
        //    SkipProgressButton.SetActive(false);
        //}
        //SoundManage.Instance.Play_CoinGain();
        //float time = 0f;
        //while (time < .8f)
        //{
        //    time += Time.deltaTime;
        //    fillChestProgress.fillAmount = Mathf.Lerp(fillChestProgress.fillAmount, progress, time / .8f);
        //    yield return new WaitForEndOfFrame();
        //}
        //fillChestProgress.fillAmount = progress;
        //if (ProgressComponent.Instance.GetProgress() >= 1.0f)
        //{
        //    giftBoxAnimator.Play("canget");
        //}
        //else
        //{
        //    giftBoxAnimator.Play("idle");
        //}
    }


    public void ButtonHome()
    {
        if (isClicked) return;
        isClicked = true;
        Deactive();
        GameController.Instance.StopRotateObject();
        PlayingPanel.Instance.ActiveHome();
        LevelManage.Instance.LoadCurrentLevel();
        SoundManage.Instance.Play_ButtonClick();
        EffectManage.Instance.TurnOffExplore();
    }

    public void ButtonReplay()
    {
        if (!isClicked)
        {
            isClicked = true;
            //BlackScreenEffect.instance.On(blackScreenTime, ReplayAction);
            Ground.SetActive(true);
            LoseFloatingObject.SetActive(false);
            SoundManage.Instance.Play_ButtonClick();
            SoundManage.Instance.Play_HomeMusic();
            ReplayAction();
        }
    }
    private void ReplayAction()
    {
        Deactive();
        PlayingPanel.Instance.ActiveHome();
        LevelManage.Instance.LoadCurrentLevel();
        //SceneLevelManage.Instance.ReloadLevel();
    }
    public void ButtonLotery()
    {
        DeactiveImediately();
        //LoteryPanel.Instance.Active();
    }
}
