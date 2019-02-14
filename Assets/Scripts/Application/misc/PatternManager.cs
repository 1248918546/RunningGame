using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//所有方案
public class PatternManager : MonoSingleton<PatternManager>
{
    public List<Pattern> Patterns = new List<Pattern>();
}


//一个游戏物体的信息
[Serializable]
public class PatternItem
{
    public string prefabName;
    public Vector3 prefabPos;
}


//一个方案
[Serializable]
public class Pattern
{
    public List<PatternItem> PatternItems = new List<PatternItem>();
}