using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWall : MonoBehaviour
{
    [SerializeField]
    GameObject Collider;
    [SerializeField]
    Rigidbody[] listRigidbodyChild;
    private void Start()
    {
        Collider.SetActive(false);
    }
    public void TurnOff()
    {
        Collider.SetActive(true);
        for (int i = 0; i < listRigidbodyChild.Length; i++)
        {
            listRigidbodyChild[i].gameObject.layer = 23;
        }
    }
    public void TurnOffAll()
    {
        for (int i = 0; i < listRigidbodyChild.Length; i++)
        {
            listRigidbodyChild[i].isKinematic = false;
            listRigidbodyChild[i].useGravity = true;
            listRigidbodyChild[i].gameObject.layer = 23;
        }
    }

}
