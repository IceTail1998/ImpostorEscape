using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaleCustom : MonoBehaviour
{
    [SerializeField]
    Vector3 scale9_16;
    [SerializeField]
    Vector3 scale9_19;
    [SerializeField]
    Vector3 pos9_16;
    [SerializeField]
    Vector3 pos9_19;
    [SerializeField]
    bool bChangePosition = false;
    void Start()
    {
        if (((float)Screen.width / Screen.height) < (9f / 16))
        {
            transform.localScale = scale9_19;
            if (bChangePosition)
            {
                GetComponent<RectTransform>().anchoredPosition3D = pos9_19;
            }
        }
        else
        {
            transform.localScale = scale9_16;
            if (bChangePosition)
            {
                GetComponent<RectTransform>().anchoredPosition3D = pos9_16;
            }
        }
    }
}
