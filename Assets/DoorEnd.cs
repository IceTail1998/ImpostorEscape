using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnd : MonoBehaviour
{
    bool bIsOk = false;
    private void OnTriggerEnter(Collider other)
    {
        if (bIsOk) return;
        if (other.CompareTag("Player"))
        {
            //BitPlay.BitPlayManager.Instance.RegisterValidAction(BitPlay.ActionType.Play);
            bIsOk = true;
            GetComponent<Animator>().Play("in");
            SoundManage.Instance.Play_DoorEndOpen();
        }
    }
}
