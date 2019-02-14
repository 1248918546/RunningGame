using System;
using System.Collections.Generic;

public abstract class Controller
{
    //执行事件
    public abstract void Execute(object data);

    //获取model
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>();
    }

    //获取view
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>();
    }

    //注册model
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    //注册view
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }

    //注册controller
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}