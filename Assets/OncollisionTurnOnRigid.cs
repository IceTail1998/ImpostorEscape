using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncollisionTurnOnRigid : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;
    bool ok = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (ok) return;
        if (collision.collider.CompareTag("Player"))
        {
            ok = true;
            rigidBody.isKinematic = false;
        }
    }
}
