using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public bool dead = false;
    public float health = 100;

    private Animator animator;

    private string nameObject;

    private void Start()
    {
        nameObject = transform.name;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        if (dead == false)
        {
            if (nameObject == "Bad eye")
            {
                transform.GetComponent<letat>().hit();
            }

            health -= damage;
            animator.SetBool("Run", true);
            animator.SetTrigger("Hit");
            if (health <= 0)
            {
                dead = true;
                animator.SetBool("Dead", true);
                gameObject.layer = 6;
            }
        }
    }
}
