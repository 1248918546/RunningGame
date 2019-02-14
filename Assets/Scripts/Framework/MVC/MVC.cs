using System;
using System.Collections.Generic;

public static class MVC
{
    //资源
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();    //名字对应Model
    public static Dictionary<string, View> Views = new Dictionary<string, View>();       //名字对应View
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();  //事件名对应Controller类型

    //注册资源
    //注册view
    public static void RegisterView(View view)
    {
        //防止重复注册view
        if(Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }

        view.RegisterAttentionEvent();
        Views[view.Name] = view;
    }
    //注册model
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }
    //注册controller
    public static void RegisterController(string eventName, Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //获取model
    public static T GetModel<T>() where T : Model
    {
        foreach(var m in Models.Values)
        {
            if(m is T)
            {
                return (T)m;
            }
        }
        return null;
    }

    //获取view
    public static T GetView<T>() where T : View
    {
        foreach (var v in Views.Values)
        {
            if (v is T)
            {
                return (T)v;
            }
        }
        return null;
    }

    //发送事件
    public static void SendEvent(string eventName, object data = null)
    {
        //controller响应事件
        if(CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            //生成控制器
            Controller c = Activator.CreateInstance(t) as Controller;
            //控制器执行方法
            c.Execute(data);
        }

        //view处理
        foreach (var v in Views.Values)
        {
            if(v.AttentionList.Contains(eventName))
            {
                //执行事件
                v.HandleEvent(eventName, data);
            }
        }
    }
}
