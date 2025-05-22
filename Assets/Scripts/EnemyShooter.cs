using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 2f;

    private float cooldownTimer;
    private Transform player;
    private EnemyAnimator enemyAnimator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<EnemyAnimator>();
        cooldownTimer = 0f; // pronto a atacar logo
    }

    void Update()
    {
        if (player == null) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            enemyAnimator.PlayAttack(); // sincronizado!
            cooldownTimer = Mathf.Infinity; // espera pelo evento
        }
    }

    // Este método deve ser chamado no EVENTO da animação
    public void ShootProjectile()
    {
        if (player == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(direction);

        cooldownTimer = shootCooldown; // só reinicia aqui = sincronização perfeita
    }
}
