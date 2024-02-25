using Cainos.LucidEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingObject : MonoBehaviour
{
    private bool shaking = false;

    public Vector2 shakeAmount;
    public float shakeTime;
    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;
    }
    private void Update()
    {
        if (shaking)
        {
            transform.position = new Vector3 (originPos.x + (Random.insideUnitSphere.x * shakeAmount.x), originPos.y + (Random.insideUnitSphere.y * shakeAmount.y), originPos.z);
        }
    }

    public void ShakeNow()
    {
        Debug.Log("Shakingggg");
        StartCoroutine("Shake");
    }

    private IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        if (!shaking)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(shakeTime);
        
        shaking = false;
        transform.position = originalPos;
    }
}
