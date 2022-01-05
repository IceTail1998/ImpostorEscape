using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class CameraCapture : MonoBehaviour
{
    #region Singleton
    public static CameraCapture instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    Camera cam;

    [Header("Param")]
    [SerializeField]
    private bool flip = true;
    [SerializeField]
    private bool exportPNG = true;
    [SerializeField]
    private bool useScreenCapture = true;
    [SerializeField]
    private bool disableCamAfterCapture = false;
    [SerializeField]
    private int frame = 0;
    [SerializeField]
    RenderTexture exportTexture;

    [Header("Ingame")]
    [SerializeField]
    private bool isCapturing = false;
    [SerializeField]
    private int currentFrame = 0;

    public delegate void OnCaptureDoneCallback(string filePath);
    public static event OnCaptureDoneCallback onCaptureDone;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (exportTexture != null)
        {
        }
        else
        {
            exportTexture = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0, UnityEngine.Experimental.Rendering.DefaultFormat.LDR);
        }

        Debug.Log("Press 'Shift' + 'C' to Capture");
    }

    public void Capture()
    {
        if (isCapturing || cam == null)
            return;

        Debug.Log("Capture");
        isCapturing = true;

        if (exportTexture == null)
        {
            cam.targetTexture = RenderTexture.GetTemporary(cam.pixelWidth, cam.pixelHeight);
        }
        else
        {
            cam.targetTexture = exportTexture;
        }

        currentFrame = frame;
        cam.enabled = true;
        
    }

    private void OnPostRender()
    {
        if (!isCapturing)
            return;

        currentFrame--;
        if (currentFrame > 0)
        {
            return;
        }

        isCapturing = false;
        RenderTexture renderTexture = new RenderTexture(cam.targetTexture);

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        if (useScreenCapture)
            texture = ScreenCapture.CaptureScreenshotAsTexture();

        if (flip)
        {
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height / 2; j++)
                {
                    Color tmp = texture.GetPixel(texture.width - i - 1, texture.height - j - 1);
                    texture.SetPixel(texture.width - i - 1, texture.height - j - 1, texture.GetPixel(i, j));
                    texture.SetPixel(i, j, tmp);
                }
            }
            texture.Apply();
        }

        byte[] byteArray;
        if (exportPNG)
        {
            byteArray = texture.EncodeToPNG();
        }
        else
        {
            byteArray = texture.EncodeToJPG();
        }


        string name = string.Format("Cap {0}.png", System.DateTime.Now.ToString("HH-mm-ss"));
        string fileLocation = string.Format("{0}/Capture/{1}", Application.persistentDataPath, name);

        if (!Directory.Exists(string.Format("{0}/Capture", Application.persistentDataPath)))
        {
            Directory.CreateDirectory((string.Format("{0}/Capture", Application.persistentDataPath)));
        }
        if (!File.Exists(fileLocation))
        {
            File.Create(fileLocation).Close();
        }


        File.WriteAllBytes(fileLocation, byteArray);
        if (onCaptureDone != null)
            onCaptureDone.Invoke(fileLocation);

        Destroy(texture);
        Destroy(renderTexture);

        if (exportTexture == null)
            RenderTexture.ReleaseTemporary(cam.targetTexture);

        cam.targetTexture = null;

        if (disableCamAfterCapture)
            cam.enabled = false;
        
    }

    public static void CaptureHandle()
    {
        if (instance != null)
        {
            instance.Capture();
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (Application.isPlaying)
        {
            Event e = Event.current;
            if (e != null && e.shift && e.keyCode == KeyCode.C)
            {
                Capture();
            }
        }
    }
#endif
}
