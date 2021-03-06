﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private bool onIce = false;
    [SerializeField] private AudioClip ouch;
    [SerializeField] private AudioClip pickup;
    [SerializeField] private float ang;

    private FatherController father;
   

    public GameObject spawnPoint;
    private bool died = false;
    private bool doOnce = true;
    private Transform model;
    
    private AudioSource source;
    private Animator anim;
    private CapsuleCollider collider;
    
    public bool holdingWeapon = false;
    
    public PickUp weapon = null;
    public PickUp grenade;
    public Vector3 dir;

    public int hp = 10;
    public float cooldown = 5;

    private GameObject inst;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        ang = 5;
        father = GameObject.Find("Father").GetComponent<FatherController>();
        cooldown = 2.3f;
        collider = GetComponent<CapsuleCollider>();
        model = transform.GetChild(1);
        anim = model.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        inst = transform.GetChild(0).gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void doOnceWhenDead() {
        doOnce = false;
        if (id == 1) FatherController.player0Score++;
        else FatherController.player1Score++; //give points (father holds 'em)
        anim.SetBool("running", false);
        anim.SetBool("death", true);
        if (weapon) Destroy(weapon.gameObject);
    }
    public void respawnAfterCooldown() {
        died = false;
        hp = 10;
        transform.position = new Vector3(spawnPoint.transform.position.x, transform.position.y, spawnPoint.transform.position.z);
        cooldown = 5;
        collider.isTrigger = false;
        doOnce = true;
        anim.SetBool("death", false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (hp <= 0 && !died) {
            cooldown -= Time.deltaTime;
            if (doOnce) {
                doOnceWhenDead();
            }
            _rigidbody.velocity = new Vector3(0, 0, 0); //make hin no move

            if (cooldown <= 0) {
                respawnAfterCooldown();
            }
        }
        
        dir = Vector3.zero;
        dir.x = Input.GetAxis("LeftVertical" + id);
        dir.z = Input.GetAxis("LeftHorizontal" + id);
        
        var rotationX = Input.GetAxis("RightHorizontal" + id);
        var rotationY = -Input.GetAxis("RightVertical" + id);
        
        
        if (id == 0)
        {
            if (Input.GetKey(KeyCode.W))
                dir.x = -1;
            if (Input.GetKey(KeyCode.S))
                dir.x = 1;
            if (Input.GetKey(KeyCode.A))
                dir.z = -1;
            if (Input.GetKey(KeyCode.D))
                dir.z = 1;
            if (Input.GetKey(KeyCode.C))
                transform.Rotate(Vector3.up, -ang);
            if (Input.GetKey(KeyCode.B))
                transform.Rotate(Vector3.up, ang);
            if (Input.GetKey(KeyCode.V))
                if (weapon) weapon.Fire();
        }
        else if (id == 1)
        {
            if (Input.GetKey(KeyCode.I))
                dir.x = -1;
            if (Input.GetKey(KeyCode.K))
                dir.x = 1;
            if (Input.GetKey(KeyCode.J))
                dir.z = -1;
            if (Input.GetKey(KeyCode.L))
                dir.z = 1;
            if (Input.GetKey(KeyCode.Comma))
                transform.Rotate(Vector3.up, -ang);
            if (Input.GetKey(KeyCode.Slash))
                transform.Rotate(Vector3.up, ang);
            if (Input.GetKey(KeyCode.Period))
                if (weapon) weapon.Fire();
        }
        
        if (dir.x < 0 || dir.x > 0 || dir.z < 0 || dir.z > 0) {
            var look = new Vector3(dir.x, 0, dir.z);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation,Quaternion.LookRotation(look),0.3f);
        }

        if (hp > 0) {
            if (onIce) _rigidbody.AddForce(dir.normalized * vel);
            else _rigidbody.velocity = dir.normalized * vel;
        }
        
        if(Math.Abs(_rigidbody.velocity.magnitude) > 0.1)
            anim.SetBool("running", true);
        else
            anim.SetBool("running", false);
        

        if (hp > 0) {
            if (rotationX < 0 || rotationX > 0 || rotationY < 0 || rotationY > 0) {
                var look = new Vector3(rotationX, 0, rotationY);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), 0.3f);
            }
            else {
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
        #if UNITY_STANDALONE_WIN
        if (Input.GetAxis("FireW" + id) > 0.5) {
            if (weapon) weapon.Fire();
        } 

        #endif
        
        #if UNITY_STANDALONE_LINUX
        if (Math.Abs(Input.GetAxis("FireL" + id)) > 0.5) {
            if (weapon) weapon.Fire();
        }  
        #endif
          

        if (Input.GetButton("Back" + id)) {
        }
    }
    
    
    void OnCollisionEnter (Collision collision) {
        
       
        if (collision.gameObject.CompareTag("Bullet")){
            source.clip = ouch;
            source.PlayOneShot(source.clip);
            Destroy (collision.gameObject);
            hp -= 1;
            if (hp < 0) hp = 0;
            father.SetIsHunting(true, collision.gameObject.GetComponent<Bullet>().owner);
        }        
        if (collision.gameObject.CompareTag("SuperBullet")){
            source.clip = ouch;
            source.PlayOneShot(source.clip);
            Destroy (collision.gameObject);
            hp -= 5;
            if (hp < 0) hp = 0;
            father.SetIsHunting(true, collision.gameObject.GetComponent<Bullet>().owner);
        }
    }

    

    void OnTriggerEnter (Collider col)
    {
        
        if (col.gameObject.CompareTag("Ice"))
            onIce = true;
        else if(col.gameObject.CompareTag("Weapon")){
            PickUp rifleScript = col.gameObject.GetComponent<PickUp>();
            if(!rifleScript.isPickedUp) {
                if (weapon != null) Destroy(weapon.gameObject);
                source.clip = pickup;
                source.PlayOneShot(source.clip);
                rifleScript.owner = gameObject;
                weapon = rifleScript;
                holdingWeapon = true;
                rifleScript.isPickedUp = true;
                //Destroy(gameObject);
                if (col.GetComponent<Sniper>())
                {
                    col.GetComponent<Sniper>().LaserActivate();
                }
            }
          //  else if (col.gameObject.CompareTag("Father"))
           //     Debug.Log("chuj kurwa");

            
        }
    }
    
    void OnTriggerExit (Collider col) {
        if (col.gameObject.CompareTag("Ice"))
        {
            onIce = false;
        }
    }

  
}
