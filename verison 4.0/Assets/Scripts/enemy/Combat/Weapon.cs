using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator anim;

    public float time = 0;
    [Header("Point")]
    public Transform firePoint;
    

    [Header("Prefabs")]
    public GameObject bulletPrefabs;
    void Start()
    {
       
        anim = GetComponent<Animator>();
      
    }


    void Update()
    {
        Timer(); // 計時
        if(Input.GetButtonDown("Fire1") && time > 0.25f)
        {
            SoundManager.instance.RunAudio();
            anim.SetBool("shoot", true);
            Shoot();
            time = 0;
        }
        else
        {
            anim.SetBool("shoot", false);
        }

    }

    private void Shoot()
    {   
        // 生成物件
        Instantiate(bulletPrefabs , firePoint.position, firePoint.rotation);
        // firerate
    }


    /* 冷卻時間 */
    private void Timer()
    {
        time += Time.deltaTime;
    }
}
