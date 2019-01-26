using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioClip music;
    [SerializeField] AudioClip battle;
    // Start is called before the first frame update
    void Start()
    {
        source1.clip = music;
        source2.clip = battle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
