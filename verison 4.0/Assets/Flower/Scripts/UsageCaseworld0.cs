using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.Flowser;

public class UsageCaseworld0 : MonoBehaviour
{
    [SerializeField]
    ESMessageSystem msgSys;
    
    private int progress = 0;
    private bool isGameEnd = false;
    private bool isLocked = false;
    void Start()
    {
        // Define your customized keyword functions.
        msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
    }
    private void CustomizedFunction()
    {
        Debug.Log("Hi! This is called by CustomizedFunction!");
    }

    void Update()
    {
        // ----- Integration DEMO -----
        // Your own logic control.
        if(msgSys.isCompleted && !isGameEnd && !isLocked){
            switch(progress){
                case 0:
                    msgSys.ReadTextFromResource("world0");
                    break;
              
                case 1:
                    isGameEnd=true;
                    break;
            }
            progress ++;
        }

        if (!isGameEnd && Input.GetKeyDown(KeyCode.E))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }
    }
}
