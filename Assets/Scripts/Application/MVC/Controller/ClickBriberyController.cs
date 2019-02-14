using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBriberyController : Controller
{
    public override void Execute(object data)
    {
        CoinArgs e = data as CoinArgs;
        UIDead uIDead = GetView<UIDead>();
        GameModel gm = GetModel<GameModel>();

        //TODO
        //if(花钱成功)
        if(gm.BuyGoods(e.coin))
        {
            uIDead.Hide();
            uIDead.BriberyTime++;
            UIResume uIResume = GetView<UIResume>();
            uIResume.StartCount();
        }
        else
        {

        }
    }
}