using UnityEngine;
using TMPro;
using System;
using System.Collections;

class Damaged : MonoBehaviour
{
    [SerializeField, Range(1, 10)] int startHealth = 5;
    [SerializeField] TMP_Text textField;
    [SerializeField] Color fullHealthColor = Color.green;
    [SerializeField] Color zeroHealthColor = Color.red;

    [SerializeField] GameObject gameOverCanvas;
    //[SerializeField] MonoBehaviour[] turnOffAtDeath;
    //[SerializeField] GameObject turnOffAsteroids;
    [SerializeField] float invincibilityTime = 1;

    bool isInvincible = false;

    int currentHealth;

    MeshRenderer[] allRenderers;
    Collider[] allColliders;
    [SerializeField] MonoBehaviour[] turnOffScripts;
    void Start()
    {
        allRenderers = GetComponentsInChildren<MeshRenderer>();
        allColliders = GetComponentsInChildren<Collider>();
        turnOffScripts =GetComponentsInChildren<MonoBehaviour>();
        currentHealth = startHealth;
        UpdateText();
    }
    public void DealDamage(int value)
    {
        if (value <= 0)
            return;

        if (isInvincible)
            return;

        currentHealth -= value;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth == 0)
        {
            Cursor.visible = true;
            gameOverCanvas.SetActive(true);

            for (int i = 0; i < allColliders.Length; i++)
                allColliders[i].enabled = false;
            if (turnOffScripts != null)
            {
                for (int i = 0; i < turnOffScripts.Length; i++)
                    turnOffScripts[i].enabled = false;
            }


        }
        UpdateText();

        StartCoroutine(InvincibilityCoroutine());
    }

    IEnumerator InvincibilityCoroutine()
    {
        if (currentHealth > 0)
        {
            isInvincible = true;
            Time.timeScale = 0.5f;

            SetVisibility(allRenderers, false);

            const float flickTime = 0.02f;
            bool v = false;

            for (int i = 0; i < (invincibilityTime / flickTime); i++)
            {
                SetVisibility(allRenderers, v);
                v = !v;
                yield return new WaitForSeconds(flickTime);
            }

            SetVisibility(allRenderers, true);

            Time.timeScale = 1f;
            isInvincible = false;
        }
        else
        {
            SetVisibility(allRenderers, false);
        }

    }

    void SetVisibility(MeshRenderer[] allRenderers, bool visible)
    {
        foreach (var renderer in allRenderers)
            renderer.enabled = visible;
    }

    void UpdateText()
    {
        if (textField != null)
        {
            textField.text = $"Health: {currentHealth}";
            float t = (float)currentHealth / startHealth;
            textField.color = Color.Lerp(zeroHealthColor, fullHealthColor, t);
        }
    }


}