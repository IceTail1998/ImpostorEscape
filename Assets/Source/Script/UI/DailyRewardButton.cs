using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardButton : MonoBehaviour
{
    public int id;
    [SerializeField]
    GameObject Cover;
    [SerializeField]
    GameObject Check;
    public void UpdateData(bool didGet, bool canSelect)
    {
        if (didGet)
        {
            GetComponent<Button>().interactable = canSelect;
            Cover.SetActive(true);
            Check.SetActive(true);
        }
        else
        {
            GetComponent<Button>().interactable = canSelect;
            Check.SetActive(false);
            if (canSelect)
            {
                Cover.SetActive(false);
            }
            else
            {
                Cover.SetActive(true);
            }
        }
    }
}
