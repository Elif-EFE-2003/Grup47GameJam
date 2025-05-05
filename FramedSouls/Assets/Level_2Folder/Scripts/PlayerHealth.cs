using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Level2_PlayerController level2_PlayerController;

    public TMP_Text Hp;
    public TMP_Text Score;

    private int scoreValue = 0;
    private int remainingShells;

    void Start()
    {
        remainingShells = GameObject.FindGameObjectsWithTag("Collectible").Length;
        currentHealth = maxHealth;
        level2_PlayerController = GetComponent<Level2_PlayerController>();
        UpdateHPUI();
        UpdateScoreUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHPUI();

        if (currentHealth <= 0 && level2_PlayerController != null)
        {
            level2_PlayerController.Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHPUI();
    }

    public void AddScore(int amount)
    {
        remainingShells -= amount;
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
            Score.text = "Remaining Seashells: " + remainingShells.ToString();
    }
}
