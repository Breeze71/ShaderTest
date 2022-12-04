using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletx: MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg;
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        //�ҵ��������}

    }
    private void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    public void Lauch(Vector2 direction, float force)
    {
        rg.AddForce(direction * force  );
    }//��������Ⱥͷ���
}
