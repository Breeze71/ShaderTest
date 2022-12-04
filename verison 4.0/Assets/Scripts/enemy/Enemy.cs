using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // [SerializeField] private SpriteRenderer rd;
    // public Animator anim;
    public int _maxHealth = 200;
    public int _health;

    void Start()
    {
        _health = _maxHealth;
    }

    ////
    public void TakeDamage(int _damage)
    {
        _health -= _damage;
        // Play hurt animation
        // anim.SetTrigger("Hurt");
        if(_health <= 0)
        {
            Die();
        }
    }

    ////
    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
        // Die animation
        // anim.SetBool("isDead" , true);
        // Disable enemy
        // GetComponent<Collider2D>().enabled = false;
        // this.enabled = false;
    }


    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Debug.Log("撞到 1 ");
            Bar bar = GetComponent<Bar>();
            bar.Change(-20);
        }
    }

    */

}
