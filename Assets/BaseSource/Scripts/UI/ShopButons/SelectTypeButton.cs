using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTypeButton : MonoBehaviour
{
    [SerializeField]
    GameObject Cover;
    public void OnSelect()
    {
        Cover?.SetActive(false);
    }
    public void OnDeselect()
    {
        Cover?.SetActive(true);
    }
}
