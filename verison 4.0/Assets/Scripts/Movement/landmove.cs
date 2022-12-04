using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landmove : MonoBehaviour
{
    public float zAngle = 90f;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

            

            this.transform.Rotate(0, 0,  zAngle );

        }
       else if (Input.GetKeyDown(KeyCode.K))
        {


            this.transform.Rotate(0, 0, -zAngle );

        }
    }
}
