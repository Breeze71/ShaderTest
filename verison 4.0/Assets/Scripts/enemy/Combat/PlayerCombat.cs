using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour{

    public Animator anim;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayer;
    // 只作用在 Enemies layer 上
    public int AttackDamage = 40;
    
    void Update(){
        
        if(Input.GetButtonDown("Fire1")){

            Attack();
        }

    }

    ////
    void Attack(){
        
        // Play an attack animation
        anim.SetTrigger("AttackOne");
        
        // Detect Enemy // Physics.OverlapSphere
        // 以 AttackPoint 為圓心做圓，只作用在 Enemies layer 上
        Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position , AttackRange , EnemyLayer);

        // Damage them
        foreach(Collider2D Enemies in _hitEnemies){

            //Debug.Log("We hit" + Enemies.name);

            Enemies.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
    }

    //// 畫出攻擊範圍
    void OnDrawGizmosSelected(){

        if(AttackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position , AttackRange);
    }
}
