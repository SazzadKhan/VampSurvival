using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	public float moveSpeed = 3f;
    public int health = 100;
    public int damage = 10;
    public float attackCooldown = 1f;
    public float experienceValue = 1f;

    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Rotate to face the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        // Check if it's time to attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Check if player is in attack range (you may want to adjust this)
            if (Vector3.Distance(transform.position, player.position) < 1.5f)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;
        // Implement player damage here
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement experience drop here
        ExperienceManager.instance.AddExperience(experienceValue);
        Destroy(gameObject);
    }
}
