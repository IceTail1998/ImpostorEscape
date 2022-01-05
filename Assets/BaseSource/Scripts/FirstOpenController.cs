using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstOpenController : MonoBehaviour
{
    public static FirstOpenController instance;
    public int open;
    private const string DidRate = "Did player rate game";
    const string HIDETUT = "HIDE_TUT";
    public void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
        open = 111;
        //PlayerPrefs.DeleteAll();
        IsGameStartTheFirstTime();
    }
    public bool IsOpenFirst { get { return open != 111; } }
    private void IsGameStartTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
            open = 123;
            PlayerPrefs.SetInt(DidRate, 0);
            //GemManage.FirstOpenInit();
            CoinManage.FirstOpenInit();
            SoundManage.FirstInit();
            PrefInfo.FirstInit();
            //LifeManager.FirstInit();
            PlayerPrefs.SetInt("PLAY_COUNT", 1);
            PlayerPrefs.SetInt(HIDETUT, 0);
            //WinReward.FirstInit();
            DailyRewardController.FirstInit();
            //Const.FirstInit();
            //IQManager.FirstInit();
            //IAPControl.FirstInit();
        }
        else
        {
            int t = PlayerPrefs.GetInt("PLAY_COUNT", 1);
            t++;
            PlayerPrefs.SetInt("PLAY_COUNT", t);

        }
    }

    internal void DoHideTut()
    {
        PlayerPrefs.SetInt(HIDETUT, 1);
    }

    internal bool IsHideTut()
    {
        return PlayerPrefs.GetInt(HIDETUT, 0) == 1;
    }

    public static bool DidPlayerRate()
    {
        return PlayerPrefs.GetInt(DidRate) == 1;
    }
    public static void PlayerRated()
    {
        PlayerPrefs.SetInt(DidRate, 1);
    }
    public static int PlayCount()
    {
        return PlayerPrefs.GetInt("PLAY_COUNT", 1);

    }
}
