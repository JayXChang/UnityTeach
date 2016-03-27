using UnityEngine;

/// <summary>
/// 角色类
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 横向移动的速度
    /// </summary>
    public float horSpeed = 0.1f;
    /// <summary>
    /// 纵向移动的速度
    /// </summary>
    public float verSpeed = 0.1f;

    //Unity生命周期函数，在物体被激活的时候执行（Start之前执行）
    void OnEnable()
    {
        InputMananger.moveEvent += OnMove;//监听移动的事件
    }

    //Unity生命周期函数，在物体被取消激活的时候执行（OnDestory之前执行）
    void OnDisable()
    {
        InputMananger.moveEvent -= OnMove; //取消监听移动的事件
    }

    //移动事件的回调
    void OnMove(object sender, InfoEventArgs<Vector2> e)
    {
        var pos = transform.localPosition;//保存角色当前的位置
        var nowPos = new Vector3(pos.x + e.Info.x * horSpeed, pos.y, pos.z + e.Info.y * verSpeed);//保存移动之后的位置
        transform.localPosition = nowPos;//设置角色移动之后的位置
    }
}
