using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 偷懶變數
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer tr;
    Animator anim;
    
    // 各項數值
    [SerializeField] float speed = 5f;

    [Header("Jumping")]
    public float jumpVelocity = 5f;
    // 更改重力達到長按 jump 跳更高
    public float fallMutiplier = 2.5f;
    public float lowJumpMutiplier = 2f;

    // 設定一個標籤，判定碰到即著陸
    [SerializeField] private LayerMask collisionMask;
    // 新增判定點 groundCheck 位置
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.5f;

    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.5f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;

    void Start(){           
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update(){

        // 令 input. 為接收到鍵盤輸入
        var inputX = Input.GetAxisRaw("Horizontal");
        var dashInput = Input.GetButtonDown("Dash");

        //// 水平速度改為 input方向 * speed
        rb.velocity = new Vector2(inputX * speed , rb.velocity.y);

        ////Jump
        if(Input.GetButtonDown("Jump") && IsGrounded() ){
            // 新增垂直速度
            rb.velocity = Vector2.up * jumpVelocity;
            
        }
        // BetterJump
        // 加速墜落感
        // 若沒長按跳，則向上重力較小 = 加速向下
        if(rb.velocity.y < 0){

            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMutiplier - 1) * Time.deltaTime;
        
        }
        // 若沒長按跳，則向上重力較小 = 加速向下
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){

            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMutiplier - 1) * Time.deltaTime;
        }

        // 如果在移動   
        if(inputX != 0){

            //面向移動方向
            transform.localScale = new Vector3(Mathf.Sign(inputX),1,1);
            anim.SetBool("Run",true);

        }
        else{
            anim.SetBool("Run",false);
        }

        ////dash

        if(dashInput && _canDash){
            // 避免連瞬
            _isDashing = true;
            _canDash = false;

            // 軌跡
            tr.emitting = true;
            
            // 方向為 wasd 輸入                 // 更斜
            _dashingDir = new Vector2(inputX , Input.GetAxisRaw("Vertical"));
        
            // 若沒輸入，默認當前方向
            if(_dashingDir == Vector2.zero){

                _dashingDir = new Vector2(transform.localScale.x,0);
            }

            // 在一定時間內慢慢停止 Dash 
            StartCoroutine(StopDashing());
        }

        

        //// 進行一個位的移
        if(_isDashing){
            
            rb.velocity = _dashingDir.normalized * _dashingVelocity;
            // 不 return 會無限瞬移
            return;
        }

        // 落地可再次 dash
        if(IsGrounded()){

            _canDash = true;

        }


        ////播放跳跃动作
        if (rb.velocity.y > 0){

            anim.SetBool("jump", true);
            anim.SetBool("down", false);

        }
        //播放下落动作
        else if (rb.velocity.y <-5){

            anim.SetBool("jump", false);
            anim.SetBool("down", true);
        }
        else{

            anim.SetBool("down",false );
        }


    }

    // 判定落地
    private bool IsGrounded(){

        return Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,collisionMask);

    }

    void OnDrawGizmosSelected(){

        if(groundCheck == null){
            return;
        }
        Gizmos.DrawWireSphere(groundCheck.position , groundCheckRadius);
    }

    // 協程，讓動作不會在一禎中跑完
    private IEnumerator StopDashing(){
        anim.SetBool("down",false );
        // dash 時避免重力影響
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Dashing...
        yield return new WaitForSeconds(_dashingTime);

        // 停止軌跡
        tr.emitting = false;
        _isDashing = false;
        
        rb.gravityScale = 0;
        
        // Dash 持續時間完，恢復原本重力
        rb.gravityScale = originalGravity;
    }
}

