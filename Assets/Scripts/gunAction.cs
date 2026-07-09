using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class gunAction : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    public int bulletSpeed = 50; // Higher speed since we use Raycast
    public float flashTime = 2f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletHoleEffect;
    [SerializeField] private GameObject flashfx;
    [SerializeField] private GameObject bloodHoleEffect;
    public int holeDelay = 5;

    void Start()
    {
        flashfx.SetActive(false);
        inputs = transform.root.GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (inputs.shoot)
        {
            Shoot();
            inputs.shoot = false;
        }
    }

    void Shoot()
    {
        // 🔴 Ensure correct bullet rotation (fix vertical bullet issue)
        // 🔴 Get the correct forward direction
        Quaternion bulletRotation = Quaternion.LookRotation(bulletPoint.transform.forward);

        // 🔴 Rotate the bullet by 90 degrees along the X-axis
        bulletRotation *= Quaternion.Euler(90, 0, 0);

        // 🔴 Spawn the bullet with the corrected rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletRotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        // 🔴 Use Raycast to place bullet hole accurately
        RaycastHit hit;
        if (Physics.Raycast(bulletPoint.transform.position, bulletPoint.transform.forward, out hit, 100f))
        {
            Debug.DrawRay(hit.point, hit.normal * 2, Color.green, 5f); // Debugging

            // Correct bullet hole rotation
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);

            if (hit.collider.gameObject.CompareTag("Zombie"))
            {
                // Instantiate bullet hole at impact point
                GameObject impact = Instantiate(bloodHoleEffect, hit.point, rot);
                flashfx.SetActive(true);
                Invoke("DelayedFunction", flashTime);

                // Move slightly forward to prevent clipping
                impact.transform.position += hit.normal * 0.002f;

                // Attach bullet hole to the hit object
                impact.transform.SetParent(hit.collider.transform, true);
                Zombie zombie = hit.collider.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage();
                }

                // Destroy bullet hole after 3 seconds
                Destroy(impact, holeDelay);
            }
            else
            {
                // Instantiate bullet hole at impact point
                GameObject impact = Instantiate(bulletHoleEffect, hit.point, rot);
                flashfx.SetActive(true);
                Invoke("DelayedFunction", flashTime);


                // Move slightly forward to prevent clipping
                impact.transform.position += hit.normal * 0.002f;

                // Attach bullet hole to the hit object
                impact.transform.SetParent(hit.collider.transform, true);

                // Destroy bullet hole after 3 seconds
                Destroy(impact, holeDelay);
            }
        }
    }
    public void DelayedFunction()
    {
        flashfx.SetActive(false);
    }
}