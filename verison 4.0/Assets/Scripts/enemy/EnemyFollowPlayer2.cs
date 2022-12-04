using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer2 : MonoBehaviour
{
    public float speed;
    //enemy移动速度
    public float lineOfSite;
    //表示攻击范围
    public float shootingRange;
    //射击的进攻范围
    public float fireRate = 1f;
    //
    public float nextFireTime;
    //
    private Transform player;
    //玩家作为变量输入
    public GameObject bullet;
    //子弹物体
    public GameObject bulletParent;
    //子弹射出的位置



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //获得player的transform  标签为Player
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position , transform.position);
        //该变量记录player和enemy间的距离 参数（player位置 ，enemy位置）
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange) //两者距离小于一定值并且大于射击范围
        {
        transform.position = Vector2.MoveTowards(this.transform.position , player.position , speed * Time.deltaTime);
        //使enemy朝向玩家移动 参数（enemy的位置 ，玩家位置 ，移动速度）              
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)                               //两者距离小于等于射击范围
        {
            Instantiate(bullet , bulletParent.transform.position , Quaternion.identity);
            nextFireTime =Time.time + fireRate;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , lineOfSite);
        Gizmos.DrawWireSphere(transform.position , shootingRange);
    }
}
