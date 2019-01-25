using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour {

    public GameObject Menu;
    public GameObject Credits;
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
        {
            Menu.SetActive(true);
            Credits.SetActive(false);
        }
	}
}
