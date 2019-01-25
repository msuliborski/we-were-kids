using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenData : MonoBehaviour {

    public int Cnt1, Cnt2;
    public static bool IsCreated = false;

	// Use this for initialization
	void Awake () {
        if (!IsCreated)
        {
            DontDestroyOnLoad(this);
            IsCreated = true;

        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	public void FillData(int cnt1, int cnt2)
    {
        Cnt1 = cnt1;
        Cnt2 = cnt2;
    }
}
