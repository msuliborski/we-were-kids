using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    //[SerializeField] public int id;

    [SerializeField] public GameObject player;
    private Vector3 offset; 
    void Start(){
        //player = GameObject.Find("Player"+id);
        
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update(){
        transform.position = player.transform.position + offset;
    }
}
