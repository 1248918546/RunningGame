using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpController : Controller
{
    public override void Execute(object data)
    {
        //注册所有Controller
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneController));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneController));
        RegisterController(Consts.E_EndGame, typeof(EndGameController));
        RegisterController(Consts.E_PauseGame, typeof(PauseGameController));
        RegisterController(Consts.E_ResumeGame, typeof(ResumeGameController));
        RegisterController(Consts.E_HitItem, typeof(HitItemController));
        RegisterController(Consts.E_ShowFinalUI, typeof(ShowFinalUIController));
        RegisterController(Consts.E_ClickBribery, typeof(ClickBriberyController));
        RegisterController(Consts.E_ContinueGame, typeof(ContinueGameController));
        RegisterController(Consts.E_BuyTools, typeof(BuyToolsController));

        //注册所有model
        RegisterModel(new GameModel());

        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Init();

        //完成场景跳转
    }
}
