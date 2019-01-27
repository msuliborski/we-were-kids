using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private Image p0;
    private Image p1;
    private PlayerHandler player0;
    private PlayerHandler player1;
    
    void Start()
    {
        player0 = GameObject.Find("Player0").GetComponent<PlayerHandler>();
        player1 = GameObject.Find("Player1").GetComponent<PlayerHandler>();
        p0 = transform.GetChild(0).GetComponent<Image>();
        p1 = transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if (player0.hp == 0)
            p0.enabled = false;
        else
            p0.enabled = true;
        
        if (player1.hp == 0)
            p1.enabled = false;
        else
            p1.enabled = true;
        
        p0.sprite = sprites[player0.hp];
        p1.sprite = sprites[player1.hp];
    }
}
