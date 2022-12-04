using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
  public float bounce = 20f;

    public Animator anim;
    void Start()
    {
        
        anim = GetComponent<Animator>();
       

        // gravityStore = rb.gravityScale; // É¶¥Ê≥ı º°°£«
    }
    private void OnCollisionEnter2D(Collision2D collision) 
   {
        if(collision.gameObject.CompareTag( "Player"))
        {
            SoundManager.instance.JumpAudio();
            anim.SetBool("jumppad", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }


   }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("jumppad", false);
        }


    }


}

