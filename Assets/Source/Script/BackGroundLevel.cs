﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if(mr != null)
        {
            mr.sharedMaterial = MaterialManage.Instance.GetBackGroundMaterial();
        }
    }

}
