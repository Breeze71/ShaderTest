using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 50f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private SpriteRenderer rd;

    Animator anim;

    void Start(){           
        anim = GetComponent<Animator>();
    }

    private void Update(){
        // 避免行走跳躍時，dash
        if(isDashing){
            return;
        }

    }



    private void FixedUpdate() {
        
        if(isDashing){
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
            anim.SetBool("Dash",true);
        }
        
    }

    private IEnumerator Dash(){
        // 正在進行 dash
        canDash = false;
        isDashing = true;
        // dash 時避免重力影響
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        
        // 左右轉頭時瞬不同方向 true 向左
        if(rd.flipX){
            rb.velocity = new Vector2(-transform.localScale.x * dashingPower , 0f);
        }
        else{
            rb.velocity = new Vector2(transform.localScale.x * dashingPower , 0f);
        }

        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        
    }

}
