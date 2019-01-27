using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PickUp
{
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip noAmmo;
    private AudioSource source;
    public GameObject projectile;
    private bool play = true;
    public int ammo = 20;
    void Start() {
        shootCooldown = 0.8f;
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
            transform.position = new Vector3(owner.transform.GetChild(0).position.x, owner.transform.GetChild(0).position.y, owner.transform.GetChild(0).position.z);
            transform.rotation = owner.transform.GetChild(0).rotation;
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0) shootCooldown = 0f;
    }
    
    public override void Fire() {
        if (Math.Abs(shootCooldown) < 0.01 && ammo > 0) {
            var parent = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            Instantiate(projectile, owner.transform.GetChild(0).gameObject.transform.position, parent);
            source.clip = shot;
            source.PlayOneShot(source.clip);
            shootCooldown = 0.8f;
            ammo -= 1;
        } else if (ammo == 0) {
            source.clip = noAmmo;
            source.PlayOneShot(source.clip);
        }
    }
    
}
