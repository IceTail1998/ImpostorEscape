using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Node_Progress : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image nodeImage;
    public void UpdateNodeData(string data, Sprite spr)
    {
        if(text != null)
        {
            text.text = string.Empty + data;
        }
        if(nodeImage != null)
        {
            nodeImage.sprite = null;
            nodeImage.sprite = spr;
        }
    }
}
