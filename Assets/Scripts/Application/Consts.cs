using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    //事件名
    public const string E_ExitScene = "E_ExitScene"; //ScenesArgs
    public const string E_EnterScene = "E_EnterScene"; //ScenesArgs
    public const string E_StartUp = "E_StartUp";
    public const string E_EndGame = "E_EndGame";
    public const string E_PauseGame = "E_PauseGame";
    public const string E_ResumeGame = "E_ResumeGame";

    /// <summary>
    /// UI相关
    /// </summary>
    public const string E_UpdateDistance = "E_UpdateDistance";  //DistanceArgs
    public const string E_UpdateCoin = "E_UpdateCoin";        //CoinArgs
    public const string E_AddTime = "E_AddTime";
    public const string E_HitItem = "E_HitItem";   //ItemArgs
    //射门触发
    public const string E_HitGoalTrigger = "E_HitGoalTrigger";
    public const string E_ClickGoalBtn = "E_ClickGoalBtn";
    public const string E_ShootGoal = "E_ShootGoal";
    //结算
    public const string E_ShowFinalUI = "E_ShowFinalUI";
    //贿赂
    public const string E_ClickBribery = "E_ClickBribery";  //CoinArgs
    //Resume播放完毕，继续游戏
    public const string E_ContinueGame = "E_ContinueGame";
    //买道具
    public const string E_BuyTools = "E_BuyTools"; //ToolsArgs

    //model名
    public const string M_GameModel = "M_GameModel";

    //view名
    public const string V_PlayerMove = "V_PlayerMove";
    public const string V_PlayerAnimation= "V_PlayerAnimation";
    public const string V_MainMenu = "V_MainMenu";
    public const string V_Board = "V_Board";
    public const string V_Pause = "V_Pause";
    public const string V_Dead = "V_Dead";
    public const string V_Resume = "V_Resume";
    public const string V_FinalScore = "V_FinalScore";
    public const string V_BuyTools = "V_BuyTools";
}

public enum InputDirection
{
    NULL,
    Right,
    Left,
    Down,
    Up
}

//技能种类
public enum ItemKind
{
    InvincibleItem,
    MultiplyItem,
    MagnetItem
}