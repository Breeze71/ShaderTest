using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Interfaces;

public class Movement : MonoBehaviour
{
    // 偷懶變數
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer tr;
    public Animator anim;
    
    // 各項數值
    [SerializeField] float speed = 5f;
    private bool g_facingRight = true;
    private int currentScene;// 當前地圖編號
    [SerializeField]private float timer;


    [Header("Jumping")]
    public float jumpVelocity = 5f;
    // 更改重力達到長按 jump 跳更高
    public float fallMutiplier = 2.5f;
    public float lowJumpMutiplier = 2f;
    
    [Header("Check")]

    // 新增 Check 
    [SerializeField] private GameObject groundCheckObject;
    [SerializeField] private GameObject grabCheckObject;
    [SerializeField] private GameObject TrapCheckObject;
    [SerializeField] private GameObject TrapCheckObject1;

    [Header("Grabbing")]
    private bool isGrabbing;
    public float wallJumpTime = 1f;
    private float wallJumpCounter;
    // private float gravityStore;

    [Header("Dashing")]
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.5f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;

    [Header("Shield")]
    [SerializeField] private GameObject shield;
    private bool isdead = false;
    private bool shielded = false;

    void Start()
    {           
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
         currentScene = SceneManager.GetActiveScene().buildIndex;   




        // gravityStore = rb.gravityScale; // 儲存初始　Ｇ
    }

    void Update()
    {   
        // 令 input 為接收到鍵盤輸入
        var inputX = Input.GetAxisRaw("Horizontal");
        var dashInput = Input.GetButtonDown("Dash");

        /* wallGrabbing 時，不能輸入別的操作　*/
        if(wallJumpCounter <= 0)
        {
            //// 水平速度改為 input方向 * speed
            rb.velocity = new Vector2(inputX * speed , rb.velocity.y);
            
            Jump();
            
            // 判斷方向
            if(inputX > 0 && !g_facingRight)
            {
                Flip();
            }
            else if(inputX < 0 && g_facingRight)
            {
                Flip();
            }

            /* dash */
            if(dashInput && _canDash){
                // 避免連瞬
                _isDashing = true;
                _canDash = false;

                // 軌跡
                tr.emitting = true;
                
                // 方向為 wasd 輸入                 // 更斜
                _dashingDir = new Vector2(inputX ,0);
            
                // 若沒輸入，默認當前方向
                if(_dashingDir == Vector2.zero&&g_facingRight)
                {

                    _dashingDir = new Vector2(transform.localScale.x,0);
                }
                else if (_dashingDir == Vector2.zero && !g_facingRight)
                {

                    _dashingDir = new Vector2(-transform.localScale.x, 0);
                }

                // 在一定時間內慢慢停止 Dash 
                StartCoroutine(StopDashing());
            }
               
            //// 進行一個位的移
            if(_isDashing){
                
                rb.velocity = _dashingDir.normalized* _dashingVelocity;
                // 不 return 會無限瞬移
                return;
            }

            // 落地可再次 dash
            if(IsGrounded())
            {
                _canDash = true;
            }
            
            /* WallGrab Jumping */ //可爬牆時不碰地
            isGrabbing = false;

            if(CanGrab() && !IsGrounded())
            {
                    isGrabbing = true;
            }

            /* run anim */
            if (inputX != 0)
            {
                //面向移動方向
                // transform.localScale = new Vector3(Mathf.Sign(inputX),1,1);
              
                anim.SetBool("Run", true);
                anim.SetBool("down", false);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            /* jump anim */

            if (rb.velocity.y > 0)
            {
                anim.SetBool("jump", true);
                anim.SetBool("down", false);
            }
            else if (rb.velocity.y < -2)
            {
                anim.SetBool("jump", false);
                anim.SetBool("down", true);
            }
            else
            {
                anim.SetBool("down", false);
            }          
            if (isGrabbing)
            {
                // rb.gravityScale = 0f;
                // rb.velocity = Vector2.zero; // 避免升天

                if(Input.GetButtonDown("Jump"))
                {
                    wallJumpCounter = wallJumpTime; // 蹬牆，開始計時
                    // rb.velocity = new Vector2( -inputX * speed, jumpVelocity);  // 反向作用力
                    if(g_facingRight)
                    {
                        rb.velocity = new Vector2( -2 * speed,1F * jumpVelocity);
                        Flip();
                    }
                    else if(!g_facingRight)
                    {
                        rb.velocity = new Vector2( 2 * speed, 1F * jumpVelocity);
                        Flip();
                    }

                    // rb.gravityScale = gravityStore; // 跳完恢復重力
                    isGrabbing = false; // 主要為　anim 服務
                }
            }
            /*else
            {
                rb.gravityScale = gravityStore;
            }*/
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;  // 倒數 2 秒恢復操作
        }

        /* shield */
        ShieldActive();
        
        /* Die */
        Die();

        /* wallgrab anim */
        anim.SetBool("grib" , isGrabbing && !IsGrounded());

        /* Die anim */
        // anim.SetBool("", isDie);

       
    // end
    }

    /* 轉身 */
    private void Flip()
    {
        
        g_facingRight = !g_facingRight;

        transform.Rotate(0f , 180f , 0f);
    }

    /* jump */
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded() ){
            // 新增垂直速度
            rb.velocity = Vector2.up * jumpVelocity;
            
        }
        // BetterJump
        // 加速墜落感
        // 若沒長按跳，則向上重力較小 = 加速向下
        if(rb.velocity.y < 0){

            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMutiplier - 1) * Time.deltaTime*0.5f;
        
        }
        // 若沒長按跳，則向上重力較小 = 加速向下
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){

            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMutiplier - 1) * Time.deltaTime*0.5f;
        }
    }

    /* Interfaces Check */
    private bool IsGrounded()
    {
        return groundCheckObject.GetComponent<ICheck>().Check();
    }
    private bool CanGrab()
    {
        return grabCheckObject.GetComponent<ICheck>().Check();
    }
    private bool OnTrap()
    {
        
        return TrapCheckObject.GetComponent<ICheck>().Check();
        
    }
    private bool OnTrap1()
    {
        
        return TrapCheckObject1.GetComponent<ICheck>().Check();

    }

    /* Dash 協程，讓動作不會在一禎中跑完 */
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


    /* Shield */
    private void ShieldActive()
    {
        if(Input.GetButtonDown("Fire0") && !shielded)
        {
            // 激活 shield
            shield.SetActive(true);
            shielded = true;
            // code for turning off shield  // Invoke 能調用函數 , 條件
            Invoke("CloseShield" ,0.5f);
        }
    }
    private void CloseShield()
    {
        shield.SetActive(false);
        shielded = false;
    }

    /* Die */
    private void Die()
    { 
    
        if(OnTrap()||OnTrap1())
        {
            isdead = true;
            
            //timer += Time.deltaTime;
            // 生成死亡動畫
            
            // GetComponent<AudioSource>().Play();s

            //Time.timeScale = 0f;
  
             }
        if (isdead)
        {
            SoundManager.instance.DeadAudio();
            anim.SetBool("die", true);
            timer += Time.deltaTime;
            if (timer >= 0.6f)
            {
                SceneManager.LoadScene(currentScene);
                timer = 0; // 等兩秒後 reload screen
            }
           
        }
    }
    
}

