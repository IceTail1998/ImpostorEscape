using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject objWillDeative;
    [SerializeField]
    ButtonLaser[] buttonLaser;
    [SerializeField]
    Button_Fence[] buttonFence;
    bool isOk = false;
    private void Update()
    {
        if (isOk)
        {
            gameObject.SetActive(false);
            objWillDeative.SetActive(false);
            return;
        }
        bool check = true;
        if (buttonFence != null)
        {
            if (buttonFence.Length > 0)
            {
                for (int i = 0; i < buttonFence.Length; i++)
                {
                    if (!buttonFence[i].bIsClicked)
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    isOk = true;
                }
            }
        }
        if (buttonLaser != null)
        {
            if (buttonLaser.Length > 0)
            {
                for (int i = 0; i < buttonLaser.Length; i++)
                {
                    if (!buttonLaser[i].bIsOn)
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    isOk = true;
                }
            }
        }
    }
}
