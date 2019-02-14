using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFinalUIController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();

        UIBoard uIBoard = GetView<UIBoard>();
        UIFinalScore uIFinalScore = GetView<UIFinalScore>();
        UIDead uIDead = GetView<UIDead>();

        uIDead.Hide();
        uIBoard.Hide();
        uIFinalScore.Show();

        //更新Exp
        gm.Exp += uIBoard.Coin + (uIBoard.Distance * (uIBoard.Goal + 1));
        uIFinalScore.UpdateUI(uIBoard.Distance, uIBoard.Coin, uIBoard.Goal, gm.Exp, gm.Grade);
    }
}