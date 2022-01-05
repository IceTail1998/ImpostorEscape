using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Laser_1 : MonoBehaviour
{
    [SerializeField]
    ButtonLaser buttonLaserCheck;
    private void Update()
    {
        if (buttonLaserCheck.bIsOn)
        {
            gameObject.SetActive(false);
        }
    }

}
