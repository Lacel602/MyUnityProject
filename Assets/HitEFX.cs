using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEFX : MonoBehaviour
{
    public GameObject[] listEFX;

    public void PlayEFX()
    {
        if (listEFX != null)
        {
            float number = Random.Range(0, 10);
            int index;
            if (number >= 5)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }
            GameObject efx = Instantiate(listEFX[index], transform.position, Quaternion.identity);
            Debug.Log(efx.name);
        }
    }
}
