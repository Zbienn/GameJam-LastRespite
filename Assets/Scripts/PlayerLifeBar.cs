using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerLifeBar : MonoBehaviour
{
    private Animator animator;

    [Header("UI")]
    [SerializeField] private Slider lifeSlider;

    [Header("Vida")]
    [SerializeField] private float maxLife = 100f;
    private float currentLife;

    [Header("Dano por contacto")]
    [SerializeField] private float contactDamage = 10f;

    [Header("Death Section")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Transform respawnPoint;

    [Header("Campfire Section")]
    [SerializeField]
    private GameObject _campfire;
    [SerializeField]
    private TextMeshProUGUI _campfireText;

    [Header("Coin Section")]
    [SerializeField]
    private GameObject _coins;
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    [Header("Audio Section")]
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentLife = maxLife;
        if (lifeSlider != null)
        {
            lifeSlider.maxValue = maxLife;
            lifeSlider.value = currentLife;
        }
        if (deathPanel != null)
            deathPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (animator != null)
        {
            animator.SetBool("isHurt", true);
        }

        currentLife -= damage;
        currentLife = Mathf.Clamp(currentLife, 0f, maxLife);
        UpdateHealthUI();

        if (currentLife <= 0)
        {
            StartCoroutine(ResetHurtAnimation());
            StartCoroutine(HandleDeath());
        } else StartCoroutine(ResetHurtAnimation());
    }

    void UpdateHealthUI()
    {
        if (lifeSlider != null)
        {
            lifeSlider.value = currentLife;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(contactDamage);
            Destroy(collision.gameObject);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Projectile": TakeDamage(contactDamage); break;
            case "Respawn": TakeDamage(maxLife); break;
        }
    }

    private IEnumerator HandleDeath()
    {

        if (deathPanel != null)
            deathPanel.SetActive(true);

        GetComponent<PlayerMovement>().enabled = false;

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;

        if (deathPanel != null) deathPanel.SetActive(false);

        currentLife = maxLife;
        UpdateHealthUI();
        if (_campfire.activeSelf)
        {
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            Vector3 result = _campfire.transform.position + offset;
            transform.position = result;
        }
        else
        {
            _campfire.SetActive(false);
            _campfireText.text = "1";
            transform.position = respawnPoint.position;
        }

        GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        if (animator != null) animator.SetBool("isHurt", false);
    }
}
