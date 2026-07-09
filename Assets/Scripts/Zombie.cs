using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health = 3; // Zombie dies after 3 hits

    public void TakeDamage()
    {
        health--; // Reduce health by 1

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Destroy the zombie
    }
}
