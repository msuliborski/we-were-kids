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
            Destroy(collision.gameObject);
            alreadySpilled = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
//            Instantiate(plama, transform.position, Quaternion.Euler(0, 0, 0), transform);
//            transform.GetChild(0).transform.rotation = Quaternion.Euler(90, 0, 0);
//            transform.GetChild(0).transform.position = new Vector3(transform.position.x - 2.0f, transform.position.y - 2.8f, transform.position.z - 3.0f);

        }

    }
}
