using TMPro;
using UnityEngine;

public class KeyUIController : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public int totalKeys = 1; // Ba�lang��ta gereken anahtar say�s�

    void Start()
    {
        UpdateKeyText();
    }

    public void UpdateKeyText()
    {
        keyText.text = "Toplanmas� gereken anahtar: " + totalKeys;
    }
}

