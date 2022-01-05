using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    Transform thisTrans;
    [SerializeField]
    Vector3 RotateVector;
    [SerializeField]
    float rotateSpeeed = 1f;
    bool bAutoRotate;
    [SerializeField]
    Material bgMat;
    [SerializeField]
    float BGFloatingSpeed = 0.01f;
    Vector2 offSet = new Vector2(0,0);
    private void Start()
    {
        thisTrans = transform;
        bAutoRotate = true;
    }
    void Update()
    {
        if (bAutoRotate)
        {
            thisTrans.Rotate(RotateVector * rotateSpeeed * Time.deltaTime);
            offSet.x += BGFloatingSpeed;
            if(offSet.x == 1)
            {
                offSet.x = 0;
            }
            bgMat.SetTextureOffset("_MainTex", offSet);
        }
    }
    public void DoRotate()
    {
        bAutoRotate = true;
    }
}
