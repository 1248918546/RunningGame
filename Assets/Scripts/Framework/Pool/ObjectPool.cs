using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    /// <summary>
    /// 资源目录
    /// </summary>
    public string ResourceDir = "";

    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    //取物体
    public GameObject Spawn(string name, Transform trans)
    {
        SubPool pool = null;
        if(!m_pools.ContainsKey(name))
        {
            RegisterNew(name, trans);
        }
        pool = m_pools[name];
        return pool.Spawn();
    }

    //回收物体
    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach(var p in m_pools.Values)
        {
            if(p.Contain(go))
            {
                pool = p;
                break;
            }
        }
        pool.UnSpawn(go);
    }

    //回收所有物体
    public void UnSpawnAll()
    {
        foreach(var p in m_pools.Values)
        {
            p.UnSpawnAll();
        }
    }

    //清除所有池子
    public void ClearAll()
    {
        m_pools.Clear();
    }

    //新建一个子池子
    void RegisterNew(string name, Transform trans)
    {
        //资源目录
        string path = ResourceDir + "/" + name;
        //预制体
        GameObject go = Resources.Load<GameObject>(path);
        //新建子池子
        SubPool pool = new SubPool(trans, go);
        m_pools.Add(pool.Name, pool);
    }
}
