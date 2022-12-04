using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        // 動畫產生 0.5 秒後銷毀
        Destroy(this.gameObject , 0.3f);
    }


}
