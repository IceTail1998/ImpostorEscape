using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tung
{
    public class UIManager : MonoBehaviour
    {
        public GameObject PleaseWaitPanel;
        public static UIManager Instance;
        public UiActivce CurrentUIActive { get; set; }
        public UiActivce PreviousUIActive { get; set; }
        public bool IsPlayPanelOnTop { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            //Init();
            IsPlayPanelOnTop = true;
        }
        private void Init()
        {
            PleaseWaitPanelOff();
        }

        public enum UiActivce
        {
            Home = 1, Playing = 2, Setting = 3, Shop = 4, EndGame = 5, Pause = 6
        }
        public void PleaseWaitPanelOn()
        {
            PleaseWaitPanel.SetActive(true);
        }
        public void PleaseWaitPanelOff()
        {
            PleaseWaitPanel.SetActive(false);

        }
        public void SetPlayOnTop(bool isOnTop)
        {
            IsPlayPanelOnTop = isOnTop;
        }
        //private void Update()
        //{
        //    Vector2 bl = new Vector2(0, 0);
        //    Vector2 br = new Vector2(Screen.width, 0);
        //    Vector2 tl = new Vector2(0, Screen.height);
        //    Vector2 tr = new Vector2(Screen.width, Screen.height);
        //    Vector3 bl3 = UICam.ScreenToWorldPoint(bl);
        //    Vector3 br3 = UICam.ScreenToWorldPoint(br);
        //    Vector3 tl3 = UICam.ScreenToWorldPoint(tl);
        //    Vector3 tr3 = UICam.ScreenToWorldPoint(tr);
        //    Debug.DrawLine(bl3, tr3, Color.green);
        //    Debug.DrawLine(br3, tl3, Color.red);
        //}
    }
}
