using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChangePosition : MonoBehaviour
{
    [SerializeField]
    GameObject checkObj;
    [SerializeField]
    Transform transChange;
    [SerializeField]
    Transform transDes;
    void Update()
    {
        if (!checkObj.activeSelf)
        {
            transChange.position = transDes.position;
            gameObject.SetActive(false);
        }
    }
}
