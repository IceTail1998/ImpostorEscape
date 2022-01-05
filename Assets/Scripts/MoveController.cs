using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private float moveSpeed;

    [SerializeField]
    private Rigidbody object_Rigidbody;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    CharacterController characterController;
    Transform thisTrans;
    public bool bIsMoving { get; private set; }
    internal bool bIsVenting;
    bool bIsJumping;
    private Vector3 moveDirection;
    Vector3 V3Zero;
    [SerializeField]
    DynamicJoystick joystick;
    [SerializeField]
    float minMoveJoystick = 0.02f;
    [SerializeField]
    float delayVent = 0.2f;
    float timerDelayVent;
    private void Start()
    {
        thisTrans = transform;
        V3Zero = Vector3.zero;

    }
    Ray rayCheckWall;
    RaycastHit hitInfor;
    private void FixedUpdate()
    {
        if (timerDelayVent > 0)
        {
            timerDelayVent -= Time.deltaTime;
        }
        if (GameController.Instance.IsPlaying)
        {
            if (joystick.IsMouseDown)
            {
                if (!GameController.Instance.isPLayerMoved)
                {
                    GameController.Instance.PlayerDidMove();
                }
                if (joystick.DirectionVector3.sqrMagnitude > minMoveJoystick)
                {
                    if (!bIsVenting && timerDelayVent <= 0)
                    {
                        moveDirection = joystick.DirectionVector3.normalized;
                        anim.SetBool("run", true);
                        Player.Instance.OnMovingAction();
                        Move();
                    }
                    else
                    {
                        if (!bIsJumping)
                        {
                            JumpOutVent();
                        }
                    }
                }
                else
                {
                    anim.SetBool("run", false);
                    SlowToStop();
                }
            }
            else
            {
                SlowToStop();
                anim.SetBool("run", false);
            }
        }
        else
        {
            anim.SetBool("run", false);
            joystick.TurnOff();
            SlowToStop();
        }
    }
    Vector3 temp;
    [SerializeField]
    LayerMask LayerCheckWall;
    private void Move()
    {
        bIsMoving = true;
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        //Vector3 rotation = lookRotation.eulerAngles;
        moveDirection.y = 0;
        temp = thisTrans.position;
        temp.y += 0.4f;
        rayCheckWall = new Ray(temp, thisTrans.forward);
        //Debug.DrawRay(rayCheckWall.origin, rayCheckWall.direction, Color.red, 2f);
        if (Physics.Raycast(rayCheckWall, out hitInfor, 2f, LayerCheckWall))
        {
            moveDirection = V3Zero;
        }
        else
        {
            if (Player.Instance.bIsConveyor)
            {
                object_Rigidbody.velocity = moveDirection * 675f * Time.deltaTime;
            }
            else
            {
                object_Rigidbody.MovePosition(object_Rigidbody.position + moveDirection * 8.925f * Time.deltaTime);
            }
        }
        thisTrans.rotation = Quaternion.Lerp(thisTrans.rotation, lookRotation, 1f);

        //object_Rigidbody.velocity = V3Zero;
        //characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        //MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }
    private void SlowToStop()
    {
        //if (!bIsMoving) return;
        //if (Player.Instance.bIsConveyor) return;
        object_Rigidbody.velocity = V3Zero;
        bIsMoving = false;
        Player.Instance.OnStopMoveAction();
    }
    float normalY;
    Vent currentVent;
    public void JumpVent(Vector3 posDes, Vent vent)
    {
        if (bIsVenting) return;
        SoundManage.Instance.Play_Jump();
        bIsVenting = true;
        currentVent = vent;
        normalY = transform.position.y;
        vent.DoOpenVent();
        bIsJumping = true;
        transform.DOJump(posDes, 7.5f, 1, .5f, false).OnComplete(() =>
        {
            vent.DoCloseVent();
            bIsJumping = false;
            timerDelayVent = delayVent;
        });
        anim.Play("Jump");
    }
    public void JumpOutVent()
    {
        if (currentVent == null) return;
        SoundManage.Instance.Play_Jump();
        currentVent.DoOpenVent();
        Vector3 des = transform.position;
        des.y = normalY;
        currentVent.DoOpenVent();
        bIsJumping = true;
        transform.DOJump(des, 7.5f, 1, .5f, false).OnComplete(() =>
        {
            currentVent.DoCloseVent();
            bIsJumping = false;
            bIsVenting = false;
            currentVent = null;
        });
        anim.Play("Jump");
    }
    public void Teleport(Vector3 pos)
    {
        SoundManage.Instance.Play_Jump();
        bIsJumping = true;
        Vector3 posTemp = pos;
        posTemp.y -= 2f;
        transform.position = posTemp;
        transform.DOMove(pos, .7f, false).OnComplete(() =>
        {
            bIsJumping = false;
        });
    }
    public void SetUp()
    {
        joystick.TurnOff();
        anim.SetBool("run", false);
        anim.ResetTrigger("win1");
        anim.ResetTrigger("win2");
        anim.ResetTrigger("win3");
        anim.Play("Idle");
        bIsMoving = false;
        bIsVenting = false;
        timerDelayVent = 0f;
    }
    public void SetupNewCharacter(Animator characterAnim)
    {
        anim = characterAnim;
        SetUp();
    }
    public void PlayerWinAnim()
    {
        int rd = Random.Range(1, 13);
        rd = Mathf.Clamp(rd, 1, 12);
        anim.SetTrigger("win" + rd);
        //if (rd == 1) anim.SetTrigger("win1");
        //else if (rd == 2) anim.SetTrigger("win2");
        //else if (rd == 3) anim.SetTrigger("win3");
    }
}