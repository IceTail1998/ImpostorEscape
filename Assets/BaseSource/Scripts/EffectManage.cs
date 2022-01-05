using System.Collections.Generic;
using UnityEngine;
public class EffectManage : MonoBehaviour
{
    public static EffectManage Instance;
    [SerializeField]
    private BaseEffect ExlporeEffectPref;

    [SerializeField]
    private List<BaseEffect> listExploreEff;
    [SerializeField]
    private BaseEffect CoinEffectPref;

    [SerializeField]
    private List<BaseEffect> listCoinEffect;
    [SerializeField]
    float dustOffsetY = 1f;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void TurnOnExplore(Vector3 pos)
    {
        pos.y += dustOffsetY;
        bool ok = false;
        for (int i = 0; i < listExploreEff.Count; i++)
        {
            if (!listExploreEff[i].gameObject.activeSelf)
            {
                listExploreEff[i].gameObject.SetActive(true);
                listExploreEff[i].transform.SetParent(transform);
                listExploreEff[i].transform.position = pos;
                listExploreEff[i].Play();
                ok = true;
                break;
            }
        }
        if (!ok)
        {
            BaseEffect a = Instantiate(ExlporeEffectPref, transform);
            listExploreEff.Add(a);
            a.transform.position = pos;
            a.gameObject.SetActive(true);
            a.Play();
        }
    }
    public void TurnOnExplore()
    {
        ExlporeEffectPref.Play();
        SoundManage.Instance.Play_Firework();
    }
    public void TurnOffExplore()
    {
        ExlporeEffectPref.Stop();
    }
    public void TurnOnCoinEffect(Vector3 pos)
    {
        bool ok = false;
        for (int i = 0; i < listCoinEffect.Count; i++)
        {
            if (!listCoinEffect[i].gameObject.activeSelf)
            {
                listCoinEffect[i].gameObject.SetActive(true);
                listCoinEffect[i].transform.SetParent(transform);
                listCoinEffect[i].transform.position = pos;
                listCoinEffect[i].Play();
                ok = true;
                break;
            }
        }
        if (!ok)
        {
            BaseEffect a = Instantiate(CoinEffectPref, transform);
            listCoinEffect.Add(a);
            a.transform.position = pos;
            a.gameObject.SetActive(true);
            a.Play();
        }
    }

}
