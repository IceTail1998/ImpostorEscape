using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotSimple : MonoBehaviour
{
    public int order = 0;
    private void Start()
    {

    }
    void Update()
    {
        //Chup man o day
        if (Input.GetKeyDown(KeyCode.C))
        {
            ScreenCapture.CaptureScreenshot("ScreenShot_" + order + ".png");
            Debug.Log("Captured " + order);
            order++;
        }
    }
}
