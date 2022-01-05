using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowTransform : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 offSet;
    Transform thisTrans;
    [SerializeField]
    Transform followTrans;
    void Start()
    {
        thisTrans = transform;
        offSet = thisTrans.position - followTrans.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        thisTrans.position = followTrans.position + offSet;
    }
}
