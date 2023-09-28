using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlatformMovement platformMovement;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        platformMovement = player.GetComponent<PlatformMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            platformMovement.ReturnLastGroundedPosition();
            Debug.Log("Trap Trigger");
        }
    }

}
