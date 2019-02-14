using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{
    #region 常量

    const int InitCoin = 1000;

    #endregion

    #region 事件
    #endregion

    #region 字段

    bool m_IsPlay = true;
    bool m_IsPause = false;

    //技能时间
    int m_SkillTime = 5;
    int m_Magnet;
    int m_Multiply;
    int m_Invincible;

    //经验、等级
    int m_Grade;
    int m_Exp;

    //金币数
    int m_Coin;

    #endregion

    #region 属性

    public override string Name => Consts.M_GameModel;

    public bool IsPlay { get => m_IsPlay; set => m_IsPlay = value; }
    public bool IsPause { get => m_IsPause; set => m_IsPause = value; }
    public int SkillTime { get => m_SkillTime; set => m_SkillTime = value; }
    public int Magnet { get => m_Magnet; set => m_Magnet = value; }
    public int Multiply { get => m_Multiply; set => m_Multiply = value; }
    public int Invincible { get => m_Invincible; set => m_Invincible = value; }
    public int Grade { get => m_Grade; set => m_Grade = value; }
    public int Exp
    {
        get => m_Exp;
        set
        {
            while (value >= 10 + Grade * 10)
            {
                //升级
                //1.减少经验值
                value -= (10 + Grade * 10);
                //2.等级提高
                Grade++;
            }
            m_Exp = value;
        }
    }

    public int Coin { get => m_Coin; set => m_Coin = value; }

    #endregion

    #region 方法

    //初始化
    public void Init()
    {
        SkillTime = 5;
        Magnet = 1;
        Multiply = 2;
        Invincible = 3;
        Exp = 0;
        Grade = 0;
        Coin = InitCoin;
    }

    //买东西
    public bool BuyGoods(int coin)
    {
        if(coin <= Coin)
        {
            Coin -= coin;
            Debug.Log("现在还剩" + Coin + "块钱");
            return true;
        }
        return false;
    }

    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
