using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    #region Singleton

    public static CameraController instance;
    Quaternion defaultRotation;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] private float minDistance;
    [SerializeField] private float minDistance2;
    [SerializeField] private float moveDuration;
    [SerializeField] private float rotateDuration;
    [SerializeField] private float rotateSpeed;
    public Camera mainCam;
    public Camera uiCam;
    public Transform player { get; set; }
    public Transform target { get; set; }
    [SerializeField]
    Transform CamTransDadRotating;
    Transform thisTrans;
    Vector3 defaultPos;
    private void Start()
    {
        defaultPos = transform.position;
        mainCam = Camera.main;
        defaultRotation = transform.rotation;
        thisTrans = transform;
    }
    bool isRotating = false;
    public void GameWinAction()
    {
        CamTransDadRotating.position = player.transform.position;
        CamTransDadRotating.rotation = Quaternion.identity;
        transform.SetParent(CamTransDadRotating);
        float angle = Vector3.SignedAngle(transform.forward, player.forward, Vector3.up);
        Quaternion quadDes = Quaternion.LookRotation(-1 * player.forward);
        isRotating = true;
        Vector3 p1 = transform.position;
        Vector3 p2 = player.position;
        p1.y = 0;
        p2.y = 0;
        float dis = Vector3.Distance(p1, p2);

        Debug.Log("distance: " + dis);
        CamTransDadRotating.DORotateQuaternion(quadDes, 2f);
        //des = mainCam.fieldOfView;
        Debug.Log("camsize origin: " + des);
        //float delta = dis * 0.38f / 51.8f;
        //delta = Mathf.Abs(0.38f - delta);
        //delta = 0.38f + delta;
        //des = des * delta;
        Debug.Log("camsize zoom: " + des);
        //transform.DOMove(target.position, 2.2f);
        // move to player
        //Ray ray = new Ray(player.position, transform.position);
        //Vector3 point = ray.GetPoint(minDistance);

        //transform.DOMove(point, moveDuration).OnComplete(() =>
        //{
        //    float maxAngle = Vector3.Angle(transform.forward, -player.forward);
        //    StartCoroutine(Rolling(maxAngle));
        //});
        // rotate around player
    }
    float des = 0f;


    private IEnumerator Rolling(float maxAngle)
    {
        float angle = 0f;
        while (angle < maxAngle)
        {
            float angleSpeed = rotateSpeed * Time.deltaTime;
            transform.RotateAround(player.position, Vector3.up, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }
        transform.RotateAround(player.position, Vector3.up, maxAngle - angle);
        transform.DOMove(target.position, rotateDuration);
        transform.DORotate(target.eulerAngles, rotateDuration);
    }
    public void Handle_StartLevel()
    {
        isRotating = false;
        transform.SetParent(null);
        StopAllCoroutines();
        transform.DOKill();
        transform.position = defaultPos;
    }
    public void GameLoseAction()
    {
        Ray ray = new Ray(player.position, transform.position - player.position);
        Vector3 point = ray.GetPoint(minDistance);

        transform.DOMove(point, moveDuration).OnComplete(() => { EndGamePanel.Instance.ShowReplayButton(); });
    }
    Quaternion quadTemp;
    Quaternion quadTemp1;
    Vector3 dir;
    float timer = 0f;
    Vector3 offset = new Vector3(0, .5f, 0);
    Vector3 localDes = new Vector3(0, 3.5f, -15f);
    Vector3 localDesBig = new Vector3(0, 4.5f, -25f);
    private void Update()
    {
        if (isRotating)
        {
            timer += Time.deltaTime;
            thisTrans.LookAt(player.position + offset);
            if (Player.Instance.bIsBig)
            {
                thisTrans.localPosition = Vector3.Lerp(thisTrans.localPosition, localDesBig, .02f);
            }
            else
            {
                thisTrans.localPosition = Vector3.Lerp(thisTrans.localPosition, localDes, .02f);
            }
            //dir = thisTrans.position;
            //dir.y = target.position.y;
            //thisTrans.position = Vector3.Lerp(thisTrans.position, dir, .03f);
            //mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, des, .03f);
            //if(timer >= 2)
            //{
            //    isRotating = false;
            //}
            //quadTemp = Quaternion.LookRotation(target.forward);
            //quadTemp = transform.rotation;
            //dir = player.position - transform.position;
            //quadTemp = Quaternion.LookRotation(dir);
            //dir = quadTemp.eulerAngles;
            //dir.x = quadTemp1.eulerAngles.x;
            //quadTemp.eulerAngles = dir;
            //transform.rotation = Quaternion.Lerp(transform.rotation, quadTemp, 1f);

        }
    }
}