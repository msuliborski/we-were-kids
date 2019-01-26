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
        
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("LeftVertical1");
        dir.z = Input.GetAxis("LeftHorizontal1");
        GetComponent<Rigidbody>().velocity = dir.normalized*vel;
        
//        Vector3 r = Vector3.zero;
//        r.y = Input.GetAxis("RightHorizontal1");
//        transform.Rotate(r * Time.deltaTime * rot, Space.World);
        float rotationX = Input.GetAxis("RightHorizontal1");
        float rotationY = -Input.GetAxis("RightVertical1");

        if (rotationX != 0)
        {
            Vector3 look = new Vector3(rotationX, 0, rotationY);
            transform.rotation = Quaternion.LookRotation(look);
        }

        if (Math.Abs(Input.GetAxis("Fire1")) > 0.5)
        {
            Quaternion parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
        }
        
    }
}
