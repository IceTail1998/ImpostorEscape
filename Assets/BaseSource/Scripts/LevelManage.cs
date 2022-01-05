using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class LevelManage : MonoBehaviour
{
    public static LevelManage Instance;

    [SerializeField]
    internal GameObject currentLevelMap;
    LevelMap lvMap;
    [SerializeField]
    internal int currentMode;

    [SerializeField]
    internal int currentLevel;
    public bool IsCompleteAllLevel;
    public bool IsFirstTimeCompleteAll = true;
    public List<int> numberOfLevels;
    [SerializeField]
    bool bIsSelectLevel = false;
    public bool isLastLevelPassedBeforePlay { get; private set; }
    [SerializeField]
    int levelZ;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        if (FirstOpenController.instance.IsOpenFirst)
        {
            Init();
        }

        IsCompleteAllLevel = false;
        int lv = GetMaxLevelCanPlay(0);

        if (lv >= numberOfLevels[0])
        {
            lv = GetLastPlayLevel(0).y;
        }
        if (bIsSelectLevel)
        {
            levelZ = Mathf.Clamp(levelZ, 1, numberOfLevels[0]);
            SetUp(0, levelZ);
            Debug.Log("LEVELZZZ = " + levelZ);
        }
        else
        {
            SetUp(0, lv);
        }
        if (lv >= numberOfLevels[0])
        {
            IsCompleteAllLevel = true;
            EndGamePanel.Instance.ShowNextRandomButton();
        }
        PlayingPanel.Instance.UpdateLevelText();
        IsFirstTimeCompleteAll = PlayerPrefs.GetInt("FIRST_COMPLETE", 1) == 1;
    }
    
    public void Init()
    {
        for (int i = 0; i < numberOfLevels.Count; i++)
        {
            int max = numberOfLevels[i];
            for (int j = 1; j <= max; j++)
            {
                PlayerPrefs.SetInt("M_" + i + "_L_" + j, 0);

            }
            PlayerPrefs.SetInt("M_" + i + "_L_" + 1, 1);
            PlayerPrefs.SetInt("LASTPLAY_" + i, 1);
        }
        PlayerPrefs.SetInt("FIRST_COMPLETE", 1);
        IsFirstTimeCompleteAll = true;
    }

    public void SetLevel(int lv)
    {
        currentLevel = lv;
        currentMode = 0;
    }
    public bool IsLevelOpened(int mode, int level)
    {
        int check = PlayerPrefs.GetInt("M_" + mode + "_L_" + level);
        return check >= 1;
    }
    public bool IsLevelPassed(int mode, int level)
    {
        int check = PlayerPrefs.GetInt("M_" + mode + "_L_" + level);
        return check >= 2;
    }

    public void OpenLevel(int mode, int level)
    {
        if (level > numberOfLevels[0])
        {
            IsCompleteAllLevel = true;
        }
        if (!IsLevelOpened(0, level))
        {
            PlayerPrefs.SetInt("M_" + mode + "_L_" + level, 1);
        }
    }
    public void MarkCompleteAllFirst()
    {
        PlayerPrefs.SetInt("FIRST_COMPLETE", 0);
        IsFirstTimeCompleteAll = false;
    }

    public int GetMaxLevelCanPlay(int mode)
    {
        int max = numberOfLevels[mode];
        int z = 1;
        for (int i = 1; i <= max; i++)
        {
            if (IsLevelOpened(mode, i))
            {
                z = i;
            }
        }
        Debug.Log("Max level mode " + mode + " is " + z);
        return z;
    }
    public Vector2Int GetLastPlayLevel(int mode)
    {
        Vector2Int ret = new Vector2Int(0, 1);
        ret.x = 0;
        ret.y = PlayerPrefs.GetInt("LASTPLAY_" + mode, 1);
        return ret;
    }

    public void LoadNormalLevel()
    {
        int maxLevel = GetMaxLevelCanPlay(0);
        SetUp(0, maxLevel);
    }
    //public void LoadRandomLevel()
    //{
    //    //MaterialManage.Instance.ChangePack();
    //    int min = (int)0.3f * numberOfLevels[0];
    //    min = Mathf.Clamp(min, 1, numberOfLevels[0]);
    //    int lv = currentLevel;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        lv = Random.Range(min, numberOfLevels[0] + 1);
    //        if (lv != currentLevel)
    //        {
    //            break;
    //        }
    //    }
    //    if (lv == currentLevel)
    //    {
    //        if (lv == numberOfLevels[0])
    //        {
    //            lv--;
    //        }
    //        else if (lv > numberOfLevels[0])
    //        {
    //            lv = currentLevel - 1;
    //        }
    //        else
    //        {
    //            lv++;
    //        }
    //    }
    //    SetUp(0, lv);
    //}
    public void LoadCurrentLevel()
    {
        //MaterialManage.Instance.ChangePack();
        SetUp(currentMode, currentLevel);
    }
    //public void LoadCurrentLevelHome()
    //{
    //    SetUpStart(currentMode, currentLevel);
    //}
    public GameObject GetCurrentLevelMap()
    {
        return currentLevelMap;
    }

    public void Replay()
    {
        SetUp(currentMode, currentLevel);
    }

    public void PassCurrentLevel()
    {
        if (currentLevel <= numberOfLevels[currentMode])
        {
            //ProgressComponent.Instance.SetCurrent(currentLevel);
            PlayerPrefs.SetInt("M_" + currentMode + "_L_" + currentLevel, 2);
            OpenLevel(currentMode, currentLevel + 1);
            currentLevel++;
            if (currentLevel > numberOfLevels[0])
            {
                currentLevel = Random.Range(1, numberOfLevels[0] + 1);
            }
        }
    }
    public void SkipCurrentLevel()
    {
        if (currentLevel <= numberOfLevels[currentMode])
        {
            //ProgressComponent.Instance.SetCurrent(currentLevel);
            OpenLevel(currentMode, currentLevel + 1);
            currentLevel++;
            if (currentLevel > numberOfLevels[0])
            {
                currentLevel = Random.Range(1, numberOfLevels[0] + 1);
            }
        }
    }

    public void QuitLevel()
    {
        DestroyCurrentLevel();
    }

    public void SkipLevel()
    {

        DestroyCurrentLevel();
    }
    public void LoadPreviousLevel()
    {
        currentLevel--;
        currentLevel = Mathf.Clamp(currentLevel, 1, numberOfLevels[0]);
        SetUp(currentMode, currentLevel);
    }
    private void AskClearData(bool check)
    {
        //if (check)
        //{
        //    Init();
        //    UIManager.Instance.PlayToHome();
        //}
        //else
        //{
        //    UIManager.Instance.PlayToHome();
        //}
    }
    public async Task SetUp(int mode, int level)
    {
        currentMode = mode;
        currentLevel = level;
        if (BulletManage.Instance != null)
            BulletManage.Instance.TurnOffAllBullet();
        if (IsLevelPassed(mode, level))
        {
            isLastLevelPassedBeforePlay = true;
        }
        else
        {
            isLastLevelPassedBeforePlay = false;
        }
        if (SoundManage.Instance != null)
        {
            SoundManage.Instance.TurnOnYeah();
        }
        //PlayingPanel.Instance.UpdateLevelText();
        //DirectionManage.Instance?.FreeAll();
        switch (mode)
        {
            case 0:
                Debug.Log("LEVEL MANAGER: Setup mode 0 level " + level);

                //FireBaseManager.Instance.LogLevel(level);
                DestroyCurrentLevel();
                await Task.Yield();
                if (Ground.Instance != null)
                {
                    Ground.Instance.ChangeBG();
                }
                if (level > numberOfLevels[mode])
                {
                    MessageCallBackPopupPanel.INSTACNE.Active("You played all level!<br>Clear data?", AskClearData, true);
                    break;
                }
                GameObject levelObj = Instantiate(Resources.Load("Level/Level (" + level + ")", typeof(GameObject)), transform) as GameObject;
                currentLevelMap = levelObj;
                currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
                PlayerPrefs.SetInt("LASTPLAY_" + mode, level);
                break;
            case 1:
                Debug.Log("LEVEL MANAGER: Setup mode 1 level " + level);
                DestroyCurrentLevel();
                await Task.Yield();
                if (Ground.Instance != null)
                {
                    Ground.Instance.ChangeBG();
                }
                if (level > numberOfLevels[mode])
                {
                    MessageCallBackPopupPanel.INSTACNE.Active("You played all level!<br>Clear data?", AskClearData, true);
                    break;
                }
                levelObj = Instantiate(Resources.Load("Ads/Level (" + level + ")", typeof(GameObject)), transform) as GameObject;
                currentLevelMap = levelObj;
                currentLevelMap.GetComponent<LevelMap>().OnStartLevel();
                break;
        }
        currentLevelMap.SetActive(true);
        lvMap = currentLevelMap.GetComponent<LevelMap>();
        PlayingPanel.Instance?.UpdateLevelText();
    }

    public void DestroyCurrentLevel()
    {
        if (currentLevelMap != null)
        {
            Destroy(currentLevelMap.gameObject);
        }
    }

    public void FinishLevel()
    {
        currentLevelMap.GetComponent<LevelMap>().OnFinishLevel();
    }
    public void PlayNextLevel()
    {
        //MaterialManage.Instance.ChangePack();
        currentLevel++;
        if (currentLevel > numberOfLevels[0])
        {
            currentLevel = Random.Range(1, numberOfLevels[0] + 1);

        }
        SetUp(currentMode, currentLevel);
    }
   

}
