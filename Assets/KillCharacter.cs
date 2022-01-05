using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCharacter : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.isHiding && other.transform.CompareTag("Player")) return;
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            other.GetComponent<Character>().Kill(tag);
            
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (Player.Instance.isHiding && other.transform.CompareTag("Player")) return;
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Character"))
        {
            other.transform.GetComponent<Character>().Kill(tag);
        }
    }
}
