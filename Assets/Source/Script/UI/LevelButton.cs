using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    //public int chapter;
    public int level;
    public TextMeshProUGUI levelTxt;
    public Image im;
    //public GameObject tick;

    /// <summary>
    /// t: 
    /// 1 = Passed 
    /// 2 = Opened 
    /// 3 = Lock
    /// </summary>
    /// <param name="sp"> Button's picture</param>
    /// <param name="t">1 = Passed \n2 = Opened \n3 = Lock</param>
    public void Init(Sprite sp, int t)
    {
        levelTxt.text = string.Empty + level;
        //Debug.Log(" " + level + " : " + t);
        //if (t == 1)
        //{
        //    levelTxt.gameObject.SetActive(true);
        //    //tick.SetActive(true);
        //}
        //else if (t == 2)
        //{
        //    levelTxt.gameObject.SetActive(true);
        //    //tick.SetActive(false);
        //}
        //else
        //{
        //    levelTxt.gameObject.SetActive(false);
        //    //tick.SetActive(false);
        //}
        im.sprite = null;
        im.sprite = sp;
    }
    //public void Click(int chap, int lev)
    //{
    //    if(chap>0 && lev > 0)
    //    {
    //        SoundManager.instance.PlayButtonSound();
    //        BlackScreenEffect.instance.On(1f);
    //        StartCoroutine(Wait(chap, lev));
    //    }
    //}
    //private IEnumerator Wait(int chap, int lev)
    //{
    //    yield return new WaitForSeconds(.25f);
    //    GameManager.instance.SelectLevel(chap, lev);
    //    LevelManager.instance.LoadLevel(chap, lev);
    //    SelectLevelPanel.instance.Deactive();
    //}
}
