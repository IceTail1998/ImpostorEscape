using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    Vector3 anchoredPos;
    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        Vector2 posDefault = new Vector2(Screen.width / 2, Screen.height / 6f);
        anchoredPos = ScreenPointToAnchoredPosition(posDefault);
        background.anchoredPosition = anchoredPos;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.anchoredPosition = anchoredPos;
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
    public override void TurnOff()
    {
        base.TurnOff();
        background.anchoredPosition = anchoredPos;
        //background.gameObject.SetActive(false);
    }

    internal void TurnOffStart()
    {
        base.TurnOff();
        background.anchoredPosition = anchoredPos;
        background.gameObject.SetActive(false);
    }
}