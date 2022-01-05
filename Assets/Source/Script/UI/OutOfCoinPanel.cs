using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCoinPanel : BasePanel
{
    public static OutOfCoinPanel Instance;
    // Start is called before the first frame update
    [SerializeField]
    Transform AdsCoinPos;
    [SerializeField]
    Transform IAPCoinTrans;
    void Start()
    {
        base.Start_BasePanel();
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void ButtonClose()
    {
        Deactive();
    }
    public void ButtonAds()
    {
        //MasterControl.Instance.ShowRewardedAd(r =>
        //{
        //    if (r)
        //    {
        //        if (FirstOpenController.instance.IsOpenFirst)
        //        {
        //            FireBaseManager.Instance.LogEvent("REWARDED_FIRST_SESSION ");
        //        }
        //        FireBaseManager.Instance.LogEvent("REWARDED_COIN_ENDGAME");
        //        CoinManage.AddGem(200);
        //        CoinAddEffectUI.Instance.Show(AdsCoinPos.position, CoinUI.Instance.CoinPos);
        //        SoundManage.Instance.Play_CoinGain();
        //        CoinUI.Instance.UpdateCoin();
        //        Deactive();
        //    }
        //});
    }
    public void IAPButtonSuccess()
    {
        CoinManage.AddGem(2000);
        CoinUI.Instance.UpdateCoin();
        CoinAddEffectUI.Instance.Show(IAPCoinTrans.position, CoinUI.Instance.CoinPos);
        SoundManage.Instance.Play_CoinGain();
        Deactive();
    }
}
