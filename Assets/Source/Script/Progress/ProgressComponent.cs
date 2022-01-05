using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressComponent : MonoBehaviour
{
    public static ProgressComponent Instance;
    [SerializeField]
    string ProgressName;
    [SerializeField]
    string progressData = "5,10";
    List<int> listData = new List<int>();

    public int currentIndex { get; private set; }
    public int currentProgress { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (FirstOpenController.instance.IsOpenFirst)
        {
            FirstInit();
        }
        FetchData();
    }
    void FirstInit()
    {
        if (progressData.Length <= 0)
        {
            progressData = "5";
        }
        PlayerPrefs.SetString(ProgressName + "_DATA", progressData);
        PlayerPrefs.SetInt(ProgressName + "_INDEX", 0);
        PlayerPrefs.SetInt(ProgressName + "_VALUE", 0);
        PlayerPrefs.SetInt(ProgressName + "_DIDGETFIRST", 0);
        string[] datas = progressData.Split(',');
        listData.Clear();
        for (int i = 0; i < datas.Length; i++)
        {
            int val = int.Parse(datas[i]);
            if (val <= 0)
            {
                val = 5;
            }
            listData.Add(val);
        }
        if (listData.Count == 0)
        {
            listData.Add(5);
        }
    }
    public bool DidGetFirstReward()
    {
        return 1 == PlayerPrefs.GetInt(ProgressName + "_DIDGETFIRST", 0);
    }
    public void FetchData()
    {
        currentIndex = PlayerPrefs.GetInt(ProgressName + "_INDEX", 0);
        progressData = PlayerPrefs.GetString(ProgressName + "_DATA", "5");
        currentProgress = PlayerPrefs.GetInt(ProgressName + "_VALUE", 0);
        string[] datas = progressData.Split(',');
        listData.Clear();
        for (int i = 0; i < datas.Length; i++)
        {
            int val = int.Parse(datas[i]);
            if (val <= 0)
            {
                val = 5;
            }
            listData.Add(val);
        }
    }
    public void OnProgressGain()
    {
        int val = GetProgressValue();
        if (val < 0)
        {
            val = 0;
        }
        val++;
        PlayerPrefs.SetInt(ProgressName + "_VALUE", val);
    }
    public void OnGetReward()
    {
        PlayerPrefs.SetInt(ProgressName + "_DIDGETFIRST", 1);
    }
    public void OnProgressComplete()
    {
        if (currentIndex < listData.Count - 1)
        {
            currentIndex++;
        }
        OnGetReward();
        PlayerPrefs.SetInt(ProgressName + "_INDEX", currentIndex);
        PlayerPrefs.SetInt(ProgressName + "_VALUE", 0);
    }
    public int GetProgressValue()
    {
        return PlayerPrefs.GetInt(ProgressName + "_VALUE", 0);
    }
    public int GetProgressRequire()
    {
        FetchData();
        return listData[currentIndex];
    }
    public float GetProgress()
    {
        FetchData();
        int val = GetProgressValue();
        int tol = listData[currentIndex];
        if (tol > 0)
        {
            return (float)val / tol;
        }
        return 0;
    }


}
