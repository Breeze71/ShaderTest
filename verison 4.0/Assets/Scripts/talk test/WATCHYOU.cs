using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATCHYOU : MonoBehaviour
{
    public GameObject PLAYER;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = PLAYER.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);
    }
}
