using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DoorCheck : MonoBehaviour
{

    public AudioSource doorCheckAudio;
    public AudioSource clockChimeAudio;
    public AudioSource[] paintingAudios;

    public GameManager gameManager;
    public GameObject dialoguePanel;
    bool isChecked = false;
    public TypewriterEffect typeWriterEffect;
    private bool triggered = false;
    public SaatCheck saatCheck;
    public AudioSource unlockSound;

    public RawImage fadeOutPanel; // siyah Image
    public TextMeshProUGUI finalText; // "SON" yazısı
    public float fadeDuration = 2f;


    private void OnTriggerStay(Collider other)
    {
        if (triggered)
        {
            return;
        }
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            doorCheckAudio.Play();
            if (GameManager.Instance.currentState == GameState.FreeRoam)
            {
                GameManager.Instance.currentState = GameState.DoorChecked;
                StartCoroutine(HandleDoorDialogue());
                triggered = true;
            }

            if (GameManager.Instance.keysCollected == 3)
            {
                StartCoroutine(GameOver());
                // ekranda son bir yazı görünüp ekran kararacak.
                // SON yazacak.
            }
        }
    }
    IEnumerator GameOver(){
        unlockSound.Play();
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("Each drawing... its own universe, waiting to be entered... huh...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1.5f);
        typeWriterEffect.StartTyping("But whatever, I can finally get out of here...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1.5f);
        dialoguePanel.SetActive(false);

        yield return StartCoroutine(FadeToBlack());
        finalText.gameObject.SetActive(true);
        Time.timeScale = 0;


    }

IEnumerator FadeToBlack()
{
    float elapsed = 0f;
    Color color = fadeOutPanel.color;
    color.a = 0f;
    fadeOutPanel.color = color;

    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime;
        color.a = Mathf.Clamp01(elapsed / fadeDuration);
        fadeOutPanel.color = color;
        yield return null;
    }
}

    IEnumerator HandleDoorDialogue()
    {
        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("Is it locked? I need to find the key...");
        
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1);
        dialoguePanel.SetActive(false);


        yield return new WaitForSeconds(9f);
        clockChimeAudio.Play();

        GameManager.Instance.currentState = GameState.ClockChimed;

        

        yield return new WaitUntil(() => saatCheck.isPlayerCheckedClock);
            print("Devam Ediyor");

            foreach (var item in paintingAudios)
            {
                if (item != null)
                {
                    item.Play();
                }
            }
            print("Devam Ediyor2");
        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("Are these sounds... coming from the paintings?");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("I should take a closer look at the paintings...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);

        GameManager.Instance.currentState = GameState.PaintingsAwakened;

    }

    private void Update()
    {
        if (saatCheck.isPlayerCheckedClock)
        {
            clockChimeAudio.volume -= Time.deltaTime;
        }
    }



}
