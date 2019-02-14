using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReuseableObject : MonoBehaviour, IReuseable
{
    public abstract void OnSpawn();

    public abstract void OnUnSpawn();
}
