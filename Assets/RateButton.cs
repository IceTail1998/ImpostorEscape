using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateButton : MonoBehaviour
{
    [SerializeField]
    Image img;
    public void ChangeSprite(Sprite spr)
    {
        if (img != null)
        {
            img.sprite = null;
            img.sprite = spr;
            img.SetNativeSize();
        }
    }
}
