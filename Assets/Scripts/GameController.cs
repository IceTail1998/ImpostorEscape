using UnityEngine;
using TMPro;

namespace DucPhung
{
    public class GameController : MonoBehaviour
    {
        #region Singleton

        public static GameController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        #endregion

        public delegate void GameEvents();
        public event GameEvents ReplayGame;
        public event GameEvents NextLevel;
        public event GameEvents WinGame;
        public event GameEvents PlayerWinAnim;
        public event GameEvents GameOver;

        [SerializeField] private GameObject GameOverObj;
        [SerializeField] private GameObject WinObj;
        [SerializeField] private TextMeshProUGUI levelCurrent;

        public bool isEvents { get; set; }

        public void NewLevel(int level)
        {
            levelCurrent.text = level.ToString();
        }

        //public void Call_ReplayGame()
        //{
        //    if (ReplayGame != null)
        //    {
        //        ReplayGame.Invoke();

        //        isEvents = false;
        //    }
        //}

        //public void Call_GameOver()
        //{
        //    if (GameOver != null)
        //    {
        //        isEvents = true;
        //        GameOverObj.SetActive(true);
        //        GameOver.Invoke();
        //    }
        //}

        //public void Call_NextLevel()
        //{
        //    if (NextLevel != null)
        //    {
        //        isEvents = false;
        //        NextLevel.Invoke();
        //    }
        //}

        //public void Call_WinGame()
        //{
        //    if (WinGame != null)
        //    {
        //        isEvents = true;
        //        WinObj.SetActive(true);
        //        WinGame.Invoke();
        //    }
        //}

        public void Call_PlayerWinAnim()
        {
            if (PlayerWinAnim != null)
            {
                PlayerWinAnim.Invoke();
            }
        }
    }
}
