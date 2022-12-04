using UnityEngine;
using Interfaces;
// 怎麼飛行
public class BossBulletControllerRed : MonoBehaviour
{
    // 速度 ， 存在時間 ， 角度 ，隨時間偏移 
    public float speed;
    public float existTime = 8f;
    public float radian;
    public float movement;
    [SerializeField] private GameObject HitCheckObject;

    // delegate
    public delegate void Recycle(BossBulletControllerRed bullet);
    public Recycle recycle;

    private void Update()
    {
        firstState();

        existTime -= Time.deltaTime;
        
        if(existTime <= 0 )
        {
            // Destroy
            // 調用 delegate 的 objectPool 的recycle
            recycle(this); // 即　ｂｕｌｌｅｔ
        }
    }

    private void firstState()
    {
        movement = speed * Time.deltaTime;
        this.transform.position += new Vector3(movement * Mathf.Sin(radian) ,
                                    movement * Mathf.Cos(radian) , 0);
    }


}
