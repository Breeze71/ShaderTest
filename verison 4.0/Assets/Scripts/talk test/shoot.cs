using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject PLAYER;
    private Transform player;
    //玩家作为变量输入
    float time = 0;
    private Vector2 PlayerVect;
    private Vector2 AttackVect;
    public float lineOfSite;
    //表示攻击范围
    public float shootingRange;
    //射击的进攻范围
    public float N = 100;

    public bool ShootOrNot = false;
    //射击动画
    public Animator anim;

        void Start()
       {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                //获得player的transform  标签为Player
                anim = GetComponent<Animator>();
       }
  
        void Update()
        {
            float distanceFromPlayer = Vector2.Distance(player.position , transform.position);
            //该变量记录player和enemy间的距离 参数（player位置 ，enemy位置）
            PlayerVect.x = PLAYER.transform.position.x;
            PlayerVect.y = PLAYER.transform.position.y;
            AttackVect.x = PlayerVect.x - this.transform.position.x;
            AttackVect.y = PlayerVect.y - this.transform.position.y;//�����ʸ��
                                                                    //  if (playerDistanceChecker.inRange)
             
            time = time + Time.deltaTime;
            if (time >=2&&distanceFromPlayer <= shootingRange )
            { anim.SetBool("ShootOrNot",true);
             time = time + Time.deltaTime;
             if(time>=2.5){
                Launch();
                time = 0;}
            }
            else {
                
                anim.SetBool("ShootOrNot",false);
            }
        }
    
        private void Launch()
        {
            //if ( )          //两者距离小于等于射击范围distanceFromPlayer <= shootingRange 
           // { 
            SoundManager.instance.ShootAudio();
                GameObject bulletx = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bulletx.GetComponent<Bulletx>().Lauch(AttackVect, N);
                 
           // }
            
        }


        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , lineOfSite);
            Gizmos.DrawWireSphere(transform.position , shootingRange);
        }
}


