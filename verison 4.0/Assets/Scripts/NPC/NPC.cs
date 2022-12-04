using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    //存放对话框整个物件
    public  Text dialogueText;
    //存放对话框中的文本物件
    public string[] dialogue;
    private int index;
    
    public GameObject contButton;
    //存放继续按钮
    public float wordSpeed;
    //文字播放速度
    public bool playerIsClose;
    //判断玩家是否靠近

    [Header("text")]
    [SerializeField] private GameObject text;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
                //如果已经打开 满足条件时关闭
            }
            else
            {
                dialoguePanel.SetActive(true);
                //打开对话框
                StartCoroutine(Typing());
                //翻页和文字渐入效果
            }
        }

        if(dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        //文本内容清空
        index = 0;
        //页数置为0
        dialoguePanel.SetActive(false);
        //关闭UI
    }

    IEnumerator Typing()        // 原理不懂 功能是翻页和文字渐入效果
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        contButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index ++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            text.SetActive(true);
           
            playerIsClose = true;
            //判断player靠近NPC或者物体时 bool值为true
        }
    }

     private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            text.SetActive(false);
         
            playerIsClose = false;
            zeroText();
            //远离时改变bool值  清空对话框
        }
    }
}
