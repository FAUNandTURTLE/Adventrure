using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedZoneScript : MonoBehaviour
{
    public letat enemy;

    public float speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Animator>().SetTrigger("Attack");
            enemy.speed *=4;
            speed = enemy.speed;
        }
    }
}
