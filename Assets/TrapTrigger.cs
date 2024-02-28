using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private GameObject spikeTrap;

    private void Awake()
    {
        spikeTrap = transform.parent.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spikeTrap.SetActive(true);
        }
    }
}
