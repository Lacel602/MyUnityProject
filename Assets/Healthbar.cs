using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float health, float duration)
    {
        float currentHealth = slider.value;
        float targetHealth = currentHealth - health;

        StartCoroutine(LerpHealth(currentHealth, targetHealth, duration));
    }

    private IEnumerator LerpHealth(float startHealth, float targetHealth, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;

            slider.value = Mathf.Lerp(startHealth, targetHealth, time / duration);
            yield return null;
        }
        // Ensure the slider ends up with the exact target value.
        slider.value = targetHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SetHealth(100, 1f);
        }
    }
}
