using TMPro;
using UnityEngine;

public class KeyUIController : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public int totalCherries = 10; // Ba�lang��ta gereken anahtar say�s�

    void Start()
    {
        UpdateKeyText();
    }

    void Update()
    {
        UpdateKeyText();
        totalCherries = GameObject.FindGameObjectsWithTag("Cherry").Length;       
    }

    public void UpdateKeyText()
    {
        keyText.text = "Cherries Remaining: " + totalCherries;
    }
}

