using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : MonoBehaviour
{
    private const string DATETIMEFORMAT = "ddMMyyyy HHmmss";
    public static string ToLevelStringLog(int t)
    {
        string res = "";
        if (t < 10)
        {
            res += "00" + t;
        }
        else if (t < 100)
        {
            res += "0" + t;
        }
        else
        {
            res += t;
        }
        return res;
    }
    public static string DateToString(DateTime t)
    {
        return t.ToString(DATETIMEFORMAT);
    }
    public static DateTime StringToDate(String s)
    {
        DateTime t = DateTime.Now;
        Boolean check = false;
        try
        {
            check = DateTime.TryParseExact(s, DATETIMEFORMAT, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces,out t);
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
        }
        if (check) Debug.Log("String to date success");

        return t;
    }
}
