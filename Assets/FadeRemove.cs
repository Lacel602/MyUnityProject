using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    public float fadeTime = 1f;

    private float timeElapsed = 0f;
    private SpriteRenderer spriteRenderer;
    private GameObject objToRemove;
    private Color startColor;
    private float newAlpha;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        newAlpha = startColor.a * (1 - timeElapsed / fadeTime);
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        timeElapsed += Time.deltaTime;
        if (timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }

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
