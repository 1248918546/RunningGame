using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetEffect : Effect
{

    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(-0.40f, 0, -3.23f);
        transform.localScale = new Vector3(1.66f, 1.66f, 1.66f);
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
