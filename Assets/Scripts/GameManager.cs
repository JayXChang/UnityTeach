using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏管理器类
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 生成敌人的间隔（每隔多长时间生成敌人单位为s）
    /// </summary>
    public float reapeat = 1f;
    /// <summary>
    /// 当前场景中最大的敌人的数量
    /// </summary>
    public int maxEnmeyNum = 6;
    /// <summary>
    /// 游戏的时间
    /// </summary>
    public int gameTime = 30;

    [SerializeField] Player player; //玩家控制对象
    [SerializeField] GameObject enemyPrefabs;//敌人的预制体对象
    [SerializeField] UIPanel uiPanel;//UI对象

    List<Enemy> enemyList = new List<Enemy>();//保存敌人的数组

    int time = 0;//开始游戏的现在的时间
    int score = 0;//游戏的得分

    // Unity生命周期函数，当游戏的开始的执行一次
    void Start()
    {
        Time.timeScale = 1;//安照正常的速度进行游戏，Time.timeScale = 0相当于暂停游戏
        uiPanel.IsEndObjActive = false;//设置游戏结束ui不显示
        uiPanel.OnClickBtnReplay = () => SceneManager.LoadScene(0);//C#里面的拉姆达表达式语法，SceneManager.LoadScene(0)表示重新加载场景
        InvokeRepeating("RandomCreatEnemy", 0, reapeat);//InvokeRepeating(methodName, time, repeatRate)表示延迟time执行methodName方法，之后每个repeatRate时间执行一次methodName方法
        InvokeRepeating("TimeCountDown", 1, 1);//同上
    }

    //倒计时，当游戏进行时间等于游戏设置的时候后，会执行游戏结束
    void TimeCountDown()
    {
        time++;//因为上面执行的该方法时候每隔1秒执行一次，所以这里每次都加1
        uiPanel.TextCountDown = gameTime - time;//ui倒计时显示为总时间减去进行游戏的时间
        if (time >= gameTime)//进行游戏的时间>=总时间时候游戏结束
        {
            GameOver();
        }
    }

    //游戏结束方法
    void GameOver()
    {
        CancelInvoke("RandomCreatEnemy");//CancelInvoke（methodName)取消InvokeRepeating方法调用
        CancelInvoke("TimeCountDown");//同上
        uiPanel.IsEndObjActive = true;//显示游戏结束的UI
        Time.timeScale = 0;//Time.timeScale = 0表示暂停游戏，Time.timeScale = 1表示正常的速度播放游戏
    }

    //随机生成敌人（位置随机）
    void RandomCreatEnemy()
    {
        if (enemyList.Count >= maxEnmeyNum) return;//当生成敌人数量大于设置的最大敌人数量的时候停止生产敌人
        float x = Random.Range(-5f, 5f);//在-5f到5f之间随机生成一个float
        float z = Random.Range(-5f, 5f);//同上
        var pos = new Vector3(x, 0, z);//生成一个Vector3对象
        CreatEnemy(pos);//生成敌人
    }

    //生成敌人的方法
    void CreatEnemy(Vector3 pos)
    {
        var tempEnmey = (GameObject)Instantiate(enemyPrefabs, pos, Quaternion.identity);//Instantiate在pos位置克隆enemyPrefabs对象
        var enemyCom = tempEnmey.GetComponent<Enemy>();//获取挂在tempEnmey对象上的Enemy脚本
        enemyCom.OnCollision += OnCollison;//监听敌人的碰撞事件
        enemyList.Add(enemyCom);//将生成的敌人添加到敌人数组中
    }

    //删除敌人
    void DestoryEnemy(Enemy e)
    {
        e.OnCollision -= OnCollison;//取消监听敌人碰撞事件
        enemyList.Remove(e);//将敌人从敌人数组中移除
        Destroy(e.gameObject);//删除敌人
    }

    //角色碰撞的敌人的回调
    void OnCollison(object sender, InfoEventArgs<Collision> e)
    {
        if (e.Info.gameObject == player.gameObject)//当和敌人碰撞的是角色的时候
        {
            Enemy enemy = (Enemy)sender;//sender是该事件的发送者，发送者是和角色发生碰撞的敌人对象，所以这里转换为Enemy类型
            DestoryEnemy(enemy);//删除发生碰撞的敌人对象
            SetScore();//计算分数
        }
    }

    //计算游戏的得分
    void SetScore()
    {
        score += 1;//每次执行的时候游戏的得分+1
        uiPanel.TextScore = score;//设置得分的UI显示
    }
}