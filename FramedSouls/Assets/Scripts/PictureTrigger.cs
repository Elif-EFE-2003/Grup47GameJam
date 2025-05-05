using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureTrigger : MonoBehaviour
{
    public int level_id; // 1, 2, 3
    public string sceneToLoad = "SceneName";
    public Transform cameraTransform;
    public Transform targetPoint;
    public Renderer fadePlaneRenderer;
    public AudioSource effectAudio;
    public float moveSpeed = 2f;
    public float fadeSpeed = 1f;

    public AudioSource backGround;

    private bool startFade = false;
    private Color fadeColor;
    private Vector3 startCamPos;
    private float fadeTimer = 0f;

    private AudioSource effect;

    private void Start()
    {
        effect = GetComponent<AudioSource>();
        fadeColor = fadePlaneRenderer.material.color;
        fadeColor.a = 0f;
        fadePlaneRenderer.material.color = fadeColor;
    }

    private void Update()
    {
        if (!startFade) return;

        fadeTimer += Time.deltaTime;

        float t = Mathf.Clamp01(Mathf.Pow(fadeTimer / 1.2f, 2)); // hizlanan yaklasma
        cameraTransform.position = Vector3.Lerp(startCamPos, targetPoint.position, t);
        cameraTransform.LookAt(transform);

        cameraTransform.position += Random.insideUnitSphere * 0.02f;

        fadePlaneRenderer.transform.Rotate(Vector3.forward, Time.deltaTime * 10f);

        if (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime * fadeSpeed;
            fadePlaneRenderer.material.color = fadeColor;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance.currentState == GameState.PaintingsAwakened && Input.GetKeyDown(KeyCode.E))
        {
            if (!GameManager.Instance.CanEnterLevel(level_id))
            {
                Debug.Log("Bu tabloya zaten girilmiş.");
                return;
            }

            startCamPos = cameraTransform.position;
            startFade = true;

            if (effectAudio != null)
            {
                effectAudio.Play();
                effect.loop = false;
                effect.volume = 0f;
                backGround.volume = 0f;
            }

            GameManager.Instance.galleryReturnPosition = other.transform;
            GameManager.Instance.MarkLevelCompleted(level_id);
        }
    }
}
