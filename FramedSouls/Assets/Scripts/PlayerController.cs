using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float timeLeft = 2f;
    private float speedMod = 0f;
    public bool rushing = false;

    private Rigidbody2D myRigidBody;
    private Animator myAnim;

    public GameObject explosion;
    public GameObject bubbles;

    private PlayerHealth playerHealth;

    private bool isDead = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!isDead)
        {
            controllerManager();
            resetBoostTime();
            myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.linearVelocity.x));
        }
    }

    void controllerManager()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float moveX = -horizontalInput;
        float moveY = -verticalInput;

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1f, 1f);
            myRigidBody.linearVelocity = new Vector2(moveX * (moveSpeed + speedMod), myRigidBody.linearVelocity.y);
        }

        else if (verticalInput != 0)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, moveY * moveSpeed);
        }


        if (Input.GetButtonDown("Jump") && !rushing)
        {
            rushing = true;
            speedMod = 2;
            Instantiate(bubbles, transform.position, transform.rotation);
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x * 2f, myRigidBody.linearVelocity.y);
        }
    }

    public void Hurt(int damageAmount)
    {
        if (!isDead && playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
			gameObject.GetComponent<Animator> ().Play ("PlayerHurt");		
			
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died");

        // Ölüm animasyonu tetikle
        myAnim.SetTrigger("Die");

        // Patlama oluştur
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);

        // Oyuncunun hareketini durdur ve collider'ı kapat
        myRigidBody.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        // Görünmez yap (isteğe bağlı)
        GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(ReloadSceneAfterDelay(3f));
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void resetBoostTime()
    {
        if (rushing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                rushing = false;
                speedMod = 0f;
                timeLeft = 2f;
            }
        }
    }
}
