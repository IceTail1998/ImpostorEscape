using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatePanel : BasePanel
{
    public static RatePanel Instance;
    [SerializeField]
    Button OkButton;
    [SerializeField]
    RateButton[] listRateButton;
    [SerializeField]
    Sprite unCheckSprite;
    [SerializeField]
    Sprite unCheckSpriteSpecial;
    [SerializeField]
    Sprite checkSprite;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public override void Active()
    {
        base.Active();
        OkButton.interactable = false;
    }
    public override void Deactive()
    {
        base.Deactive();
    }
    int currentRate = 1;
    public void ClickRate(int rate)
    {
        currentRate = rate;
        OkButton.interactable = true;
        for (int i = 0; i < listRateButton.Length; i++)
        {
            if (i < currentRate)
            {
                listRateButton[i].ChangeSprite(checkSprite);
            }
            else
            {
                if (i == 4)
                {
                    listRateButton[i].ChangeSprite(unCheckSpriteSpecial);
                }
                else
                {
                    listRateButton[i].ChangeSprite(unCheckSprite);
                }

            }
        }
    }
    public void ButtonOk()
    {
        if (currentRate == 5)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
            FirstOpenController.PlayerRated();
            Deactive();
            MessageCallBackPopupPanel.INSTACNE.Active("Thank you for rating us!", EndGamePanel.Instance.AfterRate);
        }
        else
        {
            FirstOpenController.PlayerRated();
            Deactive();
            MessageCallBackPopupPanel.INSTACNE.Active("Thank you for rating us!", EndGamePanel.Instance.AfterRate);
        }
    }
    public void ButtonClose()
    {
        Deactive();
        EndGamePanel.Instance.AfterRate();
    }
}
