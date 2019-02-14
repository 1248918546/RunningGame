using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = data as ItemArgs;
        PlayerMove player = GetView<PlayerMove>();
        UIBoard uI = GetView<UIBoard>();

        GameModel gm = GetModel<GameModel>();

        switch (e.kind)
        {
            case ItemKind.InvincibleItem:
                Debug.Log("无敌");
                player.HitInvincible();
                uI.HitInvincible();
                gm.Invincible -= e.hitCount;
                uI.UpdateUI();
                break;
            case ItemKind.MultiplyItem:
                Debug.Log("加倍");
                player.HitMultiply();
                uI.HitMultiply();
                gm.Multiply -= e.hitCount;
                uI.UpdateUI();
                break;
            case ItemKind.MagnetItem:
                Debug.Log("磁铁");
                player.HitMagnet();
                uI.HitMagnet();
                gm.Magnet -= e.hitCount;
                uI.UpdateUI();
                break;
            default:
                break;
        }
    }
}