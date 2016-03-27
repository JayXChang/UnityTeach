using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 敌人类
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 敌人发生碰撞的事件
    /// </summary>
    public event EventHandler<InfoEventArgs<Collision>> OnCollision;

    //Unity生命周期函数，在发生碰撞的时候执行
    void OnCollisionEnter(Collision collision)
    {
        if (OnCollision != null)//当有对象监听这个事件，将这个事件广播出去，监听这个事件的地方会被自动执行
            OnCollision(this, new InfoEventArgs<Collision>(collision));//广播碰撞的事件
    }
}
