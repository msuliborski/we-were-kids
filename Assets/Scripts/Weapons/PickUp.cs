using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public abstract class PickUp : MonoBehaviour
{
    public bool isPickedUp = false;
    public GameObject owner;
    public float shootCooldown;
    public int ammo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Fire();


}
