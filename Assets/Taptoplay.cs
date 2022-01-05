using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taptoplay : MonoBehaviour
{
    [SerializeField]
    DynamicJoystick joyStick;
    [SerializeField]
    GameObject DragTut;
    [SerializeField]
    GameObject TapToPlayObj;
    private void Start()
    {
        if (LevelManage.Instance.currentLevel <= 2)
        {
            DragTut.SetActive(true);
            TapToPlayObj.SetActive(false);
        }
        else
        {
            DragTut.SetActive(false);
            TapToPlayObj.SetActive(true);
        }
    }
    public void CheckShowButton()
    {
        if (LevelManage.Instance.currentLevel <= 2)
        {
            DragTut.SetActive(true);
            TapToPlayObj.SetActive(false);
        }
        else
        {
            DragTut.SetActive(false);
            TapToPlayObj.SetActive(true);
            if (!FirstOpenController.instance.IsHideTut())
            {
                FirstOpenController.instance.DoHideTut();
            }
        }
    }
    void Update()
    {
        if (joyStick.IsMouseDown)
        {
            gameObject.SetActive(false);
            PlayingPanel.Instance.ButtonTapToPlay();
        }
    }
}
