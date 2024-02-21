using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float timeToSpawn;
    private float currentTime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void SpawnEnemy()
    {
        Debug.Log("spawn");
        if (currentTime <= 0)
        {
            Instantiate(spawnObject, transform.position, Quaternion.identity);
            currentTime = timeToSpawn;
        }
    }
}
