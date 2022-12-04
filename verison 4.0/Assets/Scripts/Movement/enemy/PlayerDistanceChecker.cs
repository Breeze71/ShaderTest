using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceChecker : MonoBehaviour
{
    private Transform thisTransform;
    private Transform playerTransform;
    [HideInInspector]
    public bool inRange;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        inRange = false;

    }

    // Update is called once per frame
    private void Update()
    {
        CheckPlayerDistance();

    }
    private void CheckPlayerDistance()
    {
        if (thisTransform.position.x - playerTransform.position.x <= range)
        {
            if (inRange)
            {
                inRange = true;
            }
        }
        else if (thisTransform.position.x - playerTransform.position.x > range)
        {
            if (inRange)
            {
                inRange = false;
            }
        }
    } 
}