using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public abstract class PickUp : MonoBehaviour
{
    public bool isPickedUp = false;
    public GameObject owner;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp) {
            transform.position = owner.transform.GetChild(0).position;
        }
       
    }
    void OnCollisionEnter (Collision col) {
        if(col.gameObject.CompareTag("Player")){
            PlayerHandler playerScript = col.gameObject.GetComponent<PlayerHandler>();
            if(gameObject != playerScript.weapon) {
                isPickedUp = true;
                owner = col.gameObject;
                Destroy(playerScript.weapon.gameObject);
                playerScript.weapon = gameObject;
                playerScript.holdingWeapon = true;
                //Destroy(gameObject);
            }
        }
    }

    public abstract void Fire();
    
    
}
