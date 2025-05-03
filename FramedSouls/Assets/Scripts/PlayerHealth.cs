using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private PlayerController playerController;

    public TMP_Text Hp;
    public TMP_Text Score;

    private int scoreValue = 0;

    void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        UpdateHPUI();
        UpdateScoreUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHPUI();

        if (currentHealth <= 0 && playerController != null)
        {
            playerController.Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHPUI();
    }

    public void AddScore(int amount)
    {
        scoreValue += amount;
        UpdateScoreUI();
    }

    void UpdateHPUI()
    {
        if (Hp != null)
            Hp.text = "HP: " + currentHealth.ToString();
    }

    void UpdateScoreUI()
    {
        if (Score != null)
            Score.text = "Score: " + scoreValue.ToString();
    }
}
