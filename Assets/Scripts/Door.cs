using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public GameObject mesajPaneli;
    public float gecikme = 2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tebrikler, kapýya ulaþtýn! (Anahtar temsili)");
            if (mesajPaneli != null)
                mesajPaneli.SetActive(true);

            Invoke("BolumuBitir", gecikme);
        }
    }

    void BolumuBitir()
    {
        // Sahneyi ilerlet
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Veya sadece sahneyi durdur:
        // Time.timeScale = 0;
    }
}

