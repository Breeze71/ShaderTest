using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
