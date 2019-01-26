using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gr : MonoBehaviour
{
    [SerializeField] private GameObject grenade;
    [SerializeField] private float delay;
    [SerializeField] private GameObject player;
    private bool picked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!picked && (transform.position - player.transform.position).magnitude < 1)
        {
            picked = true;
        }

        if (picked)
        {
            transform.position = player.transform.position;
        }
    }

    IEnumerator boom()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
