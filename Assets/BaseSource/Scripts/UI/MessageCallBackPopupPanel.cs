using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageCallBackPopupPanel : BasePanel
{
    #region Singleton
    public static MessageCallBackPopupPanel INSTACNE;
    private void Awake()
    {
        if (INSTACNE == null)
        {
            INSTACNE = this;
        }
    }
    #endregion
    private GameObject panelChild;
    System.Action<bool> callBackFunc;
    System.Action callBackNormal;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject YNObject;
    [SerializeField]
    private GameObject MObject;
    public override void Active()
    {
        //SoundManage.Instance.Play_ClickOpen();
        //if (PlayingPanel.Instance.IsActive)
        //{
        //    Tung.UIManager.Instance.SetPlayOnTop(false);
        //}
        SetYNOn(false);
        callBackFunc = null;
        callBackNormal = null;
        base.Active();
    }
    public void Active(string question)
    {
        //SoundManage.Instance.Play_ClickOpen();
        //if (PlayingPanel.Instance.IsActive)
        //{
        //    Tung.UIManager.Instance.SetPlayOnTop(false);
        //}
        SetYNOn(false);
        callBackFunc = null;
        callBackNormal = null;
        if (question != null)
        {
            questionText.text = question;
        }
        if (titleText != null)
        {
            titleText.text = string.Empty;
        }
        base.Active();
    }
    public void Active(string ques, System.Action callBack)
    {
        callBackNormal = callBack;
        SetYNOn(false);
        callBackFunc = null;
        if (ques != null)
        {
            questionText.text = ques;
        }
        if (titleText != null)
        {
            titleText.text = string.Empty;
        }
        base.Active();
    }
    public void Active(string tittle, string question)
    {
        SetYNOn(false);
        callBackFunc = null;
        callBackNormal = null;
        if (question != null)
        {
            questionText.text = question;
        }
        if (titleText != null)
        {
            titleText.text = tittle;
        }
        base.Active();
    }
    public void Active(string question, System.Action<bool> callBackFunction, bool hasYN)
    {
        //SoundManage.Instance.Play_ClickOpen();
        //if (PlayingPanel.Instance.IsActive)
        //{
        //    Tung.UIManager.Instance.SetPlayOnTop(false);
        //}
        callBackFunc = callBackFunction;
        callBackNormal = null;
        SetYNOn(hasYN);
        if (question != null)
        {
            questionText.text = question;
        }
        if (titleText != null)
        {
            titleText.text = string.Empty;
        }
        base.Active();
    }
    public void SetSpriteAsset(TMP_SpriteAsset spr)
    {
        questionText.spriteAsset = spr;
    }
    private void SetYNOn(bool on)
    {
        if (on)
        {
            YNObject.SetActive(true);
            MObject.SetActive(false);
        }
        else
        {
            YNObject.SetActive(false);
            MObject.SetActive(true);
        }
    }

    public void ButtonYes()
    {
        //if (PlayingPanel.Instance.IsActive)
        //{
        //    Tung.UIManager.Instance.SetPlayOnTop(true);
        //}
        Deactive();
        SoundManage.Instance.Play_ButtonClick();
        callBackFunc?.Invoke(true);
        callBackNormal?.Invoke();
    }
    public void ButtonNo()
    {
        //if (PlayingPanel.Instance.IsActive)
        //{
        //    Tung.UIManager.Instance.SetPlayOnTop(true);
        //}
        Deactive();
        SoundManage.Instance.Play_ButtonClick();
        callBackFunc?.Invoke(false);
        callBackNormal?.Invoke();
        //SoundManage.Instance.Play_ClickClose();
    }
    public void IAPButtonPurchaseFail()
    {
        Active("Failed to purchase!");
    }
}
