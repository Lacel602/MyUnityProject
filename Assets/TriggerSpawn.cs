using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.VFX;

public class TriggerSpawn : MonoBehaviour
{
    [SerializeField]
    private SpawnObject spawnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnObject.SpawnEnemy();
        }
    }
}
