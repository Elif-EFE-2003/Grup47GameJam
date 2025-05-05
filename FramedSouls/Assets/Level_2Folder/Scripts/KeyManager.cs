using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    public GameObject keyObject; // Key objesini buraya sürükle
    private int collectedCount = 0;
    private int totalRequired = 3;

    public GameObject dialoguePanel; 
    public TypewriterEffect typeWriterEffect;
        public Renderer fadeRenderer;
    private Color fadeColor;

    void Start()
    {
        keyObject.SetActive(false); // Key başta görünmesin
        if (fadeRenderer != null)
        {
            fadeColor = fadeRenderer.material.color;
            fadeColor.a = 0f;
            fadeRenderer.material.color = fadeColor;
        }
    }

    public void CollectShell()
    {
        collectedCount++;

        if (collectedCount >= totalRequired)
        {
            keyObject.SetActive(true); // Key'i görünür yap
        }
    }
    public AudioSource effectSound;
    public Transform cameraTransform;
    public float zoomOutDistance = 5f;
    public float transitionSpeed = 1f;
    public GameObject player;

    public void CollectKey()
    {
        if (collectedCount >= totalRequired)
        {
            StartCoroutine(HandleExitSequence());
            collectedCount = 0; 
        }
    }
    public UnderWaterSceneManager underWaterSceneManager;
    IEnumerator HandleExitSequence()
    {
        GameManager.Instance.level2_completed = true;
        underWaterSceneManager.gameStarted = false;
        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("There it is... the key!");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("So... every drawing really is a world of its own...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);
        if(effectSound != null)
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
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.keysCollected++;
        SceneManager.LoadScene("gallery_scene");

        // Mevcut key sayısını al, 1 ekle ve kaydet
        //int currentKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        //PlayerPrefs.SetInt("TotalKeys", currentKeys + 1);
        //PlayerPrefs.Save();

        // Sahneyi sıfırla
    }
}
