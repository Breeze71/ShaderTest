using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    private PlayerDistanceChecker playerDistanceChecker;
    private PlayerController playerController ;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerController>();
        playerDistanceChecker = GetComponent<PlayerDistanceChecker>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
