using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] CoinText;
    [SerializeField]
    Transform CoinPosTrans;
    [SerializeField]
    GameObject uiNoButton;
    [SerializeField]
    GameObject uiWithButton;
    public Vector3 CoinPos { get { return CoinPosTrans.position; } }
    public static CoinUI Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UpdateCoin();
    }
    public void UpdateCoin()
    {
        for (int i = 0; i < CoinText.Length; i++)
        {
            CoinText[i].text = string.Empty + CoinManage.GetGem();
        }
    }
    public void ButtonActive()
    {
        uiNoButton.SetActive(true);
        uiWithButton.SetActive(false);
    }
    public void TurnOffButton()
    {
        uiNoButton.SetActive(true);
        uiWithButton.SetActive(false);
    }
    public void TurnOnButton()
    {
        uiNoButton.SetActive(false);
        uiWithButton.SetActive(true);
    }

}
