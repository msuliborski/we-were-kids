using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private int maxPoints = 5;
    private Camera cam0;
    private Camera cam1;
    [SerializeField] private GameObject win;

    void Start()
    {
        cam0 = GameObject.Find("Camera0").GetComponent<Camera>();
        cam1 = GameObject.Find("Camera1").GetComponent<Camera>();
    }
    
    void Update()
    {
        if (FatherController.player0Score == maxPoints || FatherController.player1Score == maxPoints)
        {
            win.SetActive(true);
            //SceneManager.LoadScene("Menu");
            //Application.LoadLevel(Application.loadedLevel);
            if (FatherController.player0Score == maxPoints)
            {
                win.GetComponent<RectTransform>().anchoredPosition = new Vector2(win.GetComponent<RectTransform>().sizeDelta.x/4, -win.GetComponent<RectTransform>().sizeDelta.y/2);
            }
            else
            {
                win.GetComponent<RectTransform>().anchoredPosition = new Vector2(win.GetComponent<RectTransform>().sizeDelta.x*3/4, -win.GetComponent<RectTransform>().sizeDelta.y/2);
            }
            
            FatherController.player0Score = 0;
            FatherController.player1Score = 0;
        }
    }
}
