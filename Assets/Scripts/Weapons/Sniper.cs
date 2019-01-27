using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : PickUp
{
    [SerializeField] private AudioClip shot;
    private AudioSource source;
    public GameObject projectile;
    private bool play = true;
    void Start() {
        shootCooldown = 2f;
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
            Quaternion rot = Quaternion.Euler(owner.transform.GetChild(0).rotation.eulerAngles.x+180, owner.transform.GetChild(0).rotation.eulerAngles.y, 90);
            
            transform.position = owner.transform.GetChild(0).position;
            transform.rotation = rot;
        }
        shootCooldown -= Time.deltaTime;
        if (shootCooldown < 0) shootCooldown = 0f;
    }
    
    public override void Fire() {
        if (Math.Abs(shootCooldown) < 0.01) {
            var ang1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
            var pos1 = new Vector3(owner.transform.GetChild(0).gameObject.transform.position.x, owner.transform.GetChild(0).gameObject.transform.position.y-4, owner.transform.GetChild(0).gameObject.transform.position.z);
            source.clip = shot;
            source.PlayOneShot(source.clip);
            Instantiate(projectile, pos1, ang1);
            shootCooldown = 2f;
        }
    }

    public void LaserActivate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    //public void LaserDectivate()
    //{
    //    transform.GetChild(1).gameObject.SetActive(false);
    //}
    
}