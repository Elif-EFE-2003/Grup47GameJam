using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TypewriterEffect typeWriterEffect;
    public SquirrelSceneManager squirrelSceneManager;

    public float gecikme = 2f;
    public GameObject player;
    public Transform cameraTransform;
    public Renderer fadeRenderer;
    public AudioSource effectSound;

    public float zoomOutDistance = 5f;
    public float transitionSpeed = 1f;

    private Color fadeColor;
    private bool transitionStarted = false;

    void Start()
    {
        if (fadeRenderer != null)
        {
            fadeColor = fadeRenderer.material.color;
            fadeColor.a = 0f;
            fadeRenderer.material.color = fadeColor;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && squirrelSceneManager.allCherriesCollected && !transitionStarted)
        {
            transitionStarted = true;
            StartCoroutine(HandleExitSequence());
        }
    }

    IEnumerator HandleExitSequence()
    {
        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("There it is... the key!");

        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("I should return to the gallery now.");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);

        dialoguePanel.SetActive(false);

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
        SceneManager.LoadScene("gallery_scene");
    }
}
