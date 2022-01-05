using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRotateObj : MonoBehaviour
{
    [SerializeField]
    Transform ObjectToRotate;
    [SerializeField]
    float rotateSpeed = 0.5f;
    bool bIsTouching = false;
    Vector2 StartPos;
    public void OnPointerDownAction()
    {
        if (Input.touchCount == 0) return;
        bIsTouching = true;
        StartPos = Input.GetTouch(0).position;
    }
    Vector2 newPos;
    void Update()
    {
        if (Input.touchCount > 0 && bIsTouching)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                OnPointerUpAction();
            }
            else
            {
                newPos = Input.GetTouch(0).deltaPosition;
                ObjectToRotate.Rotate(new Vector3(0, -newPos.x * rotateSpeed, 0));
            }
        }
        else
        {
            ObjectToRotate.Rotate(new Vector3(0, 1 * rotateSpeed, 0));

        }
    }

    private void OnPointerUpAction()
    {
        if (bIsTouching)
        {
            bIsTouching = false;

        }
    }
}
