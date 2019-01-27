using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;

public class PickUpAnim : MonoBehaviour
{
    [SerializeField] private float ang = 1f;
    public PickUp owned;    

    void Start()
    {
        owned = GetComponent<PickUp>();
    }
    
    void FixedUpdate()
    {
        if (owned.owner == null)
        {
            transform.Rotate(transform.up, ang);    
        }
    }
}
