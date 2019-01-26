using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool onIce = false;

    
    
    
    public bool holdingWeapon = false;
    public PickUp weapon;

    public PickUp grenade;
    
    private static int hp = 100;

    private GameObject inst;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        inst = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    private void Update() {


        if (hp <= 0) {
            //Debug.Log("died");
        }
        
        
        var dir = Vector3.zero;
        dir.x = Input.GetAxis("LeftVertical" + id);
        dir.z = Input.GetAxis("LeftHorizontal" + id);
        if (onIce)_rigidbody.AddForce(dir.normalized * vel);
         else _rigidbody.velocity = dir.normalized * vel;

        var rotationX = Input.GetAxis("RightHorizontal" + id);
        var rotationY = -Input.GetAxis("RightVertical" + id);

        if (rotationX < -0.1 || rotationX > 0.1) {
            var look = new Vector3(rotationX, 0, rotationY);
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(look),0.3f);
        } else {
            _rigidbody.angularVelocity = Vector3.zero;
        }

        if (Math.Abs(Input.GetAxis("Fire" + id)) > 0.5) {
            var parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
        }    

        if (Input.GetButton("Back" + id)) {
            if (weapon) weapon.Fire();
        }
    }
    
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.CompareTag("Bullet")){
//            var hit = collision.contacts[0]; 
            //var rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //Instantiate (explosionPrefab, hit.point, rot);
            Destroy (collision.gameObject);
            hp -= 10;
        }
      }
    
    
    void OnTriggerEnter (Collider col) {
                       if (col.gameObject.CompareTag("Ice"))
                       {
                           onIce = true;
                       }
                   }
    
    void OnTriggerExit (Collider col) {
        if (col.gameObject.CompareTag("Ice"))
        {
            onIce = false;
        }
    }
    
    
}
