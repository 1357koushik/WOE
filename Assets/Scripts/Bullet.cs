using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie")) // Check if we hit a zombie
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(); // Reduce zombie health
            }
        }
        // Destroy the bullet when it collides with anything
        Destroy(gameObject);
    }
}
