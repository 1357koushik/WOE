using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for IEnumerator

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float regenRate = 5f; // Health per second
    public float damageAmount = 10f; // Damage per zombie attack
    private bool isTakingDamage = false;

    public Image bloodEffect; // Assign this in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player Health: " + currentHealth); // Initial health display

        if (bloodEffect != null)
        {
            Color tempColor = bloodEffect.color;
            tempColor.a = 0; // Make blood effect invisible at start
            bloodEffect.color = tempColor;
        }
    }

    void Update()
    {
        // Regenerate health when not taking damage
        if (!isTakingDamage && currentHealth < maxHealth)
        {
            float previousHealth = currentHealth; // Store old health
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            // Only log if health actually changed
            //if (Mathf.Abs(currentHealth - previousHealth) > 0.01f)
            //{
             //   Debug.Log("Player Health: " + currentHealth);
            //}
        }
    }

    public void TakeDamage(float damageAmount)
    {
        isTakingDamage = true;
        currentHealth -= damageAmount;
        Debug.Log("Player took damage! Health: " + currentHealth);

        // Show blood effect
        if (bloodEffect != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowBloodEffect());
        }

        // Check for Game Over
        if (currentHealth <= 0)
        {
            GameOver();
        }

        // Allow regeneration after a short delay
        Invoke("ResetDamageFlag", 2f);
    }

    void ResetDamageFlag()
    {
        isTakingDamage = false;
    }

    void GameOver()
    {
        Debug.Log("Game Over - Player Health Depleted");
        // Replace with actual game over logic
    }

    IEnumerator ShowBloodEffect()
    {
        // Set initial opacity to max (1.0f)
        Color color = bloodEffect.color;
        color.a = 0.6f; // Adjust for a less intense effect
        bloodEffect.color = color;

        // Slowly fade out the effect
        float fadeDuration = 1.5f; // Duration of fade effect
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0.6f, 0f, elapsedTime / fadeDuration);
            bloodEffect.color = color;
            yield return null; // Wait until the next frame
        }

        // Ensure the effect is completely gone
        color.a = 0f;
        bloodEffect.color = color;
    }
}