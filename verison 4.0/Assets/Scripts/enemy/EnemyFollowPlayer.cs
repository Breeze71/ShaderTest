using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    //enemy移动速度
    public float lineOfSite;
    //表示攻击范围
    private Transform player;
    //玩家作为变量输入


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //获得player的transform  标签为Player
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position , transform.position);
        //该变量记录player和enemy间的距离 参数（player位置 ，enemy位置）
        if (distanceFromPlayer < lineOfSite)        //两者距离小于一定值
        {
        transform.position = Vector2.MoveTowards(this.transform.position , player.position , speed * Time.deltaTime);
        //使enemy朝向玩家移动 参数（enemy的位置 ，玩家位置 ，移动速度）              
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfSite);
    }
}
