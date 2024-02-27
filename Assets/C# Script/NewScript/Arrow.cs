using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    // Start is called before the first frame update
    private Rigidbody2D rb2d;

    public float attackDamage = 10f;
    public Vector2 knockbackForce = Vector2.zero;

    public float mutiplier = 1f;
    [SerializeField]
    private Vector2 moveSpeed = new Vector2(15, 0);
    private bool isCritical = false;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        float moveForce = transform.localScale.x >= 0 ? moveSpeed.x * transform.localScale.x + mutiplier * 7f * random : moveSpeed.x * transform.localScale.x - mutiplier * 7f * random;  
        rb2d.AddForce(new Vector2(moveForce, moveSpeed.y * mutiplier * 0.5f * random), ForceMode2D.Impulse);

        if (mutiplier >= 3)
        {
            if (UnityEngine.Random.Range(1, 100) > 0)
            {
                isCritical = true;
            }
            else
            {
                isCritical = false;
            }
        }
    }

    private float currentDestroyTime = 0;
    public float destroyTime = 5f;
    // Update is called once per frame
    void Update()
    {
        if (!rb2d.isKinematic)
        {
            TrackMovement();
        }
        if (autoDestroy)
        {
            currentDestroyTime += Time.deltaTime;
            if (currentDestroyTime > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool canHit = true;
    [HideInInspector]
    public bool canBePickedUp = false;
    private bool autoDestroy = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            canHit = false;
            canBePickedUp = true;
            //Start auto destroy
            autoDestroy = true;
        }
        else if (collision.CompareTag("Player"))
        {
        }
        else
        {
            Damagable damagable = collision.GetComponent<Damagable>();
            if (damagable != null && canHit)
            {

                knockbackForce = new Vector2(knockbackForce.x * (mutiplier * 0.5f), knockbackForce.y * (mutiplier * 0.5f));
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockbackForce : new Vector2(-knockbackForce.x, knockbackForce.y);
                damagable.Hit(attackDamage * (mutiplier * 0.5f), deliveredKnockback, isCritical);

                //Destroy when hit
                Destroy(gameObject);
            }
        }
    }
    private void TrackMovement()
    {
        Vector2 direction = rb2d.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (transform.localScale.x >= 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.AngleAxis(180, Vector3.forward);
        }

    }
}
