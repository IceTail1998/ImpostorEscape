using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardController : MonoBehaviour
{
    public static DailyRewardController Instance;
    private const string LASTDAYPLAY = "LAST_TIME_PLAY";
    private const string LASTDAYREWARD = "LAST_TIME_REWARDED";
    private const string DIDGETRW = "DID_GET_RW_DAILY";
    private const string GETRWDAYCOUNT = "COUNT_GET_RW";
    [SerializeField]
    RewardData[] listRewardData;
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
        CheckDatePlay();
    }
    public static void FirstInit()
    {
        PlayerPrefs.SetInt(GETRWDAYCOUNT, 0);
        PlayerPrefs.SetInt(DIDGETRW, 0);
        DateTime t = DateTime.Now;
        t.AddDays(-1);
        PlayerPrefs.SetString(LASTDAYREWARD, Converter.DateToString(t));
        SetLastTimePlay();
    }
    public static bool DidGetReward()
    {
        return 1 == PlayerPrefs.GetInt(DIDGETRW, 0);
    }
    public static void SetLastTimePlay()
    {
        DateTime t = DateTime.Now;
        PlayerPrefs.SetString(LASTDAYPLAY, Converter.DateToString(t));
        Debug.Log("Set last time play: " + Converter.DateToString(t));
    }
    public static string GetLastTimePlay()
    {
        return PlayerPrefs.GetString(LASTDAYPLAY);
    }
    public static string GetLastTimeReward()
    {
        return PlayerPrefs.GetString(LASTDAYREWARD);
    }
    private static void ResetDidGetReward()
    {
        PlayerPrefs.SetInt(DIDGETRW, 0);
    }
    public static bool CanGetReward()
    {
        return !DidGetReward();
    }
    public void CheckDatePlay()
    {
        DateTime now = DateTime.Now;
        Debug.Log("LAST TIME PLAY: " + GetLastTimePlay());
        DateTime last = Converter.StringToDate(GetLastTimePlay());
        TimeSpan timeSpan = now.Subtract(last);
        SetLastTimePlay();
        if (now.Day.Equals(last.Day) && now.Month.Equals(last.Month) && now.Year.Equals(last.Year))
        {

        }
        else
        {
            if (timeSpan.TotalDays > 0)
            {
                ResetDidGetReward();
            }
        }
        //if (CanGetReward() && !FirstOpenController.instance.IsOpenFirst)
        //{
        //    DailyRewardPanel.Instance.Active();
        //}
        //PlayingPanel.Instance.CheckShowNotifications();
    }
    public static bool IsGetRewardToday()
    {
        DateTime now = DateTime.Now;
        DateTime last = Converter.StringToDate(GetLastTimeReward());
        if (now.Day.Equals(last.Day) && now.Month.Equals(last.Month) && now.Year.Equals(last.Year))
        {
            return true;
        }
        return false;
    }
    public static int GetRewardedCount()
    {
        return PlayerPrefs.GetInt(GETRWDAYCOUNT, 0);
    }
    public static void GetReward()
    {
        PlayerPrefs.SetInt(DIDGETRW, 1);
        int count = PlayerPrefs.GetInt(GETRWDAYCOUNT, 0);
        count++;
        PlayerPrefs.SetInt(GETRWDAYCOUNT, count);
    }
}
[System.Serializable]
public class RewardData
{
    public bool isCoin;
    public bool isSkin;
    public bool isPet;
    public Sprite icon;
    public string name;
    public int value;
}



