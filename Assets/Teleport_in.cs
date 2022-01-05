using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_in : MonoBehaviour
{
    [SerializeField]
    Teleport_out Out;
    bool isIn= false;

    private void Start()
    {
        isIn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isIn) return;
        if (other.CompareTag("Player"))
        {
            isIn = true;
            Player.Instance.Teleport(Out.transform.position);
            Out.DoOpenDoor();
            //other.transform.position = Out.transform.position;
            Invoke("ResetValue", .6f);
        }
    }
    private void ResetValue()
    {
        isIn = false;
    }

}
