using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_WallTele : MonoBehaviour
{
    [SerializeField]
    Teleport_Wall teleCheck;
    [SerializeField]
    GameObject[] tut;
    private void Update()
    {
        if (teleCheck.hasPassed)
        {
            for (int i = 0; i < tut.Length; i++)
            {
                tut[i].SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

}
