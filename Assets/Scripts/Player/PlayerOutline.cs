using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutline : MonoBehaviour
{
    private MeshRenderer renderer;
    [SerializeField] private Shader a;
    [SerializeField] private Shader b;


    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }
  

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            renderer.material.shader = b;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
            renderer.material.shader = a;
    }
}
