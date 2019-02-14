using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : EditorWindow
{
    [MenuItem("Tools/Click Me!")]

    static void patternSystem()
    {
        GameObject spawnManager = GameObject.Find("PatternManager");
        if(spawnManager != null)
        {
            //获取PatternManager脚本
            var patternManager = spawnManager.GetComponent<PatternManager>();
            if(Selection.gameObjects.Length == 1)
            {
                //找Item这个物体
                var item = Selection.gameObjects[0].transform.Find("Item");
                if(item != null)
                {
                    Pattern pattern = new Pattern();
                    foreach(var child in item)
                    {
                        Transform childTrans = child as Transform;
                        if(childTrans != null)
                        {
                            //找到它的prefab的名字，以防止取到xxx(1)这种情况
                            var prefab = PrefabUtility.GetPrefabParent(childTrans.gameObject);
                            if(prefab != null)
                            {
                                PatternItem patternItem = new PatternItem
                                {
                                    prefabName = prefab.name,
                                    prefabPos = childTrans.localPosition
                                };
                                pattern.PatternItems.Add(patternItem);
                            }
                        }
                    }
                    patternManager.Patterns.Add(pattern);
                }
            }

        }
    }
}
