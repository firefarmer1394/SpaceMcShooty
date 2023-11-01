using UnityEngine;

public class PowerUpMover : MonoBehaviour
{
    [SerializeField] float velocity = 1;
    [SerializeField] float rotSpeed;
    [SerializeField] float sizeOfCollectable = 1;
    //[SerializeField] Bounds startLocation;
    //[SerializeField] Bounds destination;
    Camera cam;
    float aspectRatio;
    float lifetime;
    float maxLifetime;
    Vector3 move;
    Rigidbody body;
    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        aspectRatio = cam.aspect;

        float nearRandY = cam.nearClipPlane * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2);
        float nearRandX = aspectRatio * nearRandY;
        nearRandY = Random.Range(-nearRandY, nearRandY);
        nearRandX = Random.Range(-nearRandX, nearRandX);
        Vector3 farClipCenter = cam.transform.position + cam.transform.forward * cam.farClipPlane;
        Vector3 startPos = farClipCenter + nearRandY * cam.transform.up + nearRandX * cam.transform.right;

        if (body == null)
            body = GetComponent<Rigidbody>();

        transform.localScale *= sizeOfCollectable;

        lifetime = 0;

        maxLifetime = 60 / velocity;  //az indulási pozíció per a sebesség ad egy jó közelítést hogy kb meddig kell élnie az aszteroidának

        /*float x = Random.Range(startLocation.min.x, startLocation.max.x);
        //float y = Random.Range(startLocation.min.y, startLocation.max.y);
        //float z = Random.Range(startLocation.min.z, startLocation.max.z);
        //Vector3 newPos = new(x, y, z);*/
        transform.position = startPos;

        move = new(0, 0, -velocity);
    }
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(startLocation.center, startLocation.size);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(destination.center, destination.size);
    }*/
    void Update()
    {
        transform.position += move*Time.deltaTime;
        transform.Rotate(rotSpeed * Time.deltaTime, rotSpeed * Time.deltaTime, 0);
        lifetime += Time.deltaTime;
        if (lifetime >= maxLifetime)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ShieldManagement collector = other.GetComponent<ShieldManagement>();
        if (collector != null)
        {
            collector.ActivateShield();
            Destroy(gameObject);
        }
    }




}
