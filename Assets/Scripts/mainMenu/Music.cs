using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

	[SerializeField] private AudioClip clip;

	private AudioSource source;
	// Use this for initialization
	void Start ()
	{
		source = gameObject.GetComponent<AudioSource>();
		source.clip = clip;
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
