using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chuan1 : MonoBehaviour
{
    // Start is called before the first frame update
    int currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;               //ȡ�������еĳ���
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("world 2");
        }

    }
 


    
}
