using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed;
    Rigidbody myBody;
    Transform thisTrans;
    [SerializeField]
    Transform ExplorePos;
    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        thisTrans = transform;
    }

    private void Update()
    {
        myBody.velocity = speed * thisTrans.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleWall")) return;
        if(other.CompareTag("Laser")) return;
        if (!other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            EffectManage.Instance.TurnOnExplore(ExplorePos.position);
            SoundManage.Instance.Play_Explore();
            if (other.CompareTag("Character"))
            {
                other.gameObject.GetComponent<Character>().Kill(tag);
            }
            else if (other.CompareTag("GreatWallChild"))
            {
                other.gameObject.GetComponentInParent<GreatWall>().TurnOffAll();
                Collider[] listAffected = Physics.OverlapSphere(ExplorePos.position, 2f);
                for (int i = 0; i < listAffected.Length; i++)
                {
                    if (listAffected[i].CompareTag("GreatWallChild"))
                    {
                        Rigidbody rgbd = listAffected[i].GetComponent<Rigidbody>();
                        if (rgbd != null)
                        {
                            rgbd.isKinematic = false;
                            rgbd.AddForce(thisTrans.forward * 500f, ForceMode.Force);
                        }
                    }
                }
            }
        }
    }
}
