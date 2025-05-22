using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
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
        }
    }

    //MUDAR
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(contactDamage);
        }
    }

    private IEnumerator HandleDeath()
    {

        if (deathPanel != null)
            deathPanel.SetActive(true);

        GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(3f);

        if (deathPanel != null) deathPanel.SetActive(false);

        currentLife = maxLife;
        UpdateHealthUI();
        transform.position = respawnPoint.position;

        GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        if (animator != null) animator.SetBool("isHurt", false);
    }
}
