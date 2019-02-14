using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : Item
{
    public override void HitPlayer(Vector3 pos)
    {
        //1.声音
        Game.Instance.sound.PlayEffect("Se_UI_Time");

        //2.回收
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
            other.SendMessage("HitAddTime");
        }
    }
}
