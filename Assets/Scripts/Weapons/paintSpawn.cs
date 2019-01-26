using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject plama;
    private bool alreadySpilled = false;
    
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.CompareTag("Bullet") && !alreadySpilled) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = true;
            Debug.Log("gwd");
            Instantiate(plama, transform.position, Quaternion.Euler(0, 0, 0), transform);
            transform.GetChild(0).transform.rotation = Quaternion.Euler(90, 0, 0);
            transform.GetChild(0).transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            Destroy(collision.gameObject);
            alreadySpilled = true;
        }

    }
}
