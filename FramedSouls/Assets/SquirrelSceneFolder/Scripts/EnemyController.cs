using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.7f;        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Karakter düşmanın üstündeyse
        float playerY = collision.transform.position.y;
        float enemyY = transform.position.y;

        if (playerY > enemyY + 0.2f) // +0.2f: daha sağlam ayar
        {
            Debug.Log("✅ Üstten bastın! Düşman yok edildi.");
            Destroy(gameObject);

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 6f); // Sekme efekti
            }
        }
        else
        {
            Debug.Log("💀 Yan veya alttan geldin! Oyun başa sarıyor...");
            GameManager.Instance.level1_first_time = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}




