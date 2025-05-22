using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private enum EnemyType { Goblin, Bat }

    [Header("Tipo de Inimigo")]
    [SerializeField] private EnemyType enemyType;

    [Header("Disparo")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 2f;

    private float cooldownTimer;
    private Transform player;
    private EnemyAnimator enemyAnimator;

    void Start()
    {
        if (enemyType == EnemyType.Goblin)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        enemyAnimator = GetComponent<EnemyAnimator>();
        cooldownTimer = 0f;
    }

    void Update()
    {
        if (enemyType == EnemyType.Goblin && player == null) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
                enemyAnimator.PlayAttack();
                cooldownTimer = Mathf.Infinity;
        }
    }

    // Chamado no evento da animação
    public void ShootProjectile()
    {
        Vector2 direction;

        if (enemyType == EnemyType.Goblin && player != null)
        {
            direction = (player.position - firePoint.position).normalized;
        }
        else if (enemyType == EnemyType.Bat)
        {
            direction = Vector2.down;
        }
        else return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(direction);

        cooldownTimer = shootCooldown;
    }
}
