using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{

    Farmer farmer;
    MusicManager Music;
   // bool collideWithWall;
   // bool hasCultist;
    List<GameObject> walls = new List<GameObject>();
   

    void Awake()
    {
        Music = GameObject.Find("Main Camera").GetComponent<MusicManager>();
        farmer = transform.parent.GetComponent<Farmer>();
    }

    /* private void OnTriggerStay(Collider col)
     {
         if (col.gameObject.tag == "wall")
         {
             collideWithWall = true;
         }
     }

     private void OnTriggerExit(Collider col)
     {
         if (col.gameObject.tag == "wall")
         {
             if (!walls.Contains(col.gameObject))
                 walls.Remove(col.gameObject);
             if (walls.Count == 0)
                 collideWithWall = false;
         }
     }*/
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cultist")
        {
            //  hasCultist = true;
            //  if (!collideWithWall)
            //  {
            Cultist cultist = col.gameObject.GetComponent<Cultist>();
            if (cultist.IsBeast)
            {
                farmer._agent.speed = 9f;


            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
       /* if (col.gameObject.tag == "wall")
        {
            if (!hasCultist)
            {
                collideWithWall = true;
                if (!walls.Contains(col.gameObject))
                    walls.Add(col.gameObject);
            }
        }*/
        /*else*/ if (col.gameObject.tag == "cultist")
        {
          //  hasCultist = true;
          //  if (!collideWithWall)
          //  {
                Cultist cultist = col.gameObject.GetComponent<Cultist>();
                if (cultist.IsBeast)
                {
                    farmer._agent.speed = 13f;
                }
          //  }
            // --
            //else
            // Load Win screen scene

        }
    }


    
}

   
