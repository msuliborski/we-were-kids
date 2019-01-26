using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private GameObject projectile;
    private GameObject inst;
    // Start is called before the first frame update
    void Start()
    {
        inst = GameObject.Find("Inst");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Math.Abs(Input.GetAxis("LeftVertical1")) > 0.5)
            GetComponent<Rigidbody>().velocity = transform.forward*vel;
        else
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        Vector3 r = Vector3.zero;
        r.y = Input.GetAxis("RightHorizontal1");
        transform.Rotate(r * Time.deltaTime * rot, Space.World);

        if (Math.Abs(Input.GetAxis("Fire1")) > 0.5)
        {
            Quaternion parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
        }
        
    }
}
