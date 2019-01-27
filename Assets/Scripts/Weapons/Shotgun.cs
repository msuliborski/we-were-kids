using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PickUp
{
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip noAmmo;
    private AudioSource source;
    public GameObject projectile;
    private bool play = true;
    void Start() {
        shootCooldown = 1.2f;
        ammo = 60;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isPickedUp) {
            if (play)
            {
                source.PlayOneShot(source.clip);
                play = false;
            }
            Quaternion rot = Quaternion.Euler(owner.transform.GetChild(0).rotation.eulerAngles.x, owner.transform.GetChild(0).rotation.eulerAngles.y+90, owner.transform.GetChild(0).rotation.eulerAngles.z);
            transform.position = owner.transform.GetChild(0).position;
            transform.rotation = rot;
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0) shootCooldown = 0f;
    }
    
    public override void Fire() {
        if (Math.Abs(shootCooldown) < 0.01 && ammo >= 5) {
            var ang1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 176, transform.rotation.eulerAngles.z);
            var ang2 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 178, transform.rotation.eulerAngles.z);
            var ang3 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 180, transform.rotation.eulerAngles.z);
            var ang4 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 182, transform.rotation.eulerAngles.z);
            var ang5 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 184, transform.rotation.eulerAngles.z);
            var pos1 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y-4, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos2 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y-2, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos3 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos4 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y+2, owner.transform.GetChild(0).gameObject.transform.position.z);
            var pos5 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y+4, owner.transform.GetChild(0).gameObject.transform.position.z);
            source.clip = shot;
            source.PlayOneShot(source.clip);
            Instantiate(projectile, pos1, ang1, gameObject.transform);
            transform.GetChild(transform.childCount - 1).GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            Instantiate(projectile, pos2, ang2, gameObject.transform);
            transform.GetChild(transform.childCount - 1).GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            Instantiate(projectile, pos3, ang3, gameObject.transform);
            transform.GetChild(transform.childCount - 1).GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            Instantiate(projectile, pos4, ang4, gameObject.transform);
            transform.GetChild(transform.childCount - 1).GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            Instantiate(projectile, pos5, ang5, gameObject.transform);
            transform.GetChild(transform.childCount - 1).GetComponent<Bullet>().owner = owner.GetComponent<PlayerHandler>().gameObject;
            shootCooldown = 1.2f;
            ammo -= 5;
        } else if (ammo == 0) {
            source.clip = noAmmo;
            source.PlayOneShot(source.clip);
        }
    }
    
}