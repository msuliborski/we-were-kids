using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private bool onIce = false;
    [SerializeField] private AudioClip ouch;
    [SerializeField] private AudioClip pickup;
    

    public GameObject spawnPoint;
    private bool died = false;
    private bool doOnce = true;
    private float cooldown = 5;
    private Transform model;
    
    private AudioSource source;
    
    
    
    public bool holdingWeapon = false;
    
    public PickUp weapon = null;
    public PickUp grenade;
    
    public int hp = 100;

    private GameObject inst;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        model = transform.GetChild(1);
        _rigidbody = GetComponent<Rigidbody>();
        inst = transform.GetChild(0).gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void FixedUpdate() {
       
        if (hp <= 0 && !died) {
            //Debug.Log("died");
            cooldown -= Time.deltaTime;
            if (doOnce) {
                doOnce = false;
                if (id == 1) FatherController.player0Score++;
                else FatherController.player1Score++; //give points (father holds 'em)
                GetComponent<CapsuleCollider>().isTrigger = true;
            }

            if (cooldown <= 0) {
                died = false;
                hp = 100;
                transform.position = new Vector3(spawnPoint.transform.position.x, transform.position.y, spawnPoint.transform.position.z);
                cooldown = 5;
                GetComponent<CapsuleCollider>().isTrigger = false;
                doOnce = true;
            }
        }
        

        

        if (hp <= 0) {
            //Debug.Log("died");
        }
        
        
        var dir = Vector3.zero;
        dir.x = Input.GetAxis("LeftVertical" + id);
        dir.z = Input.GetAxis("LeftHorizontal" + id);
        
        if (dir.x < 0 || dir.x > 0 || dir.z < 0 || dir.z > 0) {
            var look = new Vector3(dir.x, 0, dir.z);
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation,Quaternion.LookRotation(look),0.3f);
        }
        
        if (onIce)_rigidbody.AddForce(dir.normalized * vel);
        else _rigidbody.velocity = dir.normalized * vel;

        var rotationX = Input.GetAxis("RightHorizontal" + id);
        var rotationY = -Input.GetAxis("RightVertical" + id);
        
        
        
        
        if (rotationX < 0 || rotationX > 0 || rotationY < 0 || rotationY > 0) {
            var look = new Vector3(rotationX, 0, rotationY);
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(look),0.3f);
        } else {
            _rigidbody.angularVelocity = Vector3.zero;
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
            hp -= 10;
        }        
        if (collision.gameObject.CompareTag("SuperBullet")){
            source.clip = ouch;
            source.PlayOneShot(source.clip);
            Destroy (collision.gameObject);
            hp -= 50;
        }
        
    }

    

    void OnTriggerEnter (Collider col) {
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
            }
        }
    }
    
    void OnTriggerExit (Collider col) {
        if (col.gameObject.CompareTag("Ice"))
        {
            onIce = false;
        }
    }
    
    
}
