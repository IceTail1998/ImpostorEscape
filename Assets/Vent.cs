using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    bool bIsPlayerInside = false;
    [SerializeField]
    Transform PlayerPosDesTrans;
    Animator anim;
    private void Start()
    {
        bIsPlayerInside = false;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bIsPlayerInside) return;
        if (other.CompareTag("Player"))
        {
            bIsPlayerInside = true;
            Player.Instance.JumpVent(PlayerPosDesTrans.position, this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (bIsPlayerInside && other.transform.CompareTag("Player"))
        {
            bIsPlayerInside = false;
        }
    }
    public void DoOpenVent()
    {
        anim.Play("open");
    }
    public void DoCloseVent()
    {
        anim.Play("close");
    }
}
