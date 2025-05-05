using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    FreeRoam,
    DoorChecked,
    ClockChimed,
    PaintingsAwakened,
    Done
}

public class GameManager : MonoBehaviour
{
    public Transform galleryReturnPosition;
    public int keysCollected = 0;
    public TypewriterEffect typeWriterEffect;
    public GameObject dialoguePanel;

    public bool gallery_scene_first_time = true;
    public bool level1_first_time = true;
    public bool level1_completed = false;
    public bool level2_first_time = true;
    public bool level2_completed = false;
    public bool level3_first_time = true;
    public bool level3_completed = false;

    public static GameManager Instance;
    public GameState currentState = GameState.Intro;
    public List<AudioSource> paintingAudios;

    private void Awake()
    {
       if (Instance != null && Instance != this)
       {
        Destroy(gameObject); // Eğer başka bir GameManager varsa, onu yok et
        return;
       }
       Instance = this;
       DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "gallery_scene")
        {
            if (currentState == GameState.PaintingsAwakened)
            {
                foreach (var audio in paintingAudios)
                {
                    if (audio != null && !audio.isPlaying)
                        audio.Play();
                }
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (galleryReturnPosition == null)
        {
            print("E GIRR");
            new Vector3(5.09f,4.1f,9.58f);
        }
        if (SceneManager.GetActiveScene().name == "gallery_scene" && gallery_scene_first_time)
        {
            StartCoroutine(ShowIntro());
            gallery_scene_first_time = false;
        }
        
        player.transform.position = galleryReturnPosition.position;
    }

    IEnumerator ShowIntro()
    {

        yield return new WaitForSeconds(2.5f);

        dialoguePanel.SetActive(true);

        typeWriterEffect.StartTyping("Looks like I'm the only one left... I should get going.");

        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);

        yield return new WaitForSeconds(1);

        currentState = GameState.FreeRoam;

        dialoguePanel.SetActive(false);

    }



public bool CanEnterLevel(int id)
{
    switch (id)
    {
        case 1: return !level1_completed;
        case 2: return !level2_completed;
        case 3: return !level3_completed;
        default: return true;
    }
}

public void MarkLevelCompleted(int id)
{
    switch (id)
    {
        case 1: level1_completed = true; break;
        case 2: level2_completed = true; break;
        case 3: level3_completed = true; break;
    }
}



private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "gallery_scene")
    {
        paintingAudios.Clear();

        var tablo1 = GameObject.Find("girilecektablo_1");
        var tablo2 = GameObject.Find("girilecektablo_2");
        var tablo3 = GameObject.Find("girilecektablo_3");

        // Eğer tamamlanmamışsa sesini listeye ekle
        if (!level1_completed && tablo1 != null)
            paintingAudios.Add(tablo1.GetComponent<AudioSource>());

        if (!level2_completed && tablo2 != null)
            paintingAudios.Add(tablo2.GetComponent<AudioSource>());

        if (!level3_completed && tablo3 != null)
            paintingAudios.Add(tablo3.GetComponent<AudioSource>());

        if (currentState == GameState.PaintingsAwakened)
        {
            foreach (var audio in paintingAudios)
            {
                if (audio != null && !audio.isPlaying)
                    audio.Play();
            }
        }
    }
}

private void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

private void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}


}
