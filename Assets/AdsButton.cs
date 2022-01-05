using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    [SerializeField]
    Image btn_img;
    [SerializeField]
    Button btn;
    [SerializeField]
    Sprite btn_available_sprite;
    [SerializeField]
    Sprite btn_notAvailable_sprite;
    [SerializeField]
    GameObject availablePart;
    [SerializeField]
    GameObject notAvailablePart;
    [SerializeField]
    Animator animator;
    public void SetAvalable(bool available)
    {
        if (available)
        {
            btn.interactable = true;
            btn_img.sprite = null;
            btn_img.sprite = btn_available_sprite;
            availablePart.SetActive(true);
            notAvailablePart.SetActive(false);
            if (animator != null)
                animator.SetTrigger("on");
        }
        else
        {
            btn.interactable = false;
            btn_img.sprite = null;
            btn_img.sprite = btn_notAvailable_sprite;
            availablePart.SetActive(false);
            notAvailablePart.SetActive(true);
            if (animator != null)
                animator.SetTrigger("off");
        }
    }
}
