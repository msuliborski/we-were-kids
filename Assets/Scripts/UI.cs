using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private Image p0;
    private TextMeshProUGUI p0points;
    private TextMeshProUGUI p0bullets;
    private TextMeshProUGUI p1points;
    private TextMeshProUGUI p1bullets;
    private Image p1;
    private PlayerHandler player0;
    private PlayerHandler player1;
    
    void Start()
    {
        player0 = GameObject.Find("Player0").GetComponent<PlayerHandler>();
        player1 = GameObject.Find("Player1").GetComponent<PlayerHandler>();
        p0 = transform.GetChild(0).GetComponent<Image>();
        p1 = transform.GetChild(1).GetComponent<Image>();
        p0points = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        p0bullets = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        p1points = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        p1bullets = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player0.hp == 0)
        {
            p0.enabled = false;
        }
        else
        {
            p0.enabled = true;
            p0.sprite = sprites[player0.hp - 1];
        }


        if (player1.hp == 0)
        {
            p1.enabled = false;
        }
        else
        {
            p1.enabled = true;
            p1.sprite = sprites[player1.hp - 1];
        }


        p0points.text = FatherController.player0Score.ToString();
        p1points.text = FatherController.player1Score.ToString();

        if (player0.weapon == null)
            p0bullets.text = "0";
        else
            p0bullets.text = player0.weapon.ammo.ToString();

        if (player1.weapon == null)
            p1bullets.text = "0";
        else
            p1bullets.text = player1.weapon.ammo.ToString();
        
//        Debug.Log(player0.hp+"\t"+player1.hp);
    }
}
