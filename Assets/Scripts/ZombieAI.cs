using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private float lastAttackTime;
    public float attackCooldown = 3f;
    public float attackRange = 1.5f;
    public float attackDamage = 10f; // Damage per attack

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("My");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>(); // Get PlayerHealth script
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Move towards player
            agent.isStopped = false;
            agent.SetDestination(player.position);

            if (agent.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isCrawling", true);
                animator.ResetTrigger("attack");
            }
        }
        else
        {
            // Attack player
            agent.isStopped = true;
            animator.SetBool("isCrawling", false);

            if (Time.time > lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("attack");
                lastAttackTime = Time.time;
            }
        }
    }

    // 🔥 This function will be triggered by the Animation Event at the right moment!
    public void TriggerAttackImpact()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}