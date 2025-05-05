using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player tag'ine sahip bir nesneyle çarpışma kontrolü
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player touched the lava!");

            // Oyuncunun HeroKnight script'ine eriş
            HeroKnight hero = collision.GetComponent<HeroKnight>();
            if (hero != null)
            {
                hero.Die(); // HeroKnight üzerindeki Die fonksiyonunu tetikle
            }
        }
    }
}
