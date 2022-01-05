using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_ButtonFence : MonoBehaviour
{
    [SerializeField]
    Button_Fence buttonFence;
    // Update is called once per frame
    void Update()
    {
        if (buttonFence.bIsClicked)
        {
            gameObject.SetActive(false);
        }
    }
}
