using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemy : StateMachineBehaviour
{
    Rigidbody2D rb;
    float timer = 0;
    float waitIdle;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        waitIdle = Random.Range(3, 5);
        timer = 0;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > waitIdle)
        {
            animator.SetBool("Patrol", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
