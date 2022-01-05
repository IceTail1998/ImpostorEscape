using UnityEngine;
using DG.Tweening;
using System;

public class Player : Character, ResetInt
{
    public static Player Instance;
    Transform thisTrans;
    [SerializeField] private Transform targetForCam;
    private Vector3 rootPos;
    private Vector3 rootAngle;
    [SerializeField]
    Vector3 BuffScale = new Vector3(1.2f, 1.2f, 1.2f);
    Vector3 NormalScale = new Vector3(5, 5, 5);
    [SerializeField]
    private MoveController moveController;
    [SerializeField]
    Animator animator_PLayer;
    [SerializeField] private ParticleSystem winFX;
    [SerializeField] private GameObject lightObj;
    //[SerializeField]
    //GameObject Gun;
    [SerializeField]
    Outline outline;
    public bool IsVenting { get { return moveController.bIsVenting; } }
    public bool isHiding { get; private set; }
    private bool bIsBoostHide { get; set; }
    bool hasGun;
    public bool canShot { get; private set; }
    Collider playerCollider;
    public string conveyorName;
    internal bool bIsConveyor;

    private void Awake()
    {
        thisTrans = transform;
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public override void Start()
    {
        base.Start();
        //moveController = GetComponent<MoveController>();
        NormalScale = new Vector3(5, 5, 5);
        playerCollider = GetComponent<Collider>();
        rootPos = thisTrans.position;
        rootAngle = thisTrans.localEulerAngles;
        CameraController.instance.player = thisTrans;
        CameraController.instance.target = targetForCam;
    }
    public void SetPosition(Vector3 pos)
    {
        thisTrans.position = pos;
    }
    public void SetUp(Vector3 pos)
    {
        winFX.Stop();
        winFX.Clear(true);
        playerCollider = GetComponent<Collider>();
        playerCollider.enabled = true;
        thisTrans.position = pos;
        lightObj.SetActive(false);
        transform.localScale = NormalScale;
        PetController.Instance?.SetupGame();
        SetUp();
    }
    public void WinAction()
    {
        playerCollider.enabled = false;
        moveController.PlayerWinAnim();
        winFX.Play();
        SoundManage.Instance.StartFirework();
        lightObj.SetActive(true);
        hasGun = false;
        canShot = false;
        //if (Gun.activeSelf)
        //{
        //    Gun.SetActive(false);
        //}
        RemoveHide();
    }
    public void SetUp()
    {
        thisTrans.SetParent(null);
        outline.enabled = false;
        thisTrans.localRotation = Quaternion.identity;
        bIsBoostHide = false;
        bIsBig = false;
        base.ResetBool();
        moveController.SetUp();
        CharacterAndAccessoryManage.Instance.EndHide();
        animator_PLayer.SetBool("hasGun", false);
        //Gun.SetActive(false);
        bIsConveyor = false;
        RemoveHide();
    }
    public void SetupNewCharacter(GameObject newCharacter)
    {
        newCharacter.transform.SetParent(transform);
        newCharacter.transform.localPosition = Vector3.zero;
        animator_PLayer = newCharacter.GetComponent<Animator>();
        moveController.SetupNewCharacter(animator_PLayer);
    }
    public override void Kill(string senderTag)
    {
        if (!bIsAlive) return;
        base.Kill(senderTag);
        if (senderTag.Equals("Laser") || senderTag.Equals("Observer"))
        {
            SoundManage.Instance.Play_HurtPlayer();
        }
        animator_PLayer.Play("Dead");
        GameController.Instance.LoseLevel();
    }
    internal bool bIsBig = false;
    public override void OnGetBoost(string boostName)
    {
        base.OnGetBoost(boostName);
        bIsBig = true;
        transform.DOScale(BuffScale, .3f);
    }
    public void JumpVent(Vector3 pos, Vent vent)
    {
        moveController.JumpVent(pos, vent);
    }
    public void Teleport(Vector3 pos)
    {
        moveController.Teleport(pos);
    }

    internal void SetupLose(Transform rotatePlayerObject)
    {
        if (hasGun)
        {
            hasGun = false;
            animator_PLayer.SetBool("hasGun", false);
            //Gun.SetActive(false);
        }
        thisTrans.SetParent(rotatePlayerObject);
        thisTrans.localRotation = Quaternion.identity;
        thisTrans.localEulerAngles = new Vector3(180, 0, 0);
        thisTrans.localPosition = new Vector3(0, 1, 0);
    }
    public void GainHideBoost()
    {
        bIsBoostHide = true;
    }
    public void OnMovingAction()
    {
        if (isHiding)
        {
            isHiding = false;
            gameObject.layer = 8;
            outline.enabled = false;
            CharacterAndAccessoryManage.Instance.EndHide();
        }
        if (canShot)
        {
            canShot = false;
        }
    }
    public void OnStopMoveAction()
    {
        if (bIsBoostHide)
        {
            if (!isHiding)
            {
                gameObject.layer = 2;
                isHiding = true;
                outline.enabled = true;
                CharacterAndAccessoryManage.Instance.DoHide();
            }
        }
        if (hasGun && !canShot)
        {
            canShot = true;
        }
    }
    private void RemoveHide()
    {
        bIsBoostHide = false;
        isHiding = false;
        outline.enabled = false;
        gameObject.layer = 8;
        CharacterAndAccessoryManage.Instance.EndHide();
    }
    public void GainGun()
    {
        hasGun = true;
        animator_PLayer.SetBool("hasGun", true);
        //Gun.SetActive(true);
    }

}