using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherOutline : MonoBehaviour
{
    [SerializeField] private List<Outline> outlines;
    
    void Start()
    {
        for (int i = 0; i < transform.parent.GetChild(0).childCount; i++)
        {
            Transform child = transform.parent.GetChild(0).GetChild(i);
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
                outlines.Add(child.GetComponent<Outline>());
        }
    }
  

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            foreach (var outline in outlines)
                outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
            foreach (var outline in outlines)
                outline.enabled = false;
    }
}
