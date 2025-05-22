using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }
}
