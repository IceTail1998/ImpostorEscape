using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tut_CheckIfActive : MonoBehaviour
{
   [SerializeField]
   UnityEvent eventZ;
    bool didCheck = false;
    [SerializeField]
    GameObject objectCheck;
    [SerializeField]
    bool CheckConditionActiveOrDeactive;

    void Update()
    {
        if (!didCheck)
        {
            if(objectCheck.activeInHierarchy == CheckConditionActiveOrDeactive)
            {
                didCheck = true;
                eventZ.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
