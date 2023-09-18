using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    public Slider damaged;

    public void SetHealth(float health, float duration)
    {
        float currentHealth = healthBar.value;
        float targetHealth = currentHealth - health;
        healthBar.value = targetHealth;
        StartCoroutine(LerpHealth(currentHealth, targetHealth, duration));
    }

    private IEnumerator LerpHealth(float startHealth, float targetHealth, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;

            damaged.value = Mathf.Lerp(startHealth, targetHealth, time / duration);
            yield return null;
        }
        // Ensure the slider ends up with the exact target value.
        damaged.value = targetHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SetHealth(100, 1f);
        }
    }
}
