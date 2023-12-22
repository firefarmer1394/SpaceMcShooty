using UnityEngine;

public class DamageDealing : MonoBehaviour
{

    [SerializeField] int damage = 1;

    [SerializeField] GameObject shield;

    [SerializeField] GameObject sparks;
    [SerializeField] GameObject explosion;
    ParticleSystem dmgToShip;
    ParticleSystem dmgToAsteroid;
    
    
    void Start()
    {       
        
        if (sparks == null)
            sparks = GameObject.Find("Sparks");
        if (dmgToShip == null)
            dmgToShip = sparks.GetComponent<ParticleSystem>();

        if (explosion == null)
            explosion = GameObject.Find("Explosion");
        if (dmgToAsteroid == null)
            dmgToAsteroid = explosion.GetComponent<ParticleSystem>();

        if (shield == null)
            shield = GameObject.Find("Shield");
    }

        void OnCollisionEnter(Collision collision)
    {                
        Damaged damageable = collision.gameObject.GetComponent<Damaged>();        

        if (damageable != null && shield.GetComponentInChildren<Collider>().enabled == false)
        {
            damageable.DealDamage(damage);
            sparks.transform.position = collision.contacts[0].point;
            Destroy(gameObject); //destroys the asteroid that hit the ship
            dmgToShip.Play();
        }
        if (collision.gameObject == shield)
        {
            explosion.transform.position = collision.contacts[0].point;
            Destroy(gameObject); //destroys the asteroid that hit the shield
            explosion.GetComponent<ParticleSystem>().Play();
        }
    }
}
