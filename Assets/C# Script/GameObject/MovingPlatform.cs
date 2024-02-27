using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private bool hasToActivate = false;
    [HideInInspector]
    public bool isActivated = false;
    [SerializeField]
    private GameObject[] waypoints;
    private int waypointIndex = 0;
    [SerializeField]
    private float platformSpeed = 2f;
    // Update is called once per frame
    private void Update()
    {
        if (!hasToActivate)
        {
            Moving();
        } else
        {
            if (isActivated)
            {
                Moving();
            }
        }
    }

    private void Moving()
    {
        if (Vector2.Distance(waypoints[waypointIndex].transform.position, transform.position) < .1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * platformSpeed);
    }
}
