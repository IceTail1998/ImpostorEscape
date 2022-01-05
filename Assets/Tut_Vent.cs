using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Vent : MonoBehaviour
{
    bool checkP1 = false;
    [SerializeField]
    GameObject Tut1;
    [SerializeField]
    GameObject Tut2;
    // Update is called once per frame
    void Update()
    {
        if (!checkP1)
        {
            if (Player.Instance.IsVenting)
            {
                checkP1 = true;
                Tut1.SetActive(false);
                Tut2.SetActive(true);
            }
        }
        else
        {
            if (!Player.Instance.IsVenting)
            {
                Tut2.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
