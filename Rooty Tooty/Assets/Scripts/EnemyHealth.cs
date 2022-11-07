using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int life;
    public int maxLife = 3;
    public Animator enemyAnimator;

    void Start()
    {
        life = maxLife;
    }

    public void TakeDamage(int damage)
    {
        enemyAnimator.Play("export_hit");
        life -= damage;
        if (life <= 0)
        {
            enemyAnimator.Play("export_death");
            GetComponent<OgreMovement>().enabled = false;
            Destroy(gameObject);
        }
    }
}