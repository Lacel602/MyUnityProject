using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    public GameObject[] itemToSpawn;

    [SerializeField]
    private Vector2 itemVelocity = new Vector2(1, 5);

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
                //Spawn item
                GameObject spawnedItem;
                Rigidbody2D itemRb;
                if (itemToSpawn.Length > 0)
                {
                    foreach (var item in itemToSpawn)
                    {
                        spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
                        itemRb = spawnedItem.GetComponent<Rigidbody2D>();
                        itemRb.velocity = new Vector2(Random.Range(-itemVelocity.x, itemVelocity.x), Random.Range(itemVelocity.y - 1f, itemVelocity.y + 1f));
                    }
                }
                //Destroy object
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
