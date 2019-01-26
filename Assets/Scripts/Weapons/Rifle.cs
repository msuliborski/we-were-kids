using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class Rifle : PickUp
{
    
    public GameObject projectile;
    void Start() {
        shootCooldown = 0.3f;
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
            var parent = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent);
            shootCooldown = 0.3f;
        }
    }
    
}
