using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField]
    GreatWall[] listGreatWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Player.Instance.OnGetBoost("Donut");
            for (int i = 0; i < listGreatWall.Length; i++)
            {
                listGreatWall[i].TurnOff();
            }
            SoundManage.Instance.PlayBoop();
        }
    }
}
