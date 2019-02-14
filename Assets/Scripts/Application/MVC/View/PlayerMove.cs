using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{
    #region 常量

    const float m_moveSpeed = 13;
    const float gravity = 9.8f;
    const float m_jumpValue = 5;

    const float m_SpeedAddDis = 300;
    const float m_SpeedAddRate = 0.5f;
    const float m_MaxSpeed = 40;

    #endregion

    #region 事件
    #endregion

    #region 字段

    float speed = 20;
    CharacterController m_cc;
    InputDirection m_inputDir = InputDirection.NULL;
    bool activeInput = false;
    Vector3 m_mousePos;

    //左右移动
    int m_nowIndex = 1;
    int m_targetIndex = 1;

    //跳跃
    float m_yDistance;

    //滑行
    bool m_isSlide = false;
    float m_SlideTime;  //计时器

    //速度
    float m_SpeedAddCount;

    //减速
    float m_MaskSpeed;
    float m_AddRate = 10;//减速后增加速度的速率
    bool m_IsHit = false;

    //Item
    //金币
    public int m_DoubleTime = 1;
    int m_SkillTime;
    IEnumerator MultiplyCor;
    //吸铁石
    SphereCollider m_MagnetCollider;

    IEnumerator MagnetCor;
    //无敌口哨
    public bool m_IsInvincible = false;
    IEnumerator InvincibleCor;

    //射门相关
    GameObject m_Ball;
    GameObject m_Trail;
    IEnumerator GoalCor;
    bool m_IsGoal = false;

    //model
    GameModel gm;

    #endregion

    #region 属性

    public override string Name { get { return Consts.V_PlayerMove; } }

    public float Speed { get => speed; set { speed = value; if (speed > m_MaxSpeed) speed = m_MaxSpeed; } }

    #endregion

    #region 方法

    #region 移动

    IEnumerator UpdateAction()
    {

        while (true)
        {
            if(!gm.IsPause && gm.IsPlay)
            {
                //更新UI
                UpdateDistance();

                m_yDistance -= gravity * Time.deltaTime;
                m_cc.Move((transform.forward * Speed + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);
                MoveControl();
                UpdatePosition();
                UpdateSpeed();
            }
            yield return 0;
        }
    }

    //更新UI
    void UpdateDistance()
    {
        DistanceArgs e = new DistanceArgs
        {
            distance = (int)transform.position.z
        };
        SendEvent(Consts.E_UpdateDistance, e);
    }

    //获取输入
    void GetInputDirection()
    {
        //手势识别
        m_inputDir = InputDirection.NULL;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            m_mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && activeInput)
        {
            Vector3 Dir = Input.mousePosition - m_mousePos;
            if (Dir.magnitude > 20)
            {

                if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && Dir.x > 0)
                {
                    m_inputDir = InputDirection.Right;
                }
                else if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && Dir.x < 0)
                {
                    m_inputDir = InputDirection.Left;

                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y > 0)
                {
                    m_inputDir = InputDirection.Up;
                }

                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y < 0)
                {
                    m_inputDir = InputDirection.Down;
                }
                activeInput = false;
            }
        }

        //键盘识别
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.Right;
        }


        //print(m_inputDir);
    }

    //更新位置
    void UpdatePosition()
    {
        GetInputDirection();
        switch (m_inputDir)
        {
            case InputDirection.NULL:
                break;
            case InputDirection.Right:
                if (m_targetIndex < 2)
                {
                    m_targetIndex++;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Left:
                if (m_targetIndex > 0)
                {
                    m_targetIndex--;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Down:
                if (m_isSlide == false)
                {
                    m_isSlide = true;
                    SendMessage("AnimationManager", m_inputDir);
                    m_SlideTime = 0.733f;
                    Game.Instance.sound.PlayEffect("Se_UI_Slide");
                }
                break;
            case InputDirection.Up:
                if (m_cc.isGrounded)
                {
                    Game.Instance.sound.PlayEffect("Se_UI_Jump");
                    m_yDistance = m_jumpValue;
                    SendMessage("AnimationManager", m_inputDir);
                }
                break;
            default:
                break;
        }

    }

    //移动
    void MoveControl()
    {
        m_nowIndex = m_targetIndex;

        switch (m_nowIndex)
        {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-2, transform.position.y, transform.position.z), m_moveSpeed * Time.deltaTime);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), m_moveSpeed * Time.deltaTime);
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(2, transform.position.y, transform.position.z), m_moveSpeed * Time.deltaTime);
                break;
        }

        //对slide时间计时
        if (m_isSlide)
        {
            m_SlideTime -= Time.deltaTime;
            if (m_SlideTime < 0)
            {
                m_isSlide = false;
                m_SlideTime = 0;
            }
        }
    }

    //更新速度
    void UpdateSpeed()
    {
        m_SpeedAddCount += Speed * Time.deltaTime;
        if (m_SpeedAddCount > m_SpeedAddDis)
        {
            m_SpeedAddCount = 0;
            Speed += m_SpeedAddRate;
        }
    }


    #endregion

    //减速
    public void HitObstacles()
    {
        if (m_IsHit)
            return;
        m_IsHit = true;
        m_MaskSpeed = Speed;
        Speed = 0;
        StartCoroutine(DecreaseSpeed());
    }

    IEnumerator DecreaseSpeed()
    {
        while (Speed < m_MaskSpeed)
        {
            Speed += m_AddRate * Time.deltaTime;
            yield return 0;
        }
        m_IsHit = false;
    }

    //吃金币
    public void HitCoin()
    {
        CoinArgs e = new CoinArgs
        {
            coin = m_DoubleTime
        };
        SendEvent(Consts.E_UpdateCoin, e);
    }

    //技能道具
    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 0,
            kind = item
        };
        SendEvent(Consts.E_HitItem, e);
        //switch (item)
        //{
            //case ItemKind.InvincibleItem:
            //    send
            //    break;
            //case ItemKind.MultiplyItem:
            //    break;
            //case ItemKind.MagnetItem:
            //    break;
            //default:
                //break;
        //}
    }

    //双倍金币道具
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
        m_DoubleTime = 2;
        float timer = m_SkillTime;
        while(timer > 0)
        {
            if(gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);  //防止游戏暂停时还在计时
        m_DoubleTime = 1;
    }

    //吸铁石
    public void HitMagnet()
    {
        if(MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }

    IEnumerator MagnetCoroutine()
    {
        m_MagnetCollider.enabled = true;
        float timer = m_SkillTime;
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);
        m_MagnetCollider.enabled = false;
    }

    //加时间
    public void HitAddTime()
    {
        SendEvent(Consts.E_AddTime);
    }

    //无敌状态
    public void HitInvincible()
    {
        if(InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCoroutine();
        StartCoroutine(InvincibleCor);
    }

    IEnumerator InvincibleCoroutine()
    {
        m_IsInvincible = true;
        float timer = m_SkillTime;
        while (timer > 0)
        {
            if (gm.IsPlay && !gm.IsPause)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);
        m_IsInvincible = false;
    }

    //射门相关
    public void OnGoalClick()
    {
        m_Trail.gameObject.SetActive(true);
        m_Ball.gameObject.SetActive(false);
        SendMessage("PlayShootAnimation");
        if(GoalCor != null)
        {
            StopCoroutine(GoalCor);
        }
        GoalCor = MoveBall();
        StartCoroutine(GoalCor);
    }

    IEnumerator MoveBall()
    {
        while(true)
        {
            if(gm.IsPlay && !gm.IsPause)
            {
                m_Trail.transform.Translate(transform.forward * 40 * Time.deltaTime);
            }
            yield return 0;
        }
    }

    //球射入球门
    public void HitBallDoor()
    {
        //1.停止协程
        StopCoroutine(GoalCor);
        
        //2.归位
        m_Trail.transform.localPosition = new Vector3(0, 2.79f, 5.17f);
        m_Trail.transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);
        m_Trail.gameObject.SetActive(false);
        m_Ball.SetActive(true);
        //3.m_IsGoal=true 进球
        m_IsGoal = true;
        //4.特效
        Game.Instance.objectPool.Spawn("FX_GOAL", m_Trail.gameObject.transform.parent);
        //5.声音
        Game.Instance.sound.PlayEffect("Se_UI_Goal");
        //6.发送加分事件给ui,goalcount+1
        SendEvent(Consts.E_ShootGoal);
    }


    #endregion

    #region Unity回调

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tag.smallFence)
        {
            if (m_IsInvincible)
                return;
            other.gameObject.SendMessage("HitPlayer", transform.position);
            HitObstacles();
            Game.Instance.sound.PlayEffect("Se_UI_Hit");
        }
        else if(other.gameObject.tag == Tag.bigFence) 
        {
            if (m_IsInvincible)
                return;
            if (m_isSlide)
                return;
            other.gameObject.SendMessage("HitPlayer", transform.position);
            HitObstacles();
            Game.Instance.sound.PlayEffect("Se_UI_Hit");
        }
        else if (other.gameObject.tag == Tag.block)
        {
            //撞到集装箱
            other.gameObject.SendMessage("HitPlayer", transform.position);
            //发送游戏结束event
            SendEvent(Consts.E_EndGame);
            Game.Instance.sound.PlayEffect("Se_UI_End");
        }
        else if (other.gameObject.tag == Tag.smallBlock)
        {
            //撞到集装箱侧面
            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);
            //发送游戏结束event
            SendEvent(Consts.E_EndGame);
            Game.Instance.sound.PlayEffect("Se_UI_End");
        }
        else if (other.gameObject.tag == Tag.beforeTrigger)
        {
            //汽车触发器
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
        }
        else if(other.gameObject.tag == Tag.beforeGoalTrigger)
        {
            //射门触发器，准备射门
            //发消息给UIBoard
            SendEvent(Consts.E_HitGoalTrigger, SendMessageOptions.RequireReceiver);
            //显示加速特效
            Game.Instance.objectPool.Spawn("FX_JiaSu", m_Trail.transform.parent);
        }
        else if(other.gameObject.tag == Tag.goalKeeper)
        {
            //与守门员碰撞
            HitObstacles();
            //守门员被撞飞
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper", SendMessageOptions.RequireReceiver);
        }
        else if(other.tag == Tag.ballDoor)
        {
            //与球门碰撞
            if (m_IsGoal)
            {
                //进球
                m_IsGoal = false;
                return;
            }
            //1.减速
            HitObstacles();
            //2.球网附在身上
            Game.Instance.objectPool.Spawn("Effect_QiuWang", m_Trail.gameObject.transform.parent);
            //3.球门播放动画
            //4.球门的球网消失
            other.transform.parent.parent.SendMessage("HitBallDoor", m_nowIndex);
        }
    }

    private void Awake()
    {
        m_cc = GetComponent<CharacterController>();
        gm = GetModel<GameModel>();

        m_SkillTime = gm.SkillTime;
        m_MagnetCollider = GetComponentInChildren<SphereCollider>();
        m_MagnetCollider.enabled = false;

        //射门
        m_Ball = transform.Find("Ball").gameObject;
        m_Trail = GameObject.Find("Trail").gameObject;
        m_Trail.gameObject.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(UpdateAction());

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            gm.IsPause = true;
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            gm.IsPause = false;
        }
    }
    #endregion


    #region 事件回调
    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(Consts.E_ClickGoalBtn);
    }

    public override void HandleEvent(string name, object data)
    {
        switch(name)
        {
            case Consts.E_ClickGoalBtn:
                OnGoalClick();
                break;
        }
    }
}
#endregion

#region 帮助方法
#endregion































