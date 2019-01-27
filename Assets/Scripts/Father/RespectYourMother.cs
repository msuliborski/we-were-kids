using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespectYourMother : MonoBehaviour
{
    [SerializeField] private FatherController father;

    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Bullet") ||
            collision.gameObject.CompareTag("SuperBullet"))
        {
            father.SetIsHunting(true, collision.gameObject.GetComponent<Bullet>().owner);
            Destroy (collision.gameObject);
        }
    }

}

