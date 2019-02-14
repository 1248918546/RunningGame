using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlay = false;

        //TOTO:显示游戏结束UI
        UIDead ui = GetView<UIDead>();
        ui.Show();
    }
}