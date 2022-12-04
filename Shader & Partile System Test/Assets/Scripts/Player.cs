using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputProvider inputProvider;
    public Rigidbody2D rb;
    public Camera cam;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Laser")]
    public LineRenderer lineRenderer;
    public Transform firePoint;
    private Quaternion rotation;
    public Transform endLaser;

    [Header("VFXEffect")]
    public GameObject startVFX;
    public GameObject endVFX;
    private  List<ParticleSystem> particles = new List<ParticleSystem>();


    /* 對象變為可用時調用 */
    private void OnEnable()
    {
        inputProvider = new InputProvider();

        inputProvider.ShootLaser += Shoot;  // 執行 shoot
        inputProvider.Enable(); // 調用 fuction
    }
    /* 對象銷毀或 disable 時調用 */
    private void OnDisable()
    {
        inputProvider.ShootLaser -= Shoot;  // remove

        inputProvider.Disable();
    }

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        FillLists(); //
        disableLaser(); // 先關掉
    }

    private void Update()
    {
        rb.velocity = inputProvider.Vector2Move() * moveSpeed;
        UpdateLaser();  // 可再優化，觸發時再每偵判定
        RotateToMouse();
    }

    /* Laser Event 觸發後執行 */
    private void Shoot(InputAction.CallbackContext context)
    {
        // buttom down
        if(context.started)
        {
            Debug.Log("Shoot " + context.phase);
            enableLaser();
        }
        // buttom
        if(context.performed)
        {
            Debug.Log("per " + context.phase);
            endVFX.SetActive(true);
        }
        // buttom up
        if(context.canceled)
        {
            disableLaser();
            Debug.Log("Shoot " + context.phase);
        }
    }
    
    /* Laser */
    private void enableLaser()
    {
        lineRenderer.enabled = true;
        
        // n
        for(int i = 0 ; i < particles.Count ; i++)
        {
            particles[i].Play();
        }
    }
    private void disableLaser()
    {
        lineRenderer.enabled = false;

        // n
        for(int i = 0 ; i < particles.Count ; i++)
        {
            particles[i].Stop();
        }
    }
    private void UpdateLaser() 
    {
        // 持續跟隨 MOUSE 不能放 event ， 是 update
        // var mousePos = MousePos.GetMousePosition(); // mouse 位置

        lineRenderer.SetPosition(0 , firePoint.position); // 從 0 到 1
        startVFX.transform.position = firePoint.position;

        //lineRenderer.SetPosition(1 , mousePos);

        var pos = (Vector2)endLaser.transform.position;

        Vector2 direction = pos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position , direction.normalized , direction.magnitude);

        if(hit)
        {
            lineRenderer.SetPosition(1 , hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1 , pos);
        }
        endVFX.transform.position = lineRenderer.GetPosition(1);

        // component : enabled // gameObject : SetActive

    }
    private void RotateToMouse()
    {
        // Vector2 direction = (MousePos.GetMousePosition() - (Vector2)transform.position).normalized;
        // float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg; // Math.Atan2 : y/x
        
        Vector2 direction = inputProvider.LaserDirection();     // Radians to degrees = 360 / 2 * pi
        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
                                                                // convert radian to degress
        rotation.eulerAngles = new Vector3(0 , 0 , angle);
        transform.rotation = rotation;
    }

    /*  n */ 
    private void FillLists()
    {
        for(int i = 0 ; i < startVFX.transform.childCount ; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if(ps != null)
            {
                particles.Add(ps);
            }
        }

        for(int i = 0 ; i < endVFX.transform.childCount ; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if(ps != null)
            {
                particles.Add(ps);
            }
        }
    }

}
