using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinZoneDetector : MonoBehaviour
{

    public Level3_SceneManager level3SceneManager; // Level3_SceneManager referansı
    public GameObject dialoguePanel; // Diyalog paneli
    public TypewriterEffect typeWriterEffect; // Yazı efekti referansı

    void Start()
    {
        if (fadeRenderer != null)
        {
            fadeColor = fadeRenderer.material.color;
            fadeColor.a = 0f;
            fadeRenderer.material.color = fadeColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Karakterin tag'ini "Player" olarak kontrol et
        if (collision.CompareTag("Player"))
        {
            level3SceneManager.gameStarted = false; // Oyunu durdur
            StartCoroutine(HandleExitSequence()); // Çıkış sekansını başlat
           
        }
    }

    public AudioSource effectSound; // Ses efekti
    public GameObject player; // Oyuncu nesnesi
    public Transform cameraTransform; // Kamera transformu
    public Renderer fadeRenderer; // Fade renderer
    public float zoomOutDistance = 5f; // Uzaklaşma mesafesi    
    public float transitionSpeed = 1f; // Geçiş hızı
    private Color fadeColor; // Fade rengi
    IEnumerator HandleExitSequence()
    {
        dialoguePanel.SetActive(true); // Diyalog panelini aç
        typeWriterEffect.StartTyping("I can finally get out of here.");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false); // Diyalog panelini kapat

        if (effectSound != null)
            effectSound.Play();

        Vector3 startPos = cameraTransform.position;
        Vector3 targetPos = startPos + new Vector3(0, 0, -zoomOutDistance); // Kamera uzaklaşıyor

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            cameraTransform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, t));

            // Ekran kararma
            if (fadeRenderer != null)
            {
                fadeColor.a = Mathf.Clamp01(t * 1.5f);
                fadeRenderer.material.color = fadeColor;
                player.SetActive(false);
                GameManager.Instance.level1_completed = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.keysCollected++;
        GameManager.Instance.level3_completed = true;
        GameManager.Instance.level3_first_time = false;
        SceneManager.LoadScene("gallery_scene");
    }
}
