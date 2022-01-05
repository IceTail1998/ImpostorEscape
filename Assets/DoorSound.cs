using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    bool isPLayed = false;
    string lastDoorName;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (isPLayed) return;
    //    lastDoorName = collision.collider.name;
    //    SoundManage.Instance.Play_DoorOpen();
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (lastDoorName.Equals(collision.collider.name))
    //    {
    //        isPLayed = false;
    //    }        
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (isPLayed) return;
        isPLayed = true;
        lastDoorName = other.name;
        SoundManage.Instance.Play_DoorOpen();
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (lastDoorName.Equals(other.name))
        {
            isPLayed = false;
        }
    }
}
