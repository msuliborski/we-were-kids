using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class levelFadeOut : MonoBehaviour {

	Animator animator;
	
	// Use this for initialization
	void Start ()
	{
		animator.Play("fadeOut");
	}
}
