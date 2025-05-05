using UnityEngine;

public class MineController : MonoBehaviour
{
    public GameObject explosion;
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Level2_PlayerController player = other.GetComponent<Level2_PlayerController>();
            if (player != null)
            {
                player.Hurt(damage);
            }

            if (explosion != null)
                Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
