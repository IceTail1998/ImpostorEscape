using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public static PetController Instance;
    Animator animator;
    Rigidbody myBody;
    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    Transform playerTransForPet;
    [SerializeField]
    Vector2 distanceToPlayer = new Vector2(1.7f, 2.2f);
    [SerializeField]
    float currentSpeed = 0;
    [SerializeField]
    float maxSpeed = 1f;
    Transform thisTrans;
    Vector3 tmpPosDes;
    Vector3 offSetToPlayer;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        thisTrans = transform;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentSpeed = maxSpeed * .4f;
    }
    float changeOffsetTimer = 0;
    bool isFollowingPlayer = false;
    float timerChangeState = 0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetupGame();
        }
        if (changeOffsetTimer > 0)
        {
            changeOffsetTimer -= Time.deltaTime;
        }
        else
        {
            changeOffsetTimer = Random.Range(15, 50);
            offSetToPlayer.x = Random.Range(distanceToPlayer.x, distanceToPlayer.y);
            offSetToPlayer.z = Random.Range(distanceToPlayer.x, distanceToPlayer.y);
        }
        tmpPosDes = playerTransForPet.position;
        //tmpPosDes = Vector3.MoveTowards(thisTrans.position, playerTransForPet.position, currentSpeed);
        //Debug.Log("distance to player: " + (tmpPosDes - thisTrans.position).sqrMagnitude);
        //if (timerChangeState > 0)
        //{
        //    timerChangeState -= Time.deltaTime;
        //}
        //else
        //{
        //    timerChangeState = 1f;

        //}
        if (!isFollowingPlayer)
        {
            if ((tmpPosDes - thisTrans.position).sqrMagnitude > .03f)
            {
                isFollowingPlayer = true;
            }
        }
        else
        {
            if ((tmpPosDes - thisTrans.position).sqrMagnitude < .03f)
            {
                currentSpeed = maxSpeed * .6f;
                isFollowingPlayer = false;
                myBody.velocity = Vector3.zero;
            }
        }
        if (isFollowingPlayer)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += 5 * Time.deltaTime;
            }
            else
            {
                currentSpeed = maxSpeed;
            }
            FollowPlayer();
        }
    }
    public void SetupGame()
    {
        float randX = Random.Range(distanceToPlayer.x, distanceToPlayer.y);
        float randZ = Random.Range(distanceToPlayer.x, distanceToPlayer.y);
        thisTrans.position = new Vector3(playerTrans.position.x + randX, playerTrans.position.y, playerTrans.position.z + randZ);

    }
    public void FollowPlayer()
    {
        thisTrans.forward = playerTrans.forward;
        //myBody.MovePosition(tmpPosDes);
        myBody.velocity = (tmpPosDes - thisTrans.position).normalized * currentSpeed;
    }
    public void SetUpNewPet(Animator anim)
    {

    }
}
