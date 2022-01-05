using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuongDay : MonoBehaviour
{
    [SerializeField]
    List<GameObject> listObjectBlock;
    [SerializeField]
    GameObject Collider;
    public bool bIsOn { get; private set; }
    private void Start()
    {
        bIsOn = true;
    }
    public void TurnOff()
    {
        bIsOn = false;
        for(int i=0; i<listObjectBlock.Count; i++)
        {
            listObjectBlock[i].SetActive(false);
        }
        Collider.SetActive(false);
    }
}
