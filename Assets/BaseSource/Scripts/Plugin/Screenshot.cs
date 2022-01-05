using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public int order = 0;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    string prefixImg;
    private void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }
    public Texture2D TakeScreenShot()
    {
        return DoScreenshot();
    }

    Texture2D DoScreenshot()
    {

        int resWidth = mainCam.pixelWidth;
        int resHeight = mainCam.pixelHeight;
        //Camera camera = Camera.main;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 32);
        mainCam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
        mainCam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();
        mainCam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        return screenShot;
    }

    public Texture2D SaveScreenshotToFile(string fileName)
    {
        Texture2D screenShot = DoScreenshot();
        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fileName, bytes);
        return screenShot;
    }
    int c = 0;
    void Update()
    {
        //Chup man o day

        if (Input.GetKeyDown(KeyCode.A))
        {
            c++;
            c = c % 2;
            if (c == 0)
            {
                mainCam.clearFlags = CameraClearFlags.Skybox;
                Debug.Log("Cam Skybox");

            }
            else
            {
                mainCam.clearFlags = CameraClearFlags.Depth;
                Debug.Log("Cam Depth");
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //ScreenCapture.CaptureScreenshot("ScreenShot_" + order+".png");
            SaveScreenshotToFile("SC_" + prefixImg + "_" + order + ".png");
            order++;
            Debug.Log("Captured " + order);
        }
    }
}
