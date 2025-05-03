using TMPro;
using UnityEngine;

public class KeyUIController : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public int totalKeys = 1; // Baþlangýçta gereken anahtar sayýsý

    void Start()
    {
        UpdateKeyText();
    }

    public void UpdateKeyText()
    {
        keyText.text = "Toplanmasý gereken anahtar: " + totalKeys;
    }
}

