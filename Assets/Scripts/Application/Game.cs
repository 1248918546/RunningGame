using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]

//游戏入口
public class Game : MonoSingleton<Game>
{
    //全局访问
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public Sound sound;
    [HideInInspector]
    public StaticData staticData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;

        //初始化 注册StartUpController
        RegisterController(Consts.E_StartUp, typeof(StartUpController));

        //游戏启动
        SendEvent(Consts.E_StartUp);

        //跳转场景
        LoadLevel(3);
    }

    //场景跳转
    public void LoadLevel(int level)
    {
        //发送退出当前场景事件
        ScenesArgs e = new ScenesArgs
        {
            //获取当前场景索引
            sceneIndex = SceneManager.GetActiveScene().buildIndex
        };
        SendEvent(Consts.E_ExitScene, e);

        //加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    //进入新场景
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("进入新场景: " + level);
        //发送进入当前场景事件
        ScenesArgs e = new ScenesArgs
        {
            sceneIndex = level
        };
        SendEvent(Consts.E_EnterScene, e);
    }

    //发送事件
    void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    //注册controller
    void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}
