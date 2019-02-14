using System;
using System.Collections.Generic;

public abstract class Model
{
    //名字标志
    public abstract string Name { get; }

    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}