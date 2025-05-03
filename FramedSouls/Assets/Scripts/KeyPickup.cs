using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyManager keyManager;

    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyManager.CollectKey();
            Destroy(gameObject);
        }
    }
}
