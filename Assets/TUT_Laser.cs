using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TUT_Laser : MonoBehaviour
{

    [SerializeField]
    ButtonLaser buttonLaser;
    [SerializeField]
    GameObject TutObject;
    bool checkP1 = false;
    bool isOK = false;
    void Update()
    {
        if (isOK) return;
        if (buttonLaser != null)
        {
            if (buttonLaser.bIsOn)
            {
                TutObject.SetActive(true);
                checkP1 = true;
            }
            else
            {
                if (checkP1)
                {
                    TutObject.SetActive(false);
                    isOK = true;
                }
            }
        }
    }
}
