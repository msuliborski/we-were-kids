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
            var parent1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 80, transform.rotation.eulerAngles.z);
            var parent2 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 85, transform.rotation.eulerAngles.z);
            var parent3 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            var parent4 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 95, transform.rotation.eulerAngles.z);
            var parent5 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 100, transform.rotation.eulerAngles.z);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent1);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent2);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent3);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent4);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent5);
            shootCooldown = 1.0f;
        }
    }
    
}