using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RemptyTool.Flowser;

public class UsageCaseworld3 : MonoBehaviour
{
    [SerializeField]
    ESMessageSystem2 msgSys;

    private int progress = 0;
    private bool isGameEnd = false;
    private bool isLocked = true;
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

        if (msgSys.isCompleted && !isGameEnd && !isLocked)
        {
            switch (progress)
            {
                case 0:
                    msgSys.ReadTextFromResource("up");
                    break;

                case 1:
                    isGameEnd = true;
                    break;
            }
            progress++;
        }

        if (!isGameEnd && Input.GetKeyDown(KeyCode.E))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLocked = false;
        }
    }
}