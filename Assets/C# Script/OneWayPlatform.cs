using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D playerCollider;
    private Collider2D platformCollider;
    public bool isOn = false;

    private void Awake()
    {
        playerCollider = GameObject.Find("NewPlayer").GetComponent<CapsuleCollider2D>();
        platformCollider = transform.parent.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        platformCollider.enabled = false;
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platformCollider.enabled = true;
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platformCollider.enabled = false;
            Physics2D.IgnoreCollision(playerCollider, transform.parent.GetComponent<BoxCollider2D>());
            isOn = false;
        }
    }
}
