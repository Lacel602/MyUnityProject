using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarShrink : MonoBehaviour
{
    [SerializeField]
    private float testDamage;
    [SerializeField]
    private float shrinkTimerMax = 1f;
    [SerializeField]
    private float shrinkTime = 1f;
    private Image healthBarImage;
    private Image damagedBarImage;
    private float shrinkTimer;
    //private HealthSystem _healthSystem = new HealthSystem();

    [SerializeField]
    private GameObject playerObject;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = playerObject.GetComponent<HealthSystem>();
        healthBarImage = transform.Find("HealthFill").GetComponent<Image>();
        damagedBarImage = transform.Find("DamagedFill").GetComponent<Image>();
    }

    private void Start()
    {
        //_healthSystem.setMaxHealth(100);
        SetHealth(_healthSystem.GetHealthRatio());
        damagedBarImage.fillAmount = healthBarImage.fillAmount;
        Debug.Log("healthBarImage.fillAmount = " + healthBarImage.fillAmount);
        Debug.Log("damagedBarImage.fillAmount = " + damagedBarImage.fillAmount);
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnHealed += HealthSystem_OnHealed;
    }
    private bool hasCoroutineStarted = false;
    private void Update()
    {
        SetHealth(_healthSystem.GetHealthRatio());

        //Testing damage and healing
        if (Input.GetMouseButtonDown(1))
        {
            _healthSystem.Damaged(testDamage);
        }
        if (Input.GetMouseButtonDown(2))
        {
            _healthSystem.Healed(testDamage);
        }

        shrinkTimer -= Time.deltaTime;
        if (shrinkTimer <= 0)
        {
            if (healthBarImage.fillAmount < damagedBarImage.fillAmount)
            {

                if (!hasCoroutineStarted)
                {
                    StartCoroutine(DecreaseOverTime(damagedBarImage.fillAmount, healthBarImage.fillAmount));
                    hasCoroutineStarted = true;
                }

                //damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
                //Debug.Log("damagedBarImage " + damagedBarImage.fillAmount);
            }
        }
    }

    private IEnumerator DecreaseOverTime(float damagedFill, float healthFill)
    {
        {
            float startTime = Time.time;
            float endTime = startTime + shrinkTime;

            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / shrinkTime;
                damagedFill = Mathf.Lerp(damagedFill, healthFill, t);
                damagedBarImage.fillAmount = damagedFill;
                yield return null;
            }
            damagedFill = healthFill;
            damagedBarImage.fillAmount = healthFill;
            hasCoroutineStarted = false;
        }
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        SetHealth(_healthSystem.GetHealthRatio());
        damagedBarImage.fillAmount = healthBarImage.fillAmount;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {

        shrinkTimer = shrinkTimerMax;

        SetHealth(_healthSystem.GetHealthRatio());
    }

    private void SetHealth(float healthRatio)
    {
        healthBarImage.fillAmount = healthRatio;
        Debug.Log("Health" + healthRatio);
    }
}
