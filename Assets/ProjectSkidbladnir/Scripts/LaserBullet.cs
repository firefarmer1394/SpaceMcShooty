using UnityEngine;

class LaserBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float duration;

    GameObject explosion;
    ParticleSystem particleSys;
    GameObject target;

    Vector3 direction;

    Camera cam;
    float lifetime;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("LaserTarget");
        }

        //EZZEL ODA NÉZ AHOVA CÉLZUNK
        direction = target.transform.position - transform.position;
        Vector3 lookRotEuler = new Vector3(0, 0, 0);
        if (direction != Vector3.zero)
        {
               Quaternion lookrot = Quaternion.LookRotation(direction);
              lookRotEuler = lookrot.eulerAngles;
            transform.rotation = lookrot;
        }
        
        if (cam == null)
            cam = Camera.main;
        if (explosion == null)
            explosion = GameObject.Find("Explosion");
        if (particleSys == null)
        {
            particleSys = explosion.GetComponent<ParticleSystem>();
        }

    }

    void Update()
    {
        transform.position += (transform.forward * speed * Time.deltaTime);

        //Destroy based on distance from camera
        float distanceFromCam = Vector3.Distance(cam.transform.position, transform.position);

        if (cam.farClipPlane < distanceFromCam)
        {
            Destroy(gameObject);
        }

        //Destruction based on lifetime
        //Time.time a játék indítása óta eltelt idõ másodpercben
        lifetime += Time.deltaTime;
        if (lifetime >= duration)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        AsteroidMover asteroid = collision.gameObject.GetComponent<AsteroidMover>();
        //Explosion e = exploder.GetComponent<Explosion>();

        if (asteroid != null)
        {
            explosion.transform.position = collision.contacts[0].point;
            particleSys.Play();

            Destroy(asteroid.gameObject);

            Destroy(gameObject);
        }
    }
}
