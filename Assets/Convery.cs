using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convery : MonoBehaviour
{
    [SerializeField]
    Material mat;
    Vector2 offSet;
    [SerializeField]
    float offSetStep = 0.01f;
    List<Rigidbody> listHashCode = new List<Rigidbody>();
    [SerializeField]
    float force = 400f;
    private void Start()
    {
        offSet = Vector2.zero;
    }
    void Update()
    {
        if (mat != null)
        {
            offSet.y -= offSetStep;
            if (offSet.y <= -1)
            {
                offSet.y = 0f;
            }
            mat.SetTextureOffset("_MainTex", offSet);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Player.Instance.bIsConveyor)
        {
            Player.Instance.bIsConveyor = true;
            Player.Instance.conveyorName = name;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Player.Instance.bIsConveyor)
            {
                Player.Instance.bIsConveyor = true;
                Player.Instance.conveyorName = name;
            }
            if (Player.Instance.conveyorName.Equals(name))
            {
                other.GetComponent<Rigidbody>().AddForce(force * transform.right, ForceMode.Acceleration);
            }
        }
        else
        {
            other.GetComponent<Rigidbody>().AddForce(force * transform.right, ForceMode.Acceleration);
        }
        //else
        //{
        //}
        //if (!listHashCode.Contains(other.GetHashCode()))
        //{
        //    listHashCode.Add(other.GetHashCode());
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.bIsConveyor = false;
        }
        //if (listHashCode.Contains(other.GetHashCode()))
        //{
        //    listHashCode.Remove(other.GetHashCode());
        //}

    }
}
