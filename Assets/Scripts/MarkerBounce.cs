using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBounce : MonoBehaviour
{

	private float init;
	private GameObject player;
	[SerializeField] private float offset = 0.01f;
	[SerializeField] private float maxOffset = 0.2f;
	
	// Use this for initialization
	void Start () {
        player = transform.parent.gameObject;
		init = transform.position.z - player.transform.position.z;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + offset);
		if (((transform.position.z - player.transform.position.z) > (init + maxOffset)) ||
		    ((transform.position.z - player.transform.position.z) < init))
			offset *= -1;
	}
}
