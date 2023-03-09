using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputEv : MonoBehaviour
{
    public event Action inputSpace;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputSpace?.Invoke();
        }
    }
}
