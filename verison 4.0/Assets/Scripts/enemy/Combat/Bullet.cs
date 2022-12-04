using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float speed = 20f;
    public GameObject impactEffect;

    [Header("Layer")]
    public LayerMask canHitLayer;

    [Header("HitCheck")]
    public Transform hitCheck;
    public float hitCheckRadius = 0.5f;
    void Start()
    {
        // The red axis of the transform in world space.
        _rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        _destroy();
    }

    // 碰到物體後消失並產生動畫
    public void _destroy()
    {
        // if hit obstacle , destroy
        if(IsHit())
        {
            Destroy(gameObject);
            // 子彈碰撞動畫
            Instantiate(impactEffect , transform.position , transform.rotation);
        }
    }


    // 判定碰到後子彈消失
    private bool IsHit()
    {
        return Physics2D.OverlapCircle(hitCheck.position,hitCheckRadius,canHitLayer);
    }

    void OnDrawGizmosSelected(){

        if(hitCheck == null){
            return;
        }
        Gizmos.DrawWireSphere(hitCheck.position , hitCheckRadius);
    }

    // collision.gameObject.SetActive(false)

}
