using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlatformMovement platformMovement;
    private HealthSystem healthSystem;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        platformMovement = player.GetComponent<PlatformMovement>();
        healthSystem = player.GetComponent<HealthSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            platformMovement.ReturnLastGroundedPosition();
            healthSystem.Damaged(100);
            Debug.Log("Trap Trigger");
        }
    }

}
