using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            SoundManage.Instance.PlayMedicineEat();
            Player.Instance.GainHideBoost();
        }
    }
}
