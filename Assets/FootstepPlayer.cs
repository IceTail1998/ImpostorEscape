using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public void FootStep()
    {
        SoundManage.Instance.Play_FootStep();
    }
    public void Yeah()
    {
        //SoundManage.Instance.Play_Yeah();
    }
}
