using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelSceneManager : MonoBehaviour
{
    public bool gameStarted = false;
    public int cherriesCollected = 0;
    public GameObject dialoguePanel;
    public TypewriterEffect typeWriterEffect;

    void Start()
    {
        if (GameManager.Instance.level1_first_time)
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
        typeWriterEffect.StartTyping("This is the drawing I made when I was a child...");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        typeWriterEffect.StartTyping("If I remember right... collecting all the cherries opened the door.");
        yield return new WaitUntil(() => typeWriterEffect.isTypingFinished == true);
        yield return new WaitForSeconds(1f);
        dialoguePanel.SetActive(false);
        gameStarted = true;
    }
    public bool allCherriesCollected = false;
    void Update()
    {
        if (cherriesCollected == 10)
        {
            allCherriesCollected = true;
        }
        else
        {
            return;   
        }
    }
}
