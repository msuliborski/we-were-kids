using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Controls : MonoBehaviour
{

	[SerializeField] private GameObject _gameObject;
	
	void Start () {
		
	}
	
	void Update()
	{
		if (Input.anyKeyDown)
		{
			_gameObject.SetActive(false);
		}
}
}
