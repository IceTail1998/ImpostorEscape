using UnityEngine;
using System.Collections;

public class Observer : Character, ResetInt
{
    [Header("Moving")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float leftRange;
    [SerializeField] private float rightRange;
    public bool isUpDown = false;
    private bool isRotating;
    [SerializeField]
    float bodyRadius = 0.2f;
    private Vector3 rootPos;
    private Vector3 rootAngle;
    Collider thisCollider;
    private Animator anim;
    bool bIsDoingAttack;
    float startPosDimension;
    float endPosDimension;
    [SerializeField]
    LayerMask layerRaycastRoad;
    [SerializeField]
    bool isWaitPlayerMove = false;
    [SerializeField]
    MoveType moveType;
    [SerializeField]
    float angleRotate = 60f;
    [SerializeField]
    float timeStayStill = 2f;
    [SerializeField]
    int startRotateIndexStayStill = 1;
    [SerializeField]
    Transform movePointsDad;
    [SerializeField]
    bool moveClockDirection = true;
    [SerializeField]
    float timeDelayMovePoint = .5f;
    [SerializeField]
    int startIndex = 1;
    [SerializeField]
    float startDelayTime = 0f;
    [SerializeField]
    float delayAfterPlayerMove = 0f;
    [SerializeField]
    bool bIsUseRigidBody = false;
    float timerDelayMovePoint = 0f;
    Transform[] points;
    bool isMoveWithPoints;
    float timerStayStill = 0f;
    bool isMoving = false;
    bool isStay;
    bool isStayAllTime;
    bool isRotateAllTime;
    bool isMoveRight;
    Transform thisTrans;
    Rigidbody myBody;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        isMoving = false;
        anim.SetBool("run", false);
        if (transform.forward.x < -.5f || transform.forward.z < -.5f)
        {
            isMoveRight = false;
        }
        else
        {
            isMoveRight = true;
        }
    }
    
   
    public override void Start()
    {
        StartCoroutine(StartIE());
    }
    IEnumerator StartIE()
    {
        yield return new WaitForEndOfFrame();
        isInit = true;
        base.Start();
        thisCollider = GetComponent<Collider>();
        myBody = GetComponent<Rigidbody>();
        thisTrans = transform;
        rootPos = thisTrans.position;
        startPosDimension = thisTrans.position.x;
        timerStayStill = timeStayStill;
        timerDelayMovePoint = 0f;
        isStayAllTime = false;
        if (bIsUseRigidBody)
        {
            myBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            myBody.isKinematic = false;
        }
        else
        {
            myBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            myBody.isKinematic = true;
        }

        switch (moveType)
        {
            case MoveType.MoveNormal:
                isStay = false;
                isMoveWithPoints = false;
                Vector3 posRay = thisTrans.position;
                posRay.y += 0.2f;
                Ray r = new Ray(posRay, thisTrans.forward);
                RaycastHit hit;
                if (Physics.Raycast(r, out hit, 100f, layerRaycastRoad))
                {
                    //Debug.LogError(hit.thisTrans.name);
                    Debug.DrawLine(posRay, hit.point, Color.white, 3f);
                    //Debug.DrawRay(hit.point, r.direction, Color.red, 5f);
                    if (isUpDown)
                    {
                        startPosDimension = thisTrans.position.z;
                        if (hit.point.z > startPosDimension)
                        {
                            endPosDimension = hit.point.z - bodyRadius;
                            leftRange = startPosDimension;
                            rightRange = endPosDimension;
                        }
                        else
                        {
                            endPosDimension = hit.point.z + bodyRadius;
                            leftRange = endPosDimension;
                            rightRange = startPosDimension;
                        }
                    }
                    else
                    {
                        if (hit.point.x > startPosDimension)
                        {
                            endPosDimension = hit.point.x - bodyRadius;
                            leftRange = startPosDimension;
                            rightRange = endPosDimension;
                        }
                        else
                        {
                            endPosDimension = hit.point.x + bodyRadius;
                            leftRange = endPosDimension;
                            rightRange = startPosDimension;
                        }
                    }
                    Debug.Log("Init position observer!");
                }
                else
                {
                    Debug.Log("Failed to init position observer!");
                }
                break;
            case MoveType.MovePoints:
                isStay = false;
                if (movePointsDad != null)
                {
                    isMoveWithPoints = true;
                    points = movePointsDad.GetComponentsInChildren<Transform>();
                    //float minDis = Vector3.SqrMagnitude(thisTrans.position - points[0].position);
                    //for (int i = 0; i < points.Length; i++)
                    //{
                    //    int c = i;
                    //    if (minDis > Vector3.SqrMagnitude(thisTrans.position - points[c].position))
                    //    {
                    //        startIndex = c;
                    //        minDis = Vector3.SqrMagnitude(thisTrans.position - points[c].position);
                    //    }
                    //}
                }
                else
                {
                    isMoveWithPoints = false;
                    posRay = thisTrans.position;
                    posRay.y += 0.2f;
                    r = new Ray(posRay, thisTrans.forward);
                    if (Physics.Raycast(r, out hit, 100f, layerRaycastRoad))
                    {
                        //Debug.LogError(hit.thisTrans.name);
                        Debug.DrawLine(posRay, hit.point, Color.white, 3f);
                        //Debug.DrawRay(hit.point, r.direction, Color.red, 5f);
                        if (isUpDown)
                        {
                            startPosDimension = thisTrans.position.z;
                            if (hit.point.z > startPosDimension)
                            {
                                endPosDimension = hit.point.z - bodyRadius;
                                leftRange = startPosDimension;
                                rightRange = endPosDimension;
                            }
                            else
                            {
                                endPosDimension = hit.point.z + bodyRadius;
                                leftRange = endPosDimension;
                                rightRange = startPosDimension;
                            }
                        }
                        else
                        {
                            if (hit.point.x > startPosDimension)
                            {
                                endPosDimension = hit.point.x - bodyRadius;
                                leftRange = startPosDimension;
                                rightRange = endPosDimension;
                            }
                            else
                            {
                                endPosDimension = hit.point.x + bodyRadius;
                                leftRange = endPosDimension;
                                rightRange = startPosDimension;
                            }
                        }
                        Debug.Log("Init position observer!");
                    }
                    else
                    {
                        Debug.Log("Failed to init position observer!");
                    }
                }
                break;
            case MoveType.StayAlone:
                isRotateAllTime = false;
                isStayAllTime = true;
                isStay = true;
                break;
            case MoveType.StayRotate90:
                isRotateAllTime = false;
                isStay = true;
                break;
            case MoveType.StayRotate180:
                isRotateAllTime = false;
                isStay = true;
                break;
            case MoveType.StayRotate360:
                isRotateAllTime = true;
                isStay = true;
                break;
            case MoveType.StayRotateCustom:
                isRotateAllTime = false;
                isStay = true;
                break;
        }


        rootAngle = thisTrans.localEulerAngles;
        bIsDoingAttack = false;
        if (!isWaitPlayerMove && (moveType == MoveType.MoveNormal || moveType == MoveType.MovePoints))
        {
            anim.SetBool("run", true);
        }
    }
    Vector3 dirMove;
    bool isInit = false;
    private void Update()
    {
        if (!isInit) return;
        if (GameController.Instance.IsPlaying && bIsAlive && !isStayAllTime)
        {
            if (startDelayTime > 0)
            {
                startDelayTime -= Time.deltaTime;
            }
            else
            {
                if (isStay)
                {
                    if (isRotateAllTime)
                    {
                        isRotating = true;
                        if (startRotateIndexStayStill > 0)
                        {
                            thisTrans.Rotate(thisTrans.up, rotationSpeed * Time.deltaTime);
                        }
                        else
                        {
                            thisTrans.Rotate(thisTrans.up, -rotationSpeed * Time.deltaTime);
                        }

                    }
                    else
                    {
                        if (timerStayStill > 0)
                        {
                            timerStayStill -= Time.deltaTime;
                        }
                        else
                        {
                            timerStayStill = timeStayStill;
                            if (startRotateIndexStayStill > 0)
                            {
                                startRotateIndexStayStill = 0;
                                if (moveType == MoveType.StayRotate90)
                                {
                                    StartCoroutine(Rotating90Degree(Direction.left));
                                }
                                else if (moveType == MoveType.StayRotate180)
                                {
                                    StartCoroutine(Rotating(Direction.left));
                                }
                                else
                                {
                                    StartCoroutine(Rotating(angleRotate, false));
                                }
                            }
                            else
                            {
                                startRotateIndexStayStill = 1;
                                if (moveType == MoveType.StayRotate90)
                                {
                                    StartCoroutine(Rotating90Degree(Direction.right));
                                }
                                else if (moveType == MoveType.StayRotate180)
                                {
                                    StartCoroutine(Rotating(Direction.right));
                                }
                                else
                                {
                                    StartCoroutine(Rotating(angleRotate, true));
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isWaitPlayerMove)
                    {
                        if (GameController.Instance.isPLayerMoved)
                        {
                            if (delayAfterPlayerMove > 0)
                            {
                                delayAfterPlayerMove -= Time.deltaTime;
                            }
                            else
                            {
                                if (!isMoving)
                                {
                                    isMoving = true;
                                    anim.SetBool("run", true);
                                }
                                if (isMoveWithPoints)
                                {
                                    if (Vector3.SqrMagnitude(thisTrans.position - points[startIndex].position) < .1f)
                                    {
                                        if (!moveClockDirection)
                                        {
                                            startIndex++;
                                            startIndex = startIndex % points.Length;
                                        }
                                        else
                                        {
                                            startIndex--;
                                            if (startIndex < 0)
                                            {
                                                startIndex = points.Length - 1;
                                            }
                                        }
                                        timerDelayMovePoint = timeDelayMovePoint;
                                    }
                                    else
                                    {
                                        dirMove = points[startIndex].position;
                                        dirMove.y = thisTrans.position.y;
                                        dirMove = dirMove - thisTrans.position;
                                        thisTrans.forward = Vector3.Lerp(thisTrans.forward, dirMove.normalized, .25f);
                                        if (timerDelayMovePoint > 0)
                                        {
                                            timerDelayMovePoint -= Time.deltaTime;
                                        }
                                        else
                                        {
                                            if (bIsUseRigidBody)
                                            {
                                                myBody.velocity = thisTrans.forward * moveSpeed;
                                            }
                                            else
                                            {
                                                thisTrans.position += thisTrans.forward * moveSpeed * Time.deltaTime;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Movement();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isMoveWithPoints)
                        {
                            if (Vector3.SqrMagnitude(thisTrans.position - points[startIndex].position) < .05f)
                            {
                                if (!moveClockDirection)
                                {
                                    startIndex++;
                                    startIndex = startIndex % points.Length;
                                }
                                else
                                {
                                    startIndex--;
                                    if (startIndex < 0)
                                    {
                                        startIndex = points.Length - 1;
                                    }
                                }
                                timerDelayMovePoint = timeDelayMovePoint;
                            }
                            else
                            {
                                dirMove = points[startIndex].position;
                                dirMove.y = thisTrans.position.y;
                                dirMove = dirMove - thisTrans.position;
                                thisTrans.forward = Vector3.Lerp(thisTrans.forward, dirMove.normalized, .25f);
                                if (timerDelayMovePoint > 0)
                                {
                                    timerDelayMovePoint -= Time.deltaTime;
                                }
                                else
                                {
                                    if (bIsUseRigidBody)
                                    {
                                        myBody.velocity = thisTrans.forward * moveSpeed;
                                    }
                                    else
                                    {
                                        thisTrans.position += thisTrans.forward * moveSpeed * Time.deltaTime;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Movement();
                        }
                    }
                }
            }
        }
    }
    Vector3 pos;
    [SerializeField]
    bool isReverseFirst = false;
    [SerializeField]
    bool isReverseSecond = false;
    private void Movement()
    {
        if (!isRotating && !bIsDoingAttack)
        {
            if (bIsUseRigidBody)
            {
                myBody.velocity = thisTrans.forward * moveSpeed;
            }
            else
            {
                thisTrans.position += thisTrans.forward * moveSpeed * Time.deltaTime;

            }
            if (!isUpDown)
            {
                if (!isMoveRight && thisTrans.position.x < leftRange)
                {
                    pos = thisTrans.position;
                    pos.x = leftRange;
                    thisTrans.position = pos;
                    if (isReverseFirst)
                    {
                        StartCoroutine(Rotating(Direction.right));

                    }
                    else
                    {
                        StartCoroutine(Rotating(Direction.left));
                    }
                }
                else if (isMoveRight && thisTrans.position.x > rightRange)
                {
                    pos = thisTrans.position;
                    pos.x = rightRange;
                    thisTrans.position = pos;
                    if (isReverseSecond)
                    {
                        StartCoroutine(Rotating(Direction.left));
                    }
                    else
                    {
                        StartCoroutine(Rotating(Direction.right));
                    }
                }
            }
            else
            {
                if (!isMoveRight && thisTrans.position.z < leftRange)
                {
                    pos = thisTrans.position;
                    pos.z = leftRange;
                    thisTrans.position = pos;
                    if (isReverseFirst)
                    {
                        StartCoroutine(Rotating(Direction.right));

                    }
                    else
                    {
                        StartCoroutine(Rotating(Direction.left));
                    }
                }
                else if (isMoveRight && thisTrans.position.z > rightRange)
                {
                    pos = thisTrans.position;
                    pos.z = rightRange;
                    thisTrans.position = pos;
                    if (isReverseSecond)
                    {
                        StartCoroutine(Rotating(Direction.left));
                    }
                    else
                    {
                        StartCoroutine(Rotating(Direction.right));
                    }
                }
            }

        }
    }

    private IEnumerator Rotating(Direction direction)
    {
        isRotating = true;
        isMoveRight = !isMoveRight;
        myBody.velocity = Vector3.zero;
        float angle = 0f;
        Vector3 axis = Vector3.zero;
        if (direction == Direction.left) axis = Vector3.down;
        else axis = Vector3.up;
        float angleSpeed = rotationSpeed * Time.deltaTime;
        while (angle < 180f)
        {
            thisTrans.RotateAround(thisTrans.position, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }
        thisTrans.RotateAround(thisTrans.position, axis, 180f - angle);
        isRotating = false;
    }
    private IEnumerator Rotating(float angle, bool isClockDirection)
    {
        isRotating = true;
        myBody.velocity = Vector3.zero;
        float angleT = 0f;
        Vector3 axis = Vector3.zero;
        if (isClockDirection)
            axis = Vector3.up;
        else
            axis = Vector3.down;
        float angleSpeed = rotationSpeed * Time.deltaTime;
        while (angleT < angle)
        {
            thisTrans.Rotate(axis, angleSpeed);
            angleT += angleSpeed;
            yield return null;
        }
        thisTrans.Rotate(axis, angle - angleT);
        isRotating = false;
    }
    private IEnumerator Rotating90Degree(Direction direction)
    {
        isRotating = true;
        myBody.velocity = Vector3.zero;
        float angle = 0f;
        Vector3 axis = Vector3.zero;
        if (direction == Direction.left) axis = Vector3.down;
        else axis = Vector3.up;
        float angleSpeed = rotationSpeed * Time.deltaTime;
        while (angle < 90f)
        {
            thisTrans.RotateAround(thisTrans.position, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }
        thisTrans.RotateAround(thisTrans.position, axis, 90f - angle);
        isRotating = false;
    }
    public void SetUp()
    {
        thisTrans.position = rootPos;
        myBody.velocity = Vector3.zero;
        thisTrans.localEulerAngles = rootAngle;
        if (!isWaitPlayerMove && !isStay)
            anim.SetBool("run", true);
    }

    private enum Direction { left, right }

    private void GameOver()
    {
        anim.SetBool("run", false);
    }

    public void SetUp(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }
    public override void Kill(string senderTag)
    {
        base.Kill(senderTag);
        Debug.Log("Observed killed");
        if (senderTag.Equals("Laser"))
        {
            SoundManage.Instance.Play_HurtEnemy();
        }
        anim.Play("Dead");
        thisCollider.enabled = false;
        GetComponentInChildren<FieldOfView>().OffView();
    }
    public void DoAttack()
    {
        StopAllCoroutines();
        bIsDoingAttack = true;
        anim.SetBool("run", false);
        anim.Play("Attack");
    }
}
public enum MoveType
{
    MoveNormal, MovePoints, StayAlone, StayRotate90, StayRotate180, StayRotate360, StayRotateCustom
}
