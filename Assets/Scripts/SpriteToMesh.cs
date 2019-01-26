using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteToMesh : MonoBehaviour
{ 
    
    // Start is called before the first frame update
    void Awake()
    {
        
        SpriteToMeshFunc(GetComponent<SpriteRenderer>().sprite);
    }

    private void SpriteToMeshFunc(Sprite sprite)
    {
        Mesh mesh = new Mesh
        {
            vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i),
            uv = sprite.uv,
            triangles = Array.ConvertAll(sprite.triangles, i => (int)i)
        };

        AssetDatabase.CreateAsset(mesh, "Assets/Meshes/kurwa.mesh");
        AssetDatabase.SaveAssets();
        
    }
}
