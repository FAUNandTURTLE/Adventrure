using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class letat : MonoBehaviour
{
    public float speed;
    public float agroSpeed;

    private Health goose;
    private Rigidbody2D Fisika;
    private Transform player;
    private Vector2 direction = Vector2.right;

    public bool Agro = false;

    private float startSpeed;

    public Vector2[] dirs;
    public float damage;

    private Day1nigt day1nigt;
    private float lastDamageTime;
    

    public float GetStartSpeed() { return startSpeed; }
    void Start()
    {
        goose = GetComponent<Health>();
        Fisika = GetComponent<Rigidbody2D>();
        startSpeed = speed;
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(changeDir());

        day1nigt = FindFirstObjectByType<Day1nigt>();
    }


    void Update()
    {
        if (goose.dead == true) 
        {
            Fisika.gravityScale = 1.0f;
            this.enabled = false;
            player.GetComponent<HeroKnight>().AddDiedEyes();
        }

        if (day1nigt.isDay == true && lastDamageTime < Time.time - 2)
        {
            lastDamageTime = Time.time;
            goose.TakeDamage(5);

            if (goose.dead == true)
            {
                Fisika.gravityScale = 1.0f;
                this.enabled = false;
            }
        }


        float xNapr = 1;

        if (Agro)
        {
            Vector3 playerpos = player.position;
            playerpos.y += 1;
            direction = (playerpos - transform.position).normalized;
        }

        if (direction.x < 0)
        {
            xNapr = -1;
        }
        else if (direction.x > 0)
        {
            xNapr = 1;
        }

        transform.localScale = new Vector2(xNapr, 1);
    }

    private void FixedUpdate()
    {
        Fisika.velocity = direction* speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && goose.dead == false)
        {
            player.GetComponent<HeroKnight>().TakeDamage(damage);
            hit();
        }
    }

    IEnumerator changeDir()
    {
        while (Agro == false)
        {
            direction = dirs[Random.Range(0, dirs.Length)];
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    IEnumerator changeAgroSpeed()
    {
        speed *= -1;
        yield return new WaitForSeconds(0.5f);
        speed = agroSpeed;
        if (transform.Find("ZoneSpeedFast").GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            speed = transform.Find("ZoneSpeedFast").GetComponent<speedZoneScript>().speed;
        }
    }

    public void hit()
    {
        StartCoroutine(changeAgroSpeed());
    }


}
