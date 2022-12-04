using UnityEngine;
using Interfaces;
using UnityEngine.Pool;

// 朝哪射
public class BossWeapon : MonoBehaviour
{
    [SerializeField] private GameObject PlayerCheckObject;
    [SerializeField] private GameObject BossBulletPrefab;
    [SerializeField] private GameObject BossMathBullet;
    [SerializeField] private ObjectPool<BossBulletControllerRed> bulletPool;
    
    [SerializeField] private float speed;
    [SerializeField] private float fireRate = 0;   
    private int t = 0;  // 波次
    private float timer = 0f;  // 波次計時
    
    void Start()
    {    
        // function , 動作 , 回收
        bulletPool = new ObjectPool<BossBulletControllerRed>(
            // landa function
            ()=>{
                // 創建物品 // delegate // 都是指針 :)
                BossBulletControllerRed b = Instantiate(BossBulletPrefab , this.transform.position , Quaternion.identity).GetComponent<BossBulletControllerRed>();
                b.recycle = (bullet)=>{
                    bulletPool.Release(bullet); // 即　ｔｈｉｓ
                };
                return b;
            },
            (bullet)=>{
                // 從 objectPool 拿東西時，執行的動作
                bullet.transform.position = this.transform.position; // 出現位置
                bullet.existTime = 8f;
                bullet.gameObject.SetActive(true);
            },
            (bullet)=>{
                // 回收
                bullet.gameObject.SetActive(false);
            },
            (bullet)=>{
                Destroy(bullet.gameObject);
            }
            , true , 1000 , 10000 // collision check , 容量 , 最大容量
        );
    }

    private void Update() 
    {   
        // 進攻模式
        timer += Time.deltaTime;
        if(timer >= 8)
        {
            t++;
            timer = 0;
        }
        fireRate += Time.deltaTime;

        switch(t)
        {
            case 0: 
                firstDraw();
                break;
            case 1:
                secondDraw();
                break;
            case 2:
                thirdDraw();
                t = 0;  // 重新循環
                break;
 
        }

    }

    private void firstDraw()
    {
                // first state
        if(InRange() && fireRate > 1)
        {
            // d 等分
            int d = 8;
            float d_angle = 360/d;
            float d_radian = 360/d * Mathf.PI / 180; // 轉成 radian (PI)
            
            for(int i = 0 ; i < d ; i++)
            {
                BossBulletControllerRed b = bulletPool.Get();
                b.radian = d_radian * i;    // 每個角度一次
            }
            fireRate = 0;
        }
    }

    private void secondDraw()
    {
        if(InRange() && fireRate > 1)
        {   
            int d = 3;
            float d_radian = 360/d * Mathf.PI / 180;

            for(int i = 0 ; i < d ; i++)
            {
                BossBulletControllerRed b = bulletPool.Get();
                b.radian = d_radian * i;    // 每個角度一次
            }
            fireRate = 0;
        }
    }

    private void thirdDraw()
    {
        if(InRange() && fireRate > 1)
        {   
            int d = 12;
            float d_radian = 360/d * Mathf.PI / 180;

            for(int i = 0 ; i < d ; i++)
            {
                BossBulletControllerRed b = bulletPool.Get();
                b.radian = d_radian * i;    // 每個角度一次
            }
            fireRate = 0;
        }
    }


    private bool InRange()
    {
        return PlayerCheckObject.GetComponent<ICheck>().Check();
    }
}
