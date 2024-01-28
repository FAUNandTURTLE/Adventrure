using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : StateMachineBehaviour
{
    Rigidbody2D rb;
    ulitka_muve ulitka_Muve;

    float timer = 0;
    float waitPatrol;
    float waitChangeDir;
    float timerChangeDir = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        waitPatrol = Random.Range(5, 10);
        waitChangeDir = Random.Range(4, 6);
        ulitka_Muve = animator.GetComponent<ulitka_muve>();
        timer = 0;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        rb.velocity = new Vector2(ulitka_Muve.direction.x * ulitka_Muve.speed, rb.velocity.y); //скорость

        timer += Time.deltaTime;
        timerChangeDir += Time.deltaTime;

        if (timerChangeDir > waitChangeDir)
        {
            ulitka_Muve.direction *= -1;
            timerChangeDir = 0;
        }
        if (timer > waitPatrol)
        {
            animator.SetBool("Patrol", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
