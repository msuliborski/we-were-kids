using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GetMesh : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start() {
        GetComponent<MeshFilter>().sharedMesh = GameObject.Find("GameObjectAdd").GetComponent<MeshFilter>().sharedMesh;
    }

    
}
