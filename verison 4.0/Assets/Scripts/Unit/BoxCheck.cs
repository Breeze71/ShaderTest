using Interfaces;
using UnityEngine;

// [SerializeField] private GameObject groundCheckObject;

// 打包 Check 用 private ICheck _groundCheck 即可聲明 
// 記得 _groundCheck = groundCheckObject.GetComponent<ICheck>();

// 或直接 return groundCheckObject.GetComponent<ICheck>();

public class BoxCheck : MonoBehaviour , ICheck
{
    // 設定一個標籤，判定碰到即著陸
    [SerializeField] private LayerMask CheckMask;
    [SerializeField] private float width = 0.1f;
    [SerializeField] private float height = 0.1f;

    /* 建築抽象類 */ 
    public bool Check()
    {                                                                               // angles
        return Physics2D.OverlapBox(transform.position, new Vector2(width , height) , 0 ,CheckMask);
    }

    private void OnDrawGizmos()
    {
        if(transform == null)
        {
            return;
        }
        
        Gizmos.DrawWireCube(transform.position , new Vector3(width , height , 1));
    }
}
