using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] int playerHealth; // Oyuncunun başlangıç sağlığı
    [SerializeField] float respawnDelay = 2.0f; // Respawn gecikme süresi
    [SerializeField] private float invulnerabilityDuration = 1.0f; // Çarpışma sonrası bağışıklık süresi
    
    private bool isInvulnerable = false;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    public int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private Vector3 startPosition; // Başlangıç konumu

    public Level3_SceneManager level3SceneManager; 
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        // Başlangıç konumunu sakla
        startPosition = transform.position;
    }

    void Update()
    {
        m_timeSinceAttack += Time.deltaTime;

        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;

        }

        if (!m_rolling && level3SceneManager.gameStarted)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        if (Input.GetKeyDown("e") && !m_rolling)
        {
            //m_animator.SetBool("noBlood", m_noBlood);
            //m_animator.SetTrigger("Death");
        }
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Destroy(enemy.gameObject); // Yaratık yok edilir
                    Debug.Log("Enemy destroyed by attack!");
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth--;
            Debug.Log("Player Health: " + playerHealth);

            if (playerHealth <= 0)
            {
                Debug.Log("Player is dead!");
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
    if (collision.CompareTag("Enemy")&& !isInvulnerable)
    {
        Debug.Log("Enemy touched the player!");
        playerHealth--; // Canı azalt

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead!");
            Die(); // Oyuncuyu öldür
        }
    }
    else if (collision.CompareTag("Lava")&& !isInvulnerable) // Eğer Lav'a değerse
    {
        Debug.Log("Player fell into lava!");
        Die(); // Ölüm animasyonu ve yeniden doğma işlemi
    }
            else
        {
            StartCoroutine(BecomeTemporarilyInvulnerable());
        }
}
    }

    public void Die()
    {
        GameManager.Instance.level3_first_time = false;
        m_animator.SetTrigger("Death");
        Invoke(nameof(Respawn), respawnDelay); // Ölüm sonrası respawn gecikmesi
    }

private void Respawn()
{
    transform.position = startPosition; // Oyuncuyu başlangıç konumuna taşı
    playerHealth = 1; // Sağlığı sıfırla
    m_animator.SetTrigger("Respawn"); // Respawn animasyonu
    Debug.Log("Player respawned!");

    // Yaratıkların pozisyonlarını sıfırla
    Enemy[] enemies = FindObjectsOfType<Enemy>();
    foreach (Enemy enemy in enemies)
    {
        enemy.ResetEnemyPosition(); // Yaratıkların pozisyonunu sıfırla
    }
}

    void AE_SlideDust()
    {
        Vector3 spawnPosition = m_facingDirection == 1 ? m_wallSensorR2.transform.position : m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation);
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
    private IEnumerator BecomeTemporarilyInvulnerable()
{
    isInvulnerable = true;
    Debug.Log("Player is now invulnerable.");

    // Süreyi bekle
    yield return new WaitForSeconds(invulnerabilityDuration);

    isInvulnerable = false;
    Debug.Log("Player is no longer invulnerable.");
}

}
