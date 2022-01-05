using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> listParticle;
    [SerializeField]
    bool isAutoHide = false;
    [SerializeField]
    float timerDelayHide = 1.7f;
    public virtual void Play()
    {
        Stop();
        for (int i = 0; i < listParticle.Count; i++)
        {
            listParticle[i].Play();
        }
        if (isAutoHide)
            Invoke("Deactive", timerDelayHide);
    }
    public virtual void Stop()
    {
        for (int i = 0; i < listParticle.Count; i++)
        {
            listParticle[i].Stop();
        }
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
