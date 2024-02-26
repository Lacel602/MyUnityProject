using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine("DestroyThisObj");
    }

    private IEnumerator DestroyThisObj()
    {
        yield return new WaitForSeconds(_particleSystem.duration + _particleSystem.startLifetime);
        Destroy(gameObject);
    }
}
