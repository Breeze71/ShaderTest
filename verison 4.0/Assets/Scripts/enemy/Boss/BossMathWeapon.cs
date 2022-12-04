using UnityEngine;

public class BossMathWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        /* 旋轉彈 
        mathBulletPool = new ObjectPool<BossBulletControllerMathf>(
            // landa function
            ()=>{
                // 創建物品 // delegate
                BossBulletControllerMathf mb = Instantiate(BossMathBullet , this.transform.position , Quaternion.identity).GetComponent<BossBulletControllerMathf>();
                mb.mRecycle = (mathBullet)=>{
                    bulletPool.Release(mathBullet); // 即　ｔｈｉｓ
                };
                return mb;
            },
            (mathBullet)=>{
                // 從 objectPool 拿東西時，執行的動作
                mathBullet.transform.position = this.transform.position; // 出現位置
                mathBullet.mathExistTime = 8f;
                mathBullet.gameObject.SetActive(true);
            },
            (mathBullet)=>{
                // 回收
                mathBullet.gameObject.SetActive(false);
            },
            (mathBullet)=>{
                Destroy(mathBullet.gameObject);
            }
            , true , 1000 , 10000 // collision check , 容量 , 最大容量
        );  
            */  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
