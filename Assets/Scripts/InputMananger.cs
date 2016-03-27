using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 
/// </summary>
class Repeater
{
    const float threshold = 0.5f;//Threshold值决定了从最初按下开始，到开始重复输入操作的等待时间
    const float rate = 0.25f;//rate 值决定了输入操作重复的速率
    float _next;//_next 标记目标时间点，其默认和重置的值都是0
    bool _hold;//表示当前用户有没有操作
    string _axis;

    public enum AxisType
    {
        Horizontal = 1,//unity定义的横向移动
        Vertical = 2////unity定义的纵向移动
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="axisName"></param>
    public Repeater(AxisType type)
    {
        _axis = type.ToString();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="axisName"></param>
    public Repeater(string axisName)
    {
        _axis = axisName;
    }

    /// <summary>
    /// 计算当前移动的距离
    /// </summary>
    /// <returns>返回移动的距离</returns>
    public float Update()
    {
        var retValue = 0f; //函数返回值。其值只会在特殊情况下发生改变
        float value = Input.GetAxis(_axis);//Unity获取按轴向移动的方法返回值为-1到1

        if (value != 0)
        {
            if (Time.time > _next)
            {
                retValue = value;
                _next = Time.deltaTime + (_hold ? rate : threshold);//设置下次发送事件的间隔
                _hold = true;//长按状态
            }
        }
        else
        {
            _hold = false;//只要用户没有输入，就立即将 _hold 值设为false
            _next = 0;//并将下次事件等待时间设为0
        }
        return retValue;
    }
}

/// <summary>
/// 输入管理器
/// </summary>
public class InputMananger : MonoBehaviour
{
    /// <summary>
    /// 移动事件
    /// 用Vector2的x,y来保存x,z上的移动的数值
    /// </summary>
    public static event EventHandler<InfoEventArgs<Vector2>> moveEvent;

    Repeater _hor = new Repeater(Repeater.AxisType.Horizontal);
    Repeater _ver = new Repeater(Repeater.AxisType.Vertical);

    //Unity生命周期函数，每一帧都会被调用
    void Update()
    {
        var x = _hor.Update();//获取横向移动
        var y = _ver.Update();//获取纵向移动
        if (x != 0 || y != 0)
        {
            if (moveEvent != null)
                moveEvent(this, new InfoEventArgs<Vector2>(new Vector2(x, y)));//当横向或纵向移动的数值不是0的时候发送移动事件
        }

    }
}