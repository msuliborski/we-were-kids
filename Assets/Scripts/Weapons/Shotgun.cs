using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PickUp
{
    
    public GameObject projectile;
    void Start() {
        shootCooldown = 1.0f;
    }

    // Update is called once per frame
    void Update() {
        if (isPickedUp) {
            transform.position = owner.transform.GetChild(0).position;
            transform.rotation = owner.transform.GetChild(0).rotation;
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0) shootCooldown = 0f;
    }
    
    public override void Fire() {
        if (Math.Abs(shootCooldown) < 0.01) {
            var ang1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 86, transform.rotation.eulerAngles.z);
            var ang2 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 88, transform.rotation.eulerAngles.z);
            var ang3 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            var ang4 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 92, transform.rotation.eulerAngles.z);
            var ang5 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 94, transform.rotation.eulerAngles.z);
            var pos1 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y-4, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos2 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y-2, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos3 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos4 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y+2, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos5 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y+4, owner.transform.GetChild(0).gameObject.transform.position.z);
            Instantiate(projectile, pos1, ang1);
            Instantiate(projectile, pos2, ang2);
            Instantiate(projectile, pos3, ang3);
            Instantiate(projectile, pos4, ang4);
            Instantiate(projectile, pos5, ang5);
            shootCooldown = 1.0f;
        }
    }
    
}