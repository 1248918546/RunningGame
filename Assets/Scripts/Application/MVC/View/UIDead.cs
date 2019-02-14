using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    //贿赂次数
    int m_BriberyTime;

    //贿赂所需coin
    public Text txtBribery;

    public override string Name => Consts.V_Dead;

    public int BriberyTime { get => m_BriberyTime; set => m_BriberyTime = value; }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        txtBribery.text = (500 + 500 * BriberyTime).ToString();
        gameObject.SetActive(true);
    }

    public override void HandleEvent(string name, object data)
    {

    }

    private void Awake()
    {
        BriberyTime = 0;
    }

    //点击拒绝贿赂按钮，直接跳到结算界面
    public void OnCancleClick()
    {
        SendEvent(Consts.E_ShowFinalUI);
    }

    //点击贿赂按钮
    public void OnBriberyClick()
    {
        CoinArgs e = new CoinArgs
        {
            coin = 500 + 500 * BriberyTime
        };
        SendEvent(Consts.E_ClickBribery, e);
    }
}
