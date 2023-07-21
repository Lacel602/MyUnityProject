using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update

    public float x = 10f;
    public float y = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, -10);
    }
}
