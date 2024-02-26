using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public Vector2 knockBack = Vector2.zero;
    public float damage = 1000f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damagable damagable = collision.gameObject.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.Hit(damage, knockBack, false);
                NewPlayerController controller = collision.gameObject.GetComponent<NewPlayerController>();
                ParticleSystem deathEFX = controller.transform.Find("DeathEFX").GetComponent<ParticleSystem>();
                deathEFX.Play();
                controller.IsAlive = false;
                //controller.ReturnToLastGroundPos();
            }
        }
    }

}
