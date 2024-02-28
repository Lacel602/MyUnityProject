using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankLogic : MonoBehaviour
{
    private MovingPlatform platform;
    private Animator animator;

    private bool activated
    {
        get
        {
            return animator.GetBool("activated");
        }
        set
        {
            animator.SetBool("activated", value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        platform = transform.parent.Find("MovingPlatform").Find("SpritePlatform").GetComponent<MovingPlatform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        platform.isActivated = !platform.isActivated;
        activated = !activated;
    }
}
