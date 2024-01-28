using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUNenemy : StateMachineBehaviour
{
    Rigidbody2D rb;
    ulitka_muve ulitka_Muve;

    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        ulitka_Muve = animator.GetComponent<ulitka_muve>();
        player = GameObject.FindWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ulitka_Muve.ChangeDirection();
        rb.velocity = new Vector2(ulitka_Muve.direction.x * ulitka_Muve.speed, rb.velocity.y); //скорость

        float distance = Vector3.Distance(animator.transform.position, player.position); //дистанция между улиткой и игроком
        if (distance < 1)
        {
            animator.SetTrigger("kus");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
