using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{

    #region 常量

    public const float startTime = 50.00f;
    public const float addedTime = 20;

    #endregion

    #region 事件
    #endregion

    #region 字段

    GameModel gm;

    int m_Coin = 0;
    int m_Distance = 0;
    int m_Goal = 0;
    float m_Time;

    public Text txtCoin;
    public Text txtDistance;
    public Text txtTimer;

    public Text txtGizmoMagnet;
    public Text txtGizmoMultiply;
    public Text txtGizmoInvincible;

    public Slider sliTimer;

    public Button btnMagnet;
    public Button btnMultiply;
    public Button btnInvincible;

    //射门
    public Slider sliGoal;
    public Button btnGoal;

    //协程
    IEnumerator InvincibleCor;
    IEnumerator MagnetCor;
    IEnumerator MultiplyCor;

    #endregion

    #region 属性

    public override string Name => Consts.V_Board;

    public int Coin
    {
        get => m_Coin;
        set
        {
            m_Coin = value;
            txtCoin.text = value.ToString();
        }
    }
    public int Distance
    {
        get => m_Distance;
        set
        {
            m_Distance = value;
            txtDistance.text = value.ToString() + "米";
        }
    }

    public float Times
    {
        get => m_Time;
        set
        {
            if (value < 0)
            {
                //游戏结束
                value = 0;
                SendEvent(Consts.E_EndGame);
            }
            else if (value > startTime)
            {
                value = startTime;
            }

            m_Time = value;

            txtTimer.text = value.ToString("f2") + "s";
            sliTimer.value = value / startTime;
        }
    }

    public int Goal { get => m_Goal; set => m_Goal = value; }

    #endregion

    #region 方法

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    //点击暂停按钮
    public void OnPauseClick()
    {
        PauseArgs e = new PauseArgs
        {
            coin = Coin, 
            score = Coin * 10 + Distance + Goal * 30,
            distance = Distance
        };
        SendEvent(Consts.E_PauseGame, e);
    }

    //点击技能
    public void OnMagnetClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = ItemKind.MagnetItem
        };
        SendEvent(Consts.E_HitItem, e);
    }

    public void OnMultiplyClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = ItemKind.MultiplyItem
        };
        SendEvent(Consts.E_HitItem, e);
    }

    public void OnInvincibleClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = ItemKind.InvincibleItem
        };
        SendEvent(Consts.E_HitItem, e);
    }

    //更新技能按钮可用状态
    public void UpdateUI()
    {
        ShowOrHide(gm.Invincible, btnInvincible);
        ShowOrHide(gm.Multiply, btnMultiply);
        ShowOrHide(gm.Magnet, btnMagnet);
    }

    void ShowOrHide(int i, Button btn)
    {
        if(i > 0)
        {
            btn.interactable = true;
            btn.transform.Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            btn.interactable = false;
            btn.transform.Find("Mask").gameObject.SetActive(true);
        }
    }

    //UI更新

    string GetTime(float time)
    {
        return ((int)time + 1).ToString();
    }

    //双倍金币计时图标
    public void HitMultiply()
    {
        if(MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }

    IEnumerator MultiplyCoroutine()
    {
        float timer = gm.SkillTime;
        txtGizmoMultiply.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if(gm.IsPlay && !gm.IsPause)
            {
                txtGizmoMultiply.text = GetTime(timer);
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        txtGizmoMultiply.transform.parent.gameObject.SetActive(false);
    }

    //吸铁石计时图标
    public void HitMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        float timer = gm.SkillTime;
        txtGizmoMagnet.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                txtGizmoMagnet.text = GetTime(timer);
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        txtGizmoMagnet.transform.parent.gameObject.SetActive(false);
    }

    //无敌计时图标
    public void HitInvincible()
    {
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCoroutine();
        StartCoroutine(InvincibleCor);
    }

    IEnumerator InvincibleCoroutine()
    {
        float timer = gm.SkillTime;
        txtGizmoInvincible.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                txtGizmoInvincible.text = GetTime(timer);
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        txtGizmoInvincible.transform.parent.gameObject.SetActive(false);
    }

    //射门按钮
    void ShowGoalClick()
    {
        //1.显示slider
        //2.button可以按下
        StartCoroutine(GoalCountDownCoroutine());
    }

    IEnumerator GoalCountDownCoroutine()
    {
        btnGoal.interactable = true;
        sliGoal.value = 1;
        while(sliGoal.value > 0)
        {
            if(gm.IsPlay && !gm.IsPause)
            {
                sliGoal.value -= 1.5f * Time.deltaTime;
            }
            yield return 0;
        }
        btnGoal.interactable = false;
        sliGoal.value = 0;
    }

    //按下射门按钮
    public void OnGoalBtnClick()
    {
        SendEvent(Consts.E_ClickGoalBtn);
        sliGoal.value = 0;
        btnGoal.interactable = false;
    }

    #endregion

    #region Unity回调

    private void Awake()
    {
        Times = startTime;
        gm = GetModel<GameModel>();

        UpdateUI();
    }

    private void Update()
    {
        if (!gm.IsPause && gm.IsPlay)
        {
            Times -= Time.deltaTime;
        }
    }

    #endregion

    #region 事件回调

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(Consts.E_UpdateDistance);
        AttentionList.Add(Consts.E_UpdateCoin);
        AttentionList.Add(Consts.E_AddTime);
        AttentionList.Add(Consts.E_HitGoalTrigger);
        AttentionList.Add(Consts.E_ShootGoal);
    }

    public override void HandleEvent(string name, object data)
    {
        switch (name)
        {
            case Consts.E_UpdateDistance:
                DistanceArgs e1 = data as DistanceArgs;
                Distance = e1.distance;
                break;
            case Consts.E_UpdateCoin:
                CoinArgs e2 = data as CoinArgs;
                Coin += e2.coin;
                break;
            case Consts.E_AddTime:
                Times += addedTime;
                break;
            case Consts.E_HitGoalTrigger:
                ShowGoalClick();
                break;
            case Consts.E_ShootGoal:
                Goal += 1;
                break;
        }
    }

    #endregion

    #region 帮助方法
    #endregion

}
