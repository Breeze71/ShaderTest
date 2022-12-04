using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydie : MonoBehaviour
{
    public Animator anim;
    void Start()
    {

        anim = GetComponent<Animator>();


        // gravityStore = rb.gravityScale; // É¶¥Ê≥ı º°°£«
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
         
            anim.SetBool("enemydie", true);
            Destroy(this.gameObject, 0.8f);
        }

    }
}
