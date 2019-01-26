﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnim : MonoBehaviour
{
    [SerializeField] private float rot;
    [SerializeField] private float scl;
    [SerializeField] private float max;
    [SerializeField] private float min;

    private bool grow = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.up * rot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x > max)
            grow = true;
        else if (transform.localScale.x < min)
            grow = false;
        
        
        if (!grow)
        {
            transform.localScale = new Vector3(transform.localScale.x+scl, transform.localScale.y+scl,transform.localScale.z+scl);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x-scl, transform.localScale.y-scl,transform.localScale.z-scl);
        }
    }
}