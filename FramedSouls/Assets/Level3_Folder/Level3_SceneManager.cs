using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_SceneManager : MonoBehaviour
{
    public bool gameStarted = false;
    public GameObject dialoguePanel;
    public TypewriterEffect typeWriterEffect;

    void Start()
    {
        if (GameManager.Instance.level3_first_time)
        {
            StartCoroutine(ShowMemorySequence());
        }
        else
        {
            gameStarted = true;
            dialoguePanel.SetActive(false);
        }
    }

    IEnumerator ShowMemorySequence(){
        yield return new WaitForSeconds(1f);

        dialoguePanel.SetActive(true);
        typeWriterEffect.StartTyping("There was a chest at the very end. ");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("If I can reach it without getting caught by the golem, I might be able to leave the gallery...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);
        gameStarted = true;
    }


}