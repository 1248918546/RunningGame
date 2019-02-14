using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubPool
{
    List<GameObject> m_objects = new List<GameObject>();
    GameObject m_prefab;

    public string Name
    {
        get
        {
            return m_prefab.name;
        }
    }

    //父物体的位置
    Transform m_parent;

    public SubPool(Transform parent, GameObject go)
    {
        m_prefab = go;
        m_parent = parent;
    }

    //取出物体
    public GameObject Spawn()
    {
        GameObject go = null;
        foreach (var obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    //回收物体
    public void UnSpawn(GameObject go)
    {
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    public void UnSpawnAll()
    {
        foreach (var obj in m_objects)
        {
            if (obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }

    //判断物体是否在List中
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}