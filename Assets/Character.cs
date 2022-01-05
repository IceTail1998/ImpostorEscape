using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour
{
    public bool bIsAlive { get; private set; }
    public virtual void Start()
    {
        bIsAlive = true;
    }
    public virtual void Kill(string senderTag)
    {
        bIsAlive = false;
    }
    public virtual void OnGetBoost(string boostName)
    {

    }
    public void ResetBool()
    {
        bIsAlive = true;
    }
}
