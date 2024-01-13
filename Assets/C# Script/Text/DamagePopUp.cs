using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    [SerializeField]
    private float maxExistTime = 1f;
    [SerializeField]
    private Transform damagePopUpText;
    private float existTime;
    private Color textColor;
    private Vector3 moveVector;
    private static int sortingOrder;

    public DamagePopUp Create(Vector3 position, int damageAmount)
    {
        Transform damagePopUpTransform = Instantiate(damagePopUpText, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damageAmount);

        return damagePopUp;
    }
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void SetUp(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        existTime = maxExistTime;
        moveVector = new Vector3(Random.Range(-0.3f, 0.7f), Random.Range(0.7f, 1f)) * Random.Range(15f, 20f);
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (existTime > maxExistTime * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        existTime -= Time.deltaTime;
        if (existTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
