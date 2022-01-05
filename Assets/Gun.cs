using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float delayTimeShot = 1f;
    [SerializeField]
    float delayStartGun = .5f;
    float delayStartTimer;
    [SerializeField]
    Transform posBulletStart;

    private void Update()
    {
        if (Player.Instance.canShot)
        {
            if (delayStartTimer > 0)
            {
                delayStartTimer -= Time.deltaTime;
            }
            else
            {
                delayStartTimer = delayTimeShot;
                Vector3 dir = posBulletStart.forward;
                dir.y = 0;
                SoundManage.Instance.Play_Rocket();
                BulletManage.Instance.Shoot(posBulletStart.position, dir);
            }
        }
        else
        {
            delayStartTimer = delayStartGun;
        }
    }


}
