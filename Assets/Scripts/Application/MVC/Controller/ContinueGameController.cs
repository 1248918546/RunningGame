using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();

        UIBoard uIBoard = GetView<UIBoard>();
        if(uIBoard.Times < 0.01f)
        {
            uIBoard.Times += 20;
        }
        gm.IsPause = false;
        gm.IsPlay = true;
    }
}