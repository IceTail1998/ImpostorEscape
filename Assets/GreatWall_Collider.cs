using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWall_Collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GetComponentInParent<GreatWall>().TurnOffAll();
        }
    }
}
