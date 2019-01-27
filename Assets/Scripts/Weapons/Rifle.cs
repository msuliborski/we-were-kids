using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class Rifle : PickUp
{
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip noAmmo;
    private AudioSource source;
    public GameObject projectile;
    private bool play = true;
    void Start() {
        shootCooldown = 0.2f;
        ammo = 50;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isPickedUp)
        {
            Quaternion rot = Quaternion.Euler(owner.transform.GetChild(0).rotation.eulerAngles.x, owner.transform.GetChild(0).rotation.eulerAngles.y, 125);
            
            if (play)
            {
                source.PlayOneShot(source.clip);
                play = false;
            }
            transform.position = owner.transform.GetChild(0).position;
            transform.rotation = rot;
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0) shootCooldown = 0f;
    }
    
    public override void Fire() {
        if (Math.Abs(shootCooldown) < 0.01 && ammo > 0) {
            var parent = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            var temp = Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent);
            temp.GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            source.clip = shot;    
            source.PlayOneShot(source.clip);
            shootCooldown = 0.2f;
            ammo -= 1;
        } else if (ammo == 0) {
            source.clip = noAmmo;
            source.PlayOneShot(source.clip);
        }
    }
    
}
