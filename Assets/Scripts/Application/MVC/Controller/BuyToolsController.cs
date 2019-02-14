using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyToolsController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        UIBuyTools ui = GetView<UIBuyTools>();
        ToolsArgs e = data as ToolsArgs;
        bool isBought =  gm.BuyGoods(e.money);
        Debug.Log(isBought);
        if(isBought)
        {
            switch (e.kind)
            {
                case ItemKind.InvincibleItem:
                    gm.Invincible++;
                    break;
                case ItemKind.MultiplyItem:
                    gm.Multiply++;
                    break;
                case ItemKind.MagnetItem:
                    gm.Magnet++;
                    break;
            }
            ui.UpdateUI();
        }
    }
}