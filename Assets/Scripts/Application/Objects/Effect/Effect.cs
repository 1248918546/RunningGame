using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ReuseableObject
{

    public float time = 1.0f;

    public override void OnSpawn()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }


    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(time);
        //自动回收
        Game.Instance.objectPool.UnSpawn(gameObject);
    }
}
