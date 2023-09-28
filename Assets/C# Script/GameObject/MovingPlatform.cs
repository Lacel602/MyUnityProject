using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypointsArray;
    private int waypointIndex = 0;
    [SerializeField]
    private float platformSpeed = 2f;
    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypointsArray[waypointIndex].transform.position, transform.position) < .1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypointsArray.Length)
            {
                waypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypointsArray[waypointIndex].transform.position, Time.deltaTime * platformSpeed);
    }
}
