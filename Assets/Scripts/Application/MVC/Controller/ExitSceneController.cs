using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.sceneIndex)
        {
            case 1:
                break;

            case 2:
                break;

            case 3:

                break;

            case 4:
                //回收对象池里的所有对象
                Game.Instance.objectPool.ClearAll();
                break;
        }
    }
}
