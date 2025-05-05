using UnityEngine;

public class Collectible : MonoBehaviour
{
    public SquirrelSceneManager squirrelSceneManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Item topland�: " + gameObject.name);
            Destroy(gameObject);
            squirrelSceneManager.cherriesCollected++;
        }
    }
}

