using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Transform leftLimit;
    [SerializeField] Transform rightLimit;

    private bool movingRight = true;
    private bool isFrozen = false; // Yaratık durdurulmuş mu kontrolü
    private float freezeDuration = 3f; // Dondurulma süresi
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Başlangıç pozisyonunu sakla
    }

    void Update()
    {
        if (isFrozen)
            return; // Eğer yaratık durduysa, hareket etmeyi durdur

        // Sağ veya sol yönde hareket et
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightLimit.position.x)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftLimit.position.x)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Player ile çarpışma anında bu fonksiyon çalışacak
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            StartCoroutine(FreezeAndResume()); // Yaratık duraklatma ve tekrar hareket ettirme
        }
            else
    {
        Debug.Log("Enemy collided with something else: " + collision.gameObject.name);
    }
    }

    private IEnumerator FreezeAndResume()
    {
        isFrozen = true; // Yaratığı durdur
        Debug.Log("Enemy is frozen");

        yield return new WaitForSeconds(freezeDuration); // Belirtilen süre kadar bekle

        isFrozen = false; // Yaratığı hareket ettir
        Debug.Log("Enemy resumes movement");
    }

    // Respawn sırasında yaratık hareketinin başlatılması
    public void ResetEnemyPosition()
    {
        transform.position = initialPosition; // Yaratığın pozisyonunu sıfırla
        isFrozen = false; // Yaratığı serbest bırak
        Debug.Log("Enemy position reset");
    }
}
