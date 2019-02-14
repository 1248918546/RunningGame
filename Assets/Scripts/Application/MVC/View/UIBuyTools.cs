using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    GameModel gm;

    public Text txtGizmoMagnet;
    public Text txtGizmoInvincible;
    public Text txtGizmoMultiply;
    public Text txtCoin;

    public override string Name => Consts.V_BuyTools;

    public override void HandleEvent(string name, object data)
    {
    }

    private void Awake()
    {
        gm = GetModel<GameModel>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        ShowOrHide(gm.Magnet, txtGizmoMagnet);
        ShowOrHide(gm.Invincible, txtGizmoInvincible);
        ShowOrHide(gm.Multiply, txtGizmoMultiply);
        txtCoin.text = gm.Coin.ToString();
    }

    public void ShowOrHide(int i, Text txt)
    {
        if(i > 0)
        {
            txt.text = i.ToString();
            txt.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            txt.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnBuyMagnetClick(int coin = 100)
    {
        ToolsArgs e = new ToolsArgs
        {
            kind = ItemKind.MagnetItem,
            money = coin
        };
        SendEvent(Consts.E_BuyTools, e);
    }

    public void OnBuyInvincibleClick(int coin = 200)
    {
        ToolsArgs e = new ToolsArgs
        {
            kind = ItemKind.InvincibleItem,
            money = coin
        };
        SendEvent(Consts.E_BuyTools, e);
    }

    public void OnBuyMultiplyClick(int coin = 200)
    {
        ToolsArgs e = new ToolsArgs
        {
            kind = ItemKind.MultiplyItem,
            money = coin
        };
        SendEvent(Consts.E_BuyTools, e);
    }

    public void OnBuyRandomClick(int coin = 300)
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                OnBuyMagnetClick(coin);
                break;
            case 1:
                OnBuyInvincibleClick(coin);
                break;
            case 2:
                OnBuyMultiplyClick(coin);
                break;
            default:
                break;
        }
    }

    public void OnPlayClick()
    {
        Game.Instance.LoadLevel(4);
    }
}
