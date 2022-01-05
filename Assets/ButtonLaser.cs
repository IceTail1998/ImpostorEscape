using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLaser : MonoBehaviour
{
    bool bIsTouching;
    [SerializeField]
    Laser[] listLaser;
    public bool bIsOn = false;
    Animator anim;
    AudioSource audioSource;
    [SerializeField]
    bool isOnWall = false;
    private void Start()
    {
        bIsTouching = false;
        bIsOn = false;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (bIsTouching) return;
        if (isOnWall)
        {
            if ((!other.CompareTag("Bullet"))) return;
        }
        else
        {
            if ((!other.CompareTag("Player") && !other.CompareTag("Character"))) return;
        }
        if (bIsOn)
        {
            SoundManage.Instance.Play_DropComplete();
            anim.Play("off");
        }
        else
        {
            SoundManage.Instance.Play_PluginClick();
            anim.Play("on");
        }
        bIsOn = !bIsOn;
        for (int i = 0; i < listLaser.Length; i++)
        {
            listLaser[i].ChangeState();
        }
        if (!isOnWall)
            bIsTouching = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Character")) return;
        bIsTouching = false;
    }
}
