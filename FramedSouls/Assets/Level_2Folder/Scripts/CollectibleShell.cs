using UnityEngine;

public class CollectibleShell : MonoBehaviour
{
    private KeyManager keyManager;
    public PlayerHealth playerHealth;
    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.AddScore(1);
            keyManager.CollectShell();
            Destroy(gameObject);
        }
    }
}
