using UnityEngine;
using System;
using System.Collections;

public class ShieldManagement : MonoBehaviour
{
    [SerializeField] GameObject shield;

    [SerializeField, Range(3,15)] float protectionTime = 10;
    [SerializeField, Range(1, 10)] float fadingTime = 3;

    MeshRenderer[] allRenderers;
    Collider shieldCollider;

    [SerializeField] float timeLeft;
    [SerializeField] bool shieldActive;

    [SerializeField] Vector3 shipShieldOffset = new Vector3 (0f, 0f, 0.1f);

    void Start()
    {
        allRenderers = shield.GetComponentsInChildren<MeshRenderer>();
        shieldCollider = shield.GetComponentInChildren<Collider>();
        shieldActive = false;
        timeLeft = 0;
        shieldCollider.enabled = false;
        SetVisibility(allRenderers, false);
        shield.transform.position = transform.position - shipShieldOffset;
    }

    void Update()
    {
        shield.transform.position = transform.position - shipShieldOffset;


        if (timeLeft <= 0)
        {
            shieldActive = false;
            shieldCollider.enabled = false;
            SetVisibility(allRenderers, false);

            timeLeft = 0;
        }


        if (timeLeft > 0)

            timeLeft -= Time.deltaTime;
    }

    public void ActivateShield()
    {
        
        if (shieldActive == false)
        {
            shieldActive = true;
            timeLeft += protectionTime;
            shieldCollider.enabled = true;
            SetVisibility(allRenderers, true);
            StartCoroutine(FadingShieldCoroutine());
        }
        else
        {
            StopAllCoroutines();
            timeLeft += protectionTime;
            shieldCollider.enabled = true;
            SetVisibility(allRenderers, true);
            StartCoroutine(FadingShieldCoroutine());
        }
    }

    IEnumerator FadingShieldCoroutine()
    {
        yield return new WaitForSeconds(timeLeft-fadingTime);

        if (timeLeft > 0)
        {
            SetVisibility(allRenderers, false);

            const float flickTime = 0.02f;
            bool v = false;

            for (int i = 0; i < (fadingTime / flickTime); i++)
            {
                SetVisibility(allRenderers, v);
                v = !v;
                yield return new WaitForSeconds(flickTime);
            }

            SetVisibility(allRenderers, false);
            shieldCollider.enabled = false;
            shieldActive = false;

        }
        else
        {
            SetVisibility(allRenderers, false);
            shieldCollider.enabled = false;
            shieldActive = false;
        }

    }

    void SetVisibility(MeshRenderer[] allRenderers, bool visible)
    {
        foreach (var renderer in allRenderers)
            renderer.enabled = visible;
    }
}
