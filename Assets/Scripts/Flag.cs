using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (transform.position.y < 5)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
    }
}