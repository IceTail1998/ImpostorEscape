using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    public static SelectLevelPanel instance;
    public LevelButton[] listButton;
    public GameObject LevelButtonPrefab;
    public Transform ButtonsHolder;
    public Sprite lockSprite, openSprite, passedSprite;
    [SerializeField]
    ScrollRect levelScrollRect;
    bool isInit = false;
    private void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        Start_BasePanel();
        StartCoroutine(InitIE());
    }
    public void ButtonBack()
    {
        Deactive();
        PlayingPanel.Instance.Active();
        SoundManage.Instance.Play_ButtonClick();
        GameController.Instance.ContinueGame();
    }
    IEnumerator InitIE()
    {
        float time = 0;
        while (LevelManage.Instance == null)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            if (time > 2)
            {
                break;
            }
        }
        if (LevelManage.Instance == null)
        {

        }
        else
        {
            Init();
        }
    }
    public void Init()
    {
        int t = LevelManage.Instance.numberOfLevels[0];
        while (t > 0)
        {
            t--;
            Instantiate(LevelButtonPrefab, ButtonsHolder.transform);
        }
        listButton = ButtonsHolder.GetComponentsInChildren<LevelButton>();
        for (int i = 0; i < listButton.Length; i++)
        {
            LevelButton button = listButton[i];
            int c = i + 1;
            button.GetComponent<Button>().onClick.AddListener(() => SelectLevel(c));
        }
        isInit = true;
        UpdateInformation();
    }

    public override void Active()
    {
        isClicked = false;
        if (!isInit)
        {
            Init();
        }
        UpdateInformation();
        base.Active();
    }
    public override void Deactive()
    {
        base.Deactive();
    }

    public void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UpdateInformation()
    {
        float endD = listButton.Length / 4;
        float cur = LevelManage.Instance.currentLevel / 4;
        for (int i = 0; i < listButton.Length; i++)
        {
            LevelButton button = listButton[i];
            button.level = i + 1;
            int c = i + 1;
            if (LevelManage.Instance.IsLevelOpened(0, c))
            {
                if (LevelManage.Instance.IsLevelPassed(0, c))
                {
                    button.Init(passedSprite, 1);
                }
                else
                {
                    button.Init(openSprite, 1);
                }
            }
            else
            {
                button.Init(lockSprite, 0);
            }
        }
        if (levelScrollRect != null)
        {
            float d = 1 - (cur / endD);
            StopAllCoroutines();
            StartCoroutine(UpdateScrollview(d));
        }
    }
    private IEnumerator UpdateScrollview(float val)
    {
        float time = 0f;
        while (time < 0.6f)
        {
            time += Time.deltaTime;
            levelScrollRect.verticalNormalizedPosition = Mathf.Lerp(levelScrollRect.verticalNormalizedPosition, val, time / 0.6f);
            yield return new WaitForEndOfFrame();
        }
        levelScrollRect.verticalNormalizedPosition = val;
    }
    bool isClicked = false;
    public void SelectLevel(int level)
    {
        if (isClicked) return;
        if (!LevelManage.Instance.IsLevelOpened(0, level)) return;
        isClicked = true;
        LevelManage.Instance.SetLevel(level);
        LevelManage.Instance.LoadCurrentLevel();
        EndGamePanel.Instance.Deactive();
        Deactive();
        PlayingPanel.Instance.ActiveHome();
        SoundManage.Instance.Play_ButtonClick();
        SoundManage.Instance.Stop_Laser();
    }
}
