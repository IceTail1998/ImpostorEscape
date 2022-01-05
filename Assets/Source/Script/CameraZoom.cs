using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraZoom : MonoBehaviour
{
    #region
    public static CameraZoom instance;
    private void Awake()
    {
        instance = this;
        defaultRotation = transform.rotation;
    }
    #endregion

    Camera cam;

    //[SerializeField]
    //private Camera cam_Other;

    Transform camTrans;

    [Header("Param")]
    [SerializeField]
    float zoomSpeed = 25f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    [Header("Ingame")]
    private bool wasInit = false;

    [SerializeField]
    public float zoomDefault = 10f;
    [SerializeField]
    public bool isZooming = false;
    [SerializeField]
    float zoomTarget = 0;
    [SerializeField]
    float currentZoomTmp;
    [SerializeField]
    float dir = 0;
    Quaternion defaultRotation;
    
    public void Start()
    {
        cam = Camera.main;
        camTrans = cam.transform;
        defaultRotation = camTrans.rotation;
        if (cam.aspect < 9f / 16f)
        {
            zoomDefault = zoomDefault * (9f / 16f) / cam.aspect;
        }
        cam.fieldOfView = currentZoomTmp = zoomDefault;
        if (cam.aspect < 9f / 16f)
        {
            cam.fieldOfView = cam.fieldOfView * (9f / 16f) / cam.aspect;
        }
        //if (cam_Other)
        //{
        //    cam_Other.fieldOfView = currentZoomTmp = zoomDefault;
        //    if (cam.aspect < 9f / 16f)
        //    {
        //        cam_Other.fieldOfView = cam.fieldOfView * (9f / 16f) / cam.aspect;
        //    }
        //    else
        //    {
        //        cam_Other.fieldOfView = cam.fieldOfView;
        //    }
        //}

        isZooming = false;
    }

    private void Update()
    {
        if (isZooming)
        {
            currentZoomTmp = cam.fieldOfView;
            currentZoomTmp += dir * zoomSpeed * Time.deltaTime;
            currentZoomTmp = Mathf.Clamp(currentZoomTmp, minZoom, maxZoom);
            cam.fieldOfView = currentZoomTmp;

            if (dir == 0)
            {
                cam.fieldOfView = currentZoomTmp = zoomTarget;
                isZooming = false;
            }
            else if (dir == 1 && currentZoomTmp >= zoomTarget)
            {
                cam.fieldOfView = currentZoomTmp = zoomTarget;
                isZooming = false;
            }
            else if (dir == -1 && currentZoomTmp <= zoomTarget)
            {
                cam.fieldOfView = currentZoomTmp = zoomTarget;
                isZooming = false;
            }

            //if (cam_Other)
            //{
            //    cam_Other.fieldOfView = currentZoomTmp;
            //}
        }
    }

    private void Zoom(float percent)
    {
        zoomTarget = zoomDefault * percent;
        if (zoomTarget == currentZoomTmp)
        {
            dir = 0;
            isZooming = false;
        }
        else if (zoomTarget > currentZoomTmp)
        {
            dir = 1;
            isZooming = true;
        }
        else if (zoomTarget < currentZoomTmp)
        {
            dir = -1;
            isZooming = true;
        }
    }
    private void ZoomSize(float size)
    {
        float percent = size / zoomDefault;
        if (cam.aspect < 9f / 16f)
        {
            percent = (size * (9f / 16f) / cam.aspect) / zoomDefault;
        }
        Zoom(percent);
    }
    public static void Zoom_Handle(float percent)
    {
        if (instance != null)
            instance.Zoom(percent);
    }
    public static void ZoomSize_Handle(float size)
    {
        if (instance != null)
            instance.ZoomSize(size);
    }

    private void ResetZoom()
    {
        isZooming = false;
        if(cam == null)
        {
            cam = Camera.main;
        }
        if (cam)
            cam.fieldOfView = currentZoomTmp = zoomDefault;
        else
        {
            cam = CameraController.instance.mainCam;
            cam.fieldOfView = currentZoomTmp = zoomDefault;
        }
        transform.rotation = defaultRotation;
        //if (cam_Other)
        //{
        //    cam_Other.fieldOfView = currentZoomTmp;
        //}
    }
    public static void ResetZoomHandle()
    {
        if (instance != null)
            instance.ResetZoom();

    }
    public static bool IsZooming()
    {
        if (instance != null)
        {
            return instance.isZooming;
        }
        else return false;
    }
}
