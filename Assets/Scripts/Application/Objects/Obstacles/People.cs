using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Obstacles
{
    private bool isHit = false;
    public float speed = 10;
    public bool isFly = false;
    GameModel gm;

    Animation anim;

    protected override void Awake()
    {
        base.Awake();
        gm = MVC.GetModel<GameModel>();
        anim = GetComponentInChildren<Animation>();
    }

    public override void HitPlayer(Vector3 pos)
    {
        //1.特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        isHit = false;
        isFly = true;
        anim.Play("fly");
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        anim.Play("run");
    }

    public override void OnUnSpawn()
    {
        anim.transform.localPosition = Vector3.zero;
        isHit = false;
        isFly = false;
        base.OnUnSpawn();
    }

    //people开始移动
    public void HitTrigger()
    {
        isHit = true;
    }

    private void Update()
    {
        if(gm.IsPlay && !gm.IsPause && isHit)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if(isFly && !gm.IsPause && isHit)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, speed * Time.deltaTime);
        }
    }
}
