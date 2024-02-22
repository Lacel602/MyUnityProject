using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechText : MonoBehaviour
{
    [HideInInspector]
    public bool isActive;
    private float currentTime = 0f;
    public float maxExistTime = 3f;

    private void Start()
    {
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxExistTime)
        {
            isActive = false;
            gameObject.SetActive(false);
            currentTime = 0f;
        }
    }
}
