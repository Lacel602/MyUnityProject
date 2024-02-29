using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    private float currentFadeTime = 0.0f; 
    private Color originalColor; 
    private SpriteRenderer objectRenderer; 

    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        originalColor = objectRenderer.color;
    }

    void Update()
    {
        if (currentFadeTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(originalColor.a, 0.0f, currentFadeTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            objectRenderer.color = newColor;
            currentFadeTime += Time.deltaTime;
        }

        if (objectRenderer.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}

