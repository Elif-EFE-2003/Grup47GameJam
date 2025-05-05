using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{

    public float typingSpeed = 0.1f;
    public TextMeshProUGUI dialogueText;
    private Coroutine typingCoroutine;
    public bool isTypingFinished = false;
    public void StartTyping(string fullText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        isTypingFinished = false;
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingFinished = true;
    }

}
