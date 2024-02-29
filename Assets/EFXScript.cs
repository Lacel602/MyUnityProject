using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFXScript : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);    
    }
}
