using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// C#EventHander委托的参数类
/// 语法为C#的泛型类
/// </summary>
/// <typeparam name="T"></typeparam>
public class InfoEventArgs<T> : EventArgs
{
    public T Info;//具体类型的一个实例

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public InfoEventArgs()
    {
        Info = default(T);//default关键字可以将对象 陪；设置为默认值
    }

    /// <summary>
    /// 自定义构造函数
    /// </summary>
    /// <param name="info"></param>
    public InfoEventArgs(T info)
    {
        this.Info = info;
    }
}
