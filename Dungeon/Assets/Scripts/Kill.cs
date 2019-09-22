using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Exploded"))
        {
            GameObject temp = null;
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            switch (animator.gameObject.name)
            {
                case "Scuba Guy":
                    temp = Resources.Load<GameObject>("Sprites/b");
                    gameManager.enemiesDead[0] = true;
                    break;
                case "Scuba Guy (1)":
                    temp = Resources.Load<GameObject>("Sprites/m");
                    gameManager.enemiesDead[1] = true;
                    break;
                case "Scuba Guy (2)":
                    temp = Resources.Load<GameObject>("Sprites/g");
                    gameManager.enemiesDead[2] = true;
                    break;
                case "Scuba Guy (3)":
                    temp = Resources.Load<GameObject>("Sprites/w");
                    gameManager.enemiesDead[3] = true;
                    break;
                case "Scuba Guy (4)":
                    temp = Resources.Load<GameObject>("Sprites/u");
                    gameManager.enemiesDead[4] = true;
                    break;
                case "Scuba Guy (5)":
                    temp = Resources.Load<GameObject>("Sprites/y");
                    gameManager.enemiesDead[5] = true;
                    break;
                case "Scuba Guy (6)":
                    temp = Resources.Load<GameObject>("Sprites/f");
                    gameManager.enemiesDead[6] = true;
                    break;
            }
            Instantiate(temp, animator.gameObject.transform.position, Quaternion.identity);
            Destroy(animator.gameObject);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
