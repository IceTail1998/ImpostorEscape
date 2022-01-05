using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    BoxCollider Collider;
    [SerializeField]
    GameObject ParticleDad;
    public bool bIsOn;
    [SerializeField]
    LayerMask layerCheck;
    private async Task Start()
    {
        //TurnOff();
        await Task.Yield();
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, layerCheck))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 5f);
            float distance = Vector3.Distance(transform.position, hit.point) / 5;
            if (Collider != null)
            {
                Vector3 size = Collider.size;
                size.z = distance;
                Collider.size = size;
                size = Collider.center;
                size.z = distance / 2;
                Collider.center = size;
            }
        }
        if (bIsOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }
    public void ChangeState()
    {
        if (bIsOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
    public void TurnOn()
    {
        bIsOn = true;
        Collider.gameObject.SetActive(true);
        ParticleDad.SetActive(true);
        SoundManage.Instance.PLay_Laser();
    }
    public void TurnOff()
    {
        bIsOn = false;
        Collider.gameObject.SetActive(false);
        ParticleDad.SetActive(false);
        SoundManage.Instance.Stop_Laser();
    }
}
