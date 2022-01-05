using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Wall : MonoBehaviour
{
    [SerializeField]
    Teleport_Wall otherTele;
    List<Transform> listHold = new List<Transform>();
    float waitTimer;
    [SerializeField]
    Transform posOutTrans;
    public bool hasPassed = false;
    private void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
    }
    public void Wait()
    {
        waitTimer = 1f;
    }
    public void Transition(Transform transit)
    {
        transit.forward = transform.up;
        transit.position = posOutTrans.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (waitTimer > 0) return;
        if (!other.CompareTag("Bullet")) return;
        hasPassed = true;
        otherTele.Wait();
        otherTele.Transition(other.transform);

    }
}
