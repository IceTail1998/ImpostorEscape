using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    GameObject activeStatusObject;
    [SerializeField]
    GameObject deactiveStatusObject;
    [SerializeField]
    Image iconImage;
    [SerializeField]
    Sprite Sprite_On;
    [SerializeField]
    Sprite Sprite_Off;
    public void OnClick()
    {
        if (SoundManage.Instance.IsSoundOn)
        {
            if (activeStatusObject == null) return;
            SoundManage.Instance.SetSoundActive(false);
            activeStatusObject.SetActive(false);
            deactiveStatusObject.SetActive(true);
            if (iconImage != null)
            {
                iconImage.sprite = null;
                iconImage.sprite = Sprite_Off;
                iconImage.SetNativeSize();
            }
        }
        else
        {
            if (activeStatusObject == null) return;
            SoundManage.Instance.SetSoundActive(true);
            activeStatusObject.SetActive(true);
            deactiveStatusObject.SetActive(false);
            if (iconImage != null)
            {
                iconImage.sprite = null;
                iconImage.sprite = Sprite_On;
                iconImage.SetNativeSize();
            }
        }
    }

    public void FetchData()
    {
        if (SoundManage.Instance.IsSoundOn)
        {
            if (activeStatusObject == null) return;
            activeStatusObject.SetActive(true);
            deactiveStatusObject.SetActive(false);
        }
        else
        {
            if (activeStatusObject == null) return;
            activeStatusObject.SetActive(false);
            deactiveStatusObject.SetActive(true);
        }
    }

}
