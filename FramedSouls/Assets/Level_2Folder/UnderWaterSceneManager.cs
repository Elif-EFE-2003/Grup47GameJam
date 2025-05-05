using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterSceneManager : MonoBehaviour
{   
    public bool gameStarted = false;
    public GameObject dialoguePanel;

    public TypewriterEffect typeWriterEffect;
    
    void Start()
    {
        if (GameManager.Instance.level2_first_time)
        {
            StartCoroutine(ShowMemorySequence());
        }
        else
        {
            gameStarted = true;
            dialoguePanel.SetActive(false);
        }
    }

    IEnumerator ShowMemorySequence()
    {
        dialoguePanel.SetActive(true);

        typeWriterEffect.StartTyping("This place... I remember it well.");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("I need to collect the seashells...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("Then I can go back.");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);

        dialoguePanel.SetActive(false);
        GameManager.Instance.level2_first_time = false; // İlk defa oynandı olarak işaretle
        gameStarted = true;

    }

}
