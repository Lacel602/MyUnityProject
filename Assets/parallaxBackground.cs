using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
    public Vector2 parallaxMutiplier = Vector2.zero;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float imageLength;
    private float startPos;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        startPos = cameraTransform.position.x;
        imageLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        Vector3 deltamoveMent = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltamoveMent.x * parallaxMutiplier.x, deltamoveMent.y * parallaxMutiplier.y, 0);
        lastCameraPosition = cameraTransform.position;

        //if ((cameraTransform.position.x - startPos) * (1 - parallaxMutiplier.x) >= imageLength)
        //{
        //    transform.position = new Vector3(transform.position.x + imageLength, transform.position.y, transform.position.z);
        //    startPos = cameraTransform.position.x;
        //}
        //else if ((cameraTransform.position.x - startPos) * (1 - parallaxMutiplier.x) < -imageLength)
        //{ 
        //    transform.position = new Vector3(transform.position.x - imageLength, transform.position.y, transform.position.z);
        //    startPos = cameraTransform.position.x;
        //}

        if (cameraTransform.position.x >= transform.position.x + imageLength)
        {
            transform.position = new Vector3(transform.position.x + imageLength, transform.position.y, transform.position.z);
        }
        if (cameraTransform.position.x <= transform.position.x - imageLength)
        {
            transform.position = new Vector3(transform.position.x - imageLength, transform.position.y, transform.position.z);
        }
    }
}
