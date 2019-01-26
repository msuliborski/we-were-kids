using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHere : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToSpawn;
    public float rate = 5;
    private float counting;
    
    void Start() {
        counting = 2;
    }

    // Update is called once per frame
    void Update() {
        
        counting -= Time.deltaTime;
        if (counting < 0) counting = 0f;     
        
        if (Math.Abs(counting)*1f < 0.01) {
            Instantiate(objectToSpawn, transform.position, Quaternion.Euler(0, 0, 0), transform);
            counting = rate;
        }

        if (transform.childCount > 1) {
            Destroy(transform.GetChild(1).gameObject);
        }
    }
}
