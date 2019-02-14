using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(0, 0, -0.33f);
        transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
