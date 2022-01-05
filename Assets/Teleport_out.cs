using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_out : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Animator anim;
    public void DoOpenDoor()
    {
        anim.Play("in");
    }
}
