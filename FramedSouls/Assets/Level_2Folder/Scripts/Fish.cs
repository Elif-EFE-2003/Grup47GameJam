using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public int healAmount = 5; // Verilecek can miktarı
    public GameObject healEffect; // İyileşme efekti varsa
    public PlayerHealth playerHealth;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                print("Okay");
                playerHealth.Heal(healAmount); // 5 can ver

                if (healEffect != null)
                {
                    Instantiate(healEffect, transform.position, transform.rotation); // efekt göster
                }

                Destroy(gameObject); // kendini yok et (balık kaybolur)
            }
        }
    }
}
