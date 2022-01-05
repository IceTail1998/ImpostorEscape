using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    bool _isPlaying;
    public bool IsPlaying { get { return _isPlaying; } }
    //public bool isTest;
    //public bool isAds;
    public bool isEditLevel;
    bool bIsFirstOnPause = false;
    public bool bCanRotateObject { get; private set; }
    public int goldGetInLevel { get; private set; }
    public float timePlay { get; private set; }
    public bool isPLayerMoved { get; private set; }
    bool isPause = false;
    float timePlayGame = 0f;
    public int countgame = 0;
    //[SerializeField] private GameObject WinObj;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        timePlayGame = 0f;
        isPause = false;
        bIsFirstOnPause = true;
    }
    bool bDidCheckTime = false;
    private void Update()
    {
        if (_isPlaying)
        {
            timePlay += Time.deltaTime;
            if (timePlay > 25f && !bDidCheckTime)
            {
                bDidCheckTime = true;
                PlayingPanel.Instance.SkipButtonAnimationOn();
            }
        }
        timePlayGame += Time.deltaTime;
    }
    public void ResetTimeShowHint()
    {
        timePlay = 0;
        bDidCheckTime = false;
    }
    public void StartGame()
    {
        isPLayerMoved = false;
        _isPlaying = true;
        timePlay = 0f;
        goldGetInLevel = 0;
        bDidCheckTime = false;
        bCanRotateObject = false;
        //MiniMap_Controller.instance.OnStart();
    }
    public void GetGold(int g)
    {
        if (g > 0)
            goldGetInLevel += g;
        //PlayingPanel.Instance.UpdateCoinText();
    }
    public void PauseGame()
    {
        if (_isPlaying)
        {
            isPause = true;
            _isPlaying = false;
            SoundManage.Instance.CheckLaser();
        }
    }
    public void StopRotateObject()
    {
        bCanRotateObject = false;
    }
    public void Replay()
    {
        _isPlaying = true;
        goldGetInLevel = 0;
    }

    public void QuitLevel()
    {
        _isPlaying = false;
    }
    public void ContinueGame()
    {
        if (isPause)
        {
            _isPlaying = true;
            isPause = false;
            SoundManage.Instance.CheckLaser();
        }
    }
    public void PlayerDidMove()
    {
        isPLayerMoved = true;
    }

    public void EndGame()
    {
        _isPlaying = false;
    }

    public void WinLevel()
    {
        //if (!_isPlaying) return;
        _isPlaying = false;
        bCanRotateObject = true;
        Debug.Log("End game: Win");
        ProgressComponent.Instance.OnProgressGain();
        //ProgressComponent.Instance.OnProgressGain();
        //WinObj.SetActive(true); 
        EndGame();
        int lv = LevelManage.Instance.currentLevel;
        if (lv > 1 && lv % 5 == 0 && !LevelManage.Instance.isLastLevelPassedBeforePlay)
        {
            GiftPanel.Instance.Active();
        }
        else
        {
            EndGamePanel.Instance.Active_Edited(true);
        }
        PlayingPanel.Instance.Deactive();
        SoundManage.Instance.Play_LevelComplete();
        SoundManage.Instance.Stop_Laser();
        LevelManage.Instance.PassCurrentLevel();
        StartCoroutine(WinIE());
    }
    private IEnumerator WinIE()
    {
        if (LevelManage.Instance.IsCompleteAllLevel && LevelManage.Instance.IsFirstTimeCompleteAll)
        {
            yield return new WaitForSeconds(1.8f);
            MessageCallBackPopupPanel.INSTACNE.Active("You've passed all levels!<br>New levels are coming soon.");
            EndGamePanel.Instance.ShowNextRandomButton();
            LevelManage.Instance.MarkCompleteAllFirst();
            //EndGamePanel.Instance.Active();
            //EffectManage.Instance.TurnOnExplore();
        }
        else
        {
            //EndGamePanel.Instance.Active();
            //EffectManage.Instance.TurnOnExplore();
        }

    }
    public void LoseLevel()
    {
        //if (!_isPlaying) return;
        _isPlaying = false;
        Debug.Log("End game: Lose");
        EndGamePanel.Instance.Active_Edited(false);
        PlayingPanel.Instance.Deactive();
        CameraController.instance.GameLoseAction();
        SoundManage.Instance.Stop_Laser();
    }
   
}