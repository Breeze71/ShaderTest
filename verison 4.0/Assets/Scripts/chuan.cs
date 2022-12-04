using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chuan : MonoBehaviour
{
    // Start is called before the first frame update
    int currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;               //取得作用中的场景
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(currentScene+1);
        }

    }
 


    
}
