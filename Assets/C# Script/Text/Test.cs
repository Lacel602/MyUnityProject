using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Transform damagePopUp;
    void Start() 
    {
        Instantiate(damagePopUp, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
