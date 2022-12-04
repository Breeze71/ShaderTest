using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletControllerMathf : MonoBehaviour
{
    public float mathSpeed;
    public float mathExistTime = 6f;
    public float mathRadian;
    public float mathMovement;
    [SerializeField] private GameObject HitCheckObject;

    // delegate
    public delegate void Recycle(BossBulletControllerMathf mathBullet);
    public Recycle mRecycle;

    private void Update()
    {
        mathBulletFly();

        mathExistTime -= Time.deltaTime;
        
        if(mathExistTime <= 0 )
        {
            // Destroy
            // 調用 delegate 的 objectPool 的recycle
            mRecycle(this); // 即　ｂｕｌｌｅｔ
        }
    }

    private void mathBulletFly()
    {
        mathMovement = mathSpeed * Time.deltaTime;
        this.transform.position += new Vector3(mathMovement * Mathf.Sin(mathRadian + mathMovement) ,
                                    mathMovement * Mathf.Cos(mathRadian + mathMovement) , 1);
    }
}
