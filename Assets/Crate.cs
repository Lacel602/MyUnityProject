using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private int numberOfHits = 0;
    private ParticleSystem destroyEFX;
    private ShakingObject gfxShake;

    private void Awake()
    {
        gfxShake = transform.parent.gameObject.GetComponentInChildren<ShakingObject>();
        destroyEFX = transform.parent.gameObject.GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numberOfHits < 2)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                Destroy(collision.gameObject);
            }
            numberOfHits++;
            gfxShake = transform.parent.gameObject.GetComponentInChildren<ShakingObject>();
            gfxShake.ShakeNow();
            if (numberOfHits == 2)
            {
                destroyEFX.Play();
                StartCoroutine("DestroyObject");
                Transform gfx = gameObject.transform.parent.GetChild(0);
                gfx.gameObject.SetActive(false);
                Debug.Log("Destroy crate!");
            }
        }      
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyEFX.startLifetime);
        Destroy(gameObject.transform.parent.gameObject);   
    }
}
