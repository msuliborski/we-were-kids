using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PickUp
{
    [SerializeField] private AudioClip shot;
    private AudioSource source;
    public GameObject projectile;
    private bool play = true;
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
            source.clip = shot;
            source.PlayOneShot(source.clip);
            shootCooldown = 0.8f;
        }
    }
    
}
