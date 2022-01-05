using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Fence : MonoBehaviour
{
    [SerializeField]
    TuongDay[] listFence;
    public bool bIsClicked = false;
    [SerializeField]
    bool isOnWall = false;
    [SerializeField]
    Material offMat;
    Animator anim;
    private void Start()
    {
        bIsClicked = false;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (bIsClicked) return;
        if (isOnWall)
        {
            if (!other.CompareTag("Bullet")) return;
        }
        else
        {
            if (!other.CompareTag("Player")) return;
        }
        bIsClicked = true;
        SoundManage.Instance.Play_PluginClick();
        anim?.Play("in");
        for (int i = 0; i < listFence.Length; i++)
        {
            listFence[i].TurnOff();
        }
        transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = offMat;
    }
}
