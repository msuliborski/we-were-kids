using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject inst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Quaternion parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
            //projectile.transform.rotation = inst.transform.rotation;
        }
    }
}
