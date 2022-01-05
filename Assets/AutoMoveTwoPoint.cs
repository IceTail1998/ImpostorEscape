using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveTwoPoint : MonoBehaviour
{
    [SerializeField]
    Transform[] listPos;
    int curIndex = 0;
    int dir = 0;
    [SerializeField]
    float speed = .1f;
    Transform thisTrans;
    private void Start()
    {
        thisTrans = transform;
    }
    private void OnEnable()
    {
        dir = 0;
        curIndex = 0;
    }
    void Update()
    {
        if (Vector3.SqrMagnitude(thisTrans.position - listPos[curIndex].position) < 0.1f)
        {
            if(dir == 0)
            {
                if(curIndex >= listPos.Length -1)
                {
                    dir = 1;
                    curIndex--;
                }
                else
                {
                    curIndex++;
                }
            }
            else
            {
                if (curIndex <= 0)
                {
                    dir = 0;
                    curIndex++;
                }
                else
                {
                    curIndex--;
                }
            }
        }
        else
        {
            thisTrans.position = Vector3.MoveTowards(thisTrans.position, listPos[curIndex].position, speed);
        }
    }
}
