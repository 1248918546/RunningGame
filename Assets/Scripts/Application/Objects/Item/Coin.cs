using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    protected Transform effectParent;
    public float moveSpeed = 20;

    private void Awake()
    {
        effectParent = GameObject.Find("EffectParent").transform;
    }

    public override void HitPlayer(Vector3 pos)
    {
        //1.特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effectParent);
        go.transform.position = pos;

        //2.声音
        Game.Instance.sound.PlayEffect("Se_UI_JinBi");

        //3.回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.player)
        {
            HitPlayer(other.transform.position);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
        //吸铁石
        else if(other.tag == Tag.magnetCollider)
        {
            //金币飞向player
            StartCoroutine(HitMagnet(other.transform));
            /*transform.position = Vector3.MoveTowards(transform.position, other.transform.position, moveSpeed * Time.deltaTime);
            HitPlayer(other.transform.position);
            other.transform.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);*/
        }
    }

    IEnumerator HitMagnet(Transform pos)
    {
        bool isLoop = true;
        while(isLoop)
        {
            transform.position = Vector3.Lerp(transform.position, pos.position, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, pos.position) < 0.1f)
            {
                isLoop = false;
                HitPlayer(pos.position);
                pos.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }
            yield return 0;
        }
    }
}
