using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPositionCustom : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTrans;
    [SerializeField]
    Vector3 pos9_16;
    [SerializeField]
    Vector3 pos9_19;
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        float ratio = 9f / 16;
        if((Screen.width/Screen.height) < ratio)
        {
            rectTrans.anchoredPosition3D = pos9_19;
        }
        else
        {
            rectTrans.anchoredPosition3D = pos9_16;
        }
    }

}
