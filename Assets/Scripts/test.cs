using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private GameObject projectile;
    private GameObject inst;
    // Start is called before the first frame update
    void Start() {
        inst = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("LeftVertical"+id);
        dir.z = Input.GetAxis("LeftHorizontal"+id);
        GetComponent<Rigidbody>().velocity = dir.normalized*vel;

        float rotationX = Input.GetAxis("RightHorizontal"+id);
        float rotationY = -Input.GetAxis("RightVertical"+id);

        if (rotationX != 0) {
            Vector3 look = new Vector3(rotationX, 0, rotationY);
            transform.rotation = Quaternion.LookRotation(look);
        }

        if (Math.Abs(Input.GetAxis("Fire"+id)) > 0.5) {
            Quaternion parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
        }

    }
}
