using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMove : MonoBehaviour
{
    public Transform _player;
    void Update()
    {
        transform.position = _player.transform.position;
    }
}
