using UnityEngine;

public class ShootingLaser : MonoBehaviour
{
    [SerializeField] GameObject laserBeamPrototype;
    [SerializeField] KeyCode customFireButton;
    [SerializeField, Range(0f, 15f)] float fireRate = 8f;
    float nextTimeToFire = 0f;

    void Update()
    {
        bool triggerPulled = Input.GetKey(customFireButton) || Input.GetButton("Fire1");

        if (triggerPulled && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Instantiate(laserBeamPrototype, transform.position, transform.rotation);
        }
    }
}
