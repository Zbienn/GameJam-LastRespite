using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider lifeSlider;

    [Header("Vida")]
    [SerializeField] private float maxLife = 100f;
    private float currentLife;

    [Header("Dano por contacto")]
    [SerializeField] private float contactDamage = 10f;

    void Start()
    {
        currentLife = maxLife;
        if (lifeSlider != null)
        {
            lifeSlider.maxValue = maxLife;
            lifeSlider.value = currentLife;
        }
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Clamp(currentLife, 0f, maxLife);

        if (lifeSlider != null)
        {
            lifeSlider.value = currentLife;
        }

        if (currentLife <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("O jogador morreu!");
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
}
