using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour, LevelMap
{
    [SerializeField]
    Transform playerStartPos;
    [SerializeField] private Transform startPoint;
    //[SerializeField] private Transform player;
    [SerializeField] private FieldOfView[] fieldOfViews;
    [SerializeField]
    float camSize = 65f;
    public float CamSize { get { return camSize; } }

    private ResetInt[] dynamicObjects;
    private void SetUpCam()
    {
        CameraController.instance.Handle_StartLevel();
        //Camera.main.transform.position = startPoint.position;
        //Camera.main.transform.eulerAngles = startPoint.eulerAngles;
    }

    public void OnStartLevel()
    {
        CameraZoom.ResetZoomHandle();
        CameraZoom.ZoomSize_Handle(CamSize);
        GameController.Instance.StartGame();
        Player.Instance.SetUp(playerStartPos.position);
        Debug.Log("Field of view count: " + fieldOfViews.Length);
        for (int i = 0; i < fieldOfViews.Length; i++)
        {
            fieldOfViews[i].SetUp();
        }
        dynamicObjects = GetComponentsInChildren<ResetInt>();
        SetUpCam();
    }

    public void Setup()
    {

    }

    public void TurnOffLevel()
    {

    }

    public void TurnOnLevel()
    {

    }

    public void OnFinishLevel()
    {
        if (!GameController.Instance.IsPlaying) return;
        StartCoroutine(FinishIE());
    }
    IEnumerator FinishIE()
    {
        GameController.Instance.EndGame();
        CameraController.instance.GameWinAction();
        CoinUI.Instance.TurnOffButton();
        PlayingPanel.Instance.Deactive();
        yield return new WaitForSeconds(.75f);
        SoundManage.Instance.Play_Yeah();
        SoundManage.Instance.PlayDanceMusic();
        yield return new WaitForSeconds(1.75f);
        GameController.Instance.WinLevel();
    }

}