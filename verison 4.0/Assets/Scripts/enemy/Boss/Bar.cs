using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Bar : MonoBehaviour
{
    [field:SerializeField]
    public int MaxValue {get ; private set;}

    [field:SerializeField]
    public int Value { get ; private set;}

    [SerializeField] 
    private RectTransform _topBar;

    [SerializeField] 
    private RectTransform _ButtonBar;

    [SerializeField] 
    private float _animSpeed = 10f;

    private float _fullWidth;
    private float TargetWidth => Value * _fullWidth / MaxValue;

    private Coroutine _adjustBarCoroutine;

    /* Anim */
    public Animator anim;
    
    /* 狂暴攻擊點增加 */
    [SerializeField] private GameObject rageFirePoint;
    int currentScene;

    private void Start()
    {
        _fullWidth = _topBar.rect.width;

        anim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private IEnumerator AdjustBar(int damage)
    {
        // 判定被回血跟扣血時，底條和紅條順序
        var suddenChange = damage >= 0 ? _ButtonBar : _topBar;
        var slowChange = damage >= 0 ? _topBar : _ButtonBar;
        
        // anim 運用 IEnumerator 特性和 anyState 達成一閃而現又肉眼可見的效果
        anim.SetBool("IsHit" , true);

        // 調用 Extension
        suddenChange.SetWidth(TargetWidth);
        
        // 絕對值
        while(Mathf.Abs(suddenChange.rect.width - slowChange.rect.width) > 1f)
        {
            slowChange.SetWidth(
                Mathf.Lerp(slowChange.rect.width , TargetWidth , Time.deltaTime * _animSpeed));
        
            yield return null;
        }
        slowChange.SetWidth(TargetWidth);
        // anim
        anim.SetBool("IsHit" , false);
    }


    public void Change(int damage)
    {
        Value = Mathf.Clamp(Value + damage , 0 , MaxValue);
        if(_adjustBarCoroutine != null)
        {
            StopCoroutine(_adjustBarCoroutine);
        }
        _adjustBarCoroutine = StartCoroutine(AdjustBar(damage));
    }


   public void Update()
    {
        if(Value <= 0 )
        {
            // Destroy(gameObject);
            anim.SetBool("IsDead" , true);
            // stop atk
            Invoke("bossDead" , 1.5f);
        }
        else if(Value <= 250)
        {
            anim.SetBool("IsAnger" , true);
            // 第二階段
            rageFirePoint.SetActive(true);
        }
    }

    // 刪除 boss
    private void bossDead()
    {
        SceneManager.LoadScene(currentScene+1);
    }

    // 玩家攻擊
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Change(-50);
            // wrong anim
        }
    }
}
