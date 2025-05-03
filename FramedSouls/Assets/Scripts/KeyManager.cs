using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    public GameObject keyObject; // Key objesini buraya sürükle
    private int collectedCount = 0;
    private int totalRequired = 3;

    void Start()
    {
        keyObject.SetActive(false); // Key başta görünmesin
    }

    public void CollectShell()
    {
        collectedCount++;

        if (collectedCount >= totalRequired)
        {
            keyObject.SetActive(true); // Key'i görünür yap
        }
    }

    public void CollectKey()
    {
        // Mevcut key sayısını al, 1 ekle ve kaydet
        int currentKeys = PlayerPrefs.GetInt("TotalKeys", 0);
        PlayerPrefs.SetInt("TotalKeys", currentKeys + 1);
        PlayerPrefs.Save();

        // Sahneyi sıfırla
        SceneManager.LoadScene(0);
    }
}
