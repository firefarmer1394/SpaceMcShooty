using UnityEngine;

public class DamageDealing : MonoBehaviour
{

    [SerializeField] int damage = 1;

    [SerializeField] GameObject shield;

    [SerializeField] GameObject sparks;
    ParticleSystem particleSys;
    
    
    void Start()
    {       
        
        if (sparks == null)
        {
            sparks = GameObject.Find("Sparks");
        }

        if (particleSys == null)
        {
            particleSys = sparks.GetComponent<ParticleSystem>();
        }

        if (shield == null)
        {
            shield = GameObject.Find("Shield");
        }
    }


        void OnCollisionEnter(Collision collision)
    {
                
        Damaged damageable = collision.gameObject.GetComponent<Damaged>();        

        if (damageable != null && shield.GetComponentInChildren<Collider>().enabled == false)
        {
            damageable.DealDamage(damage);

            sparks.transform.position = collision.contacts[0].point;

            Destroy(gameObject); //destroys the asteroid that hit the ship

            particleSys.Play();

            //teleport.TeleportToStart();

        }
    }
}
