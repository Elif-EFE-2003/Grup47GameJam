using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstScene : MonoBehaviour
{    public GameObject EStartPanel;
    public GameObject EExitPanel;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "startGame")
            {
                EStartPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("gallery_scene");
                }
            }
            if (gameObject.name =="exitGame")
            {
                EExitPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Application.Quit();
                }
            }        
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "startGame")
            {
                EStartPanel.SetActive(false);
            }
            if (gameObject.name == "exitGame")
            {
                EExitPanel.SetActive(false);
            }
        }
    }

}
