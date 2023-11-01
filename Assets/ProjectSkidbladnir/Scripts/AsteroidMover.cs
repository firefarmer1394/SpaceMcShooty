using UnityEngine;

public class AsteroidMover : MonoBehaviour
{

    [SerializeField] AnimationCurve minVelocityOverTime;  //minVelocityOverTime.Evaluate(Time.timeSinceLevelLoad)
    [SerializeField] AnimationCurve maxVelocityOverTime;
    float minVelocity = 15;
    float maxVelocity = 35;
    [SerializeField] float minAnglespeed = 35;
    [SerializeField] float maxAnglespeed = 360;

    Camera cam;
    float aspectRatio;


    //[SerializeField, Range(3f, 10f)] float maxLifetime = 5;

    [SerializeField, Range(0.01f, 1f)] float sizeMin = 0.5f;
    [SerializeField, Range(1.1f, 5f)] float sizeMax = 1.2f;
    //[SerializeField] Bounds startLocation;
    //[SerializeField] Bounds destination;
    Vector3 targetLocation;
    [SerializeField] Vector3 originalScale;

    float velocity;
    float angleSpeed;
    Vector3 direction;
    Vector3 spinner;
    float lifetime;
    float maxLifetime;

    [SerializeField] Rigidbody body;
    


    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        aspectRatio = cam.aspect;

        if (body == null)
            body = GetComponent<Rigidbody>();

        originalScale = transform.localScale;
        TeleportToStart();       

    }


    public void TeleportToStart()
    {
        minVelocity = minVelocityOverTime.Evaluate(Time.timeSinceLevelLoad);

        maxVelocity = maxVelocityOverTime.Evaluate(Time.timeSinceLevelLoad);

        lifetime = 0;

        velocity = Random.Range(minVelocity, maxVelocity);

        maxLifetime = 60 / velocity;  //az indulási pozíció per a sebesség ad egy jó közelítést hogy kb meddig kell élnie az aszteroidának

        angleSpeed = Random.Range(minAnglespeed, maxAnglespeed);

        spinner = new Vector3(angleSpeed, angleSpeed, 0f);

        transform.localScale = originalScale * Random.Range(sizeMin, sizeMax);

                
        Vector3 farClipCenter;        
        float farRandY = cam.farClipPlane * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2);        
        float farRandX = aspectRatio * farRandY;
        farRandY = Random.Range(-farRandY, farRandY);
        farRandX = Random.Range(-farRandX, farRandX);
        farClipCenter = cam.transform.position + cam.transform.forward * cam.farClipPlane;
        Vector3 farRandomLoc = farClipCenter + farRandY * cam.transform.up + farRandX * cam.transform.right;        
        transform.position = farRandomLoc;

        targetLocation = TargetPos();

        direction = (targetLocation - transform.position).normalized;

        body.velocity = (direction * velocity);
        body.angularVelocity = spinner;
    }

    public Vector3 TargetPos()
    {
        Vector3 nearClipCenter;
        float nearRandY = cam.nearClipPlane * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2);
        float nearRandX = aspectRatio * nearRandY;
        nearRandY = Random.Range(-nearRandY, nearRandY);
        nearRandX = Random.Range(-nearRandX, nearRandX);
        nearClipCenter = cam.transform.position + cam.transform.forward * cam.nearClipPlane;
        Vector3 nearRandomLoc = nearClipCenter + nearRandY * cam.transform.up + nearRandX * cam.transform.right;
        return nearRandomLoc;
    }





    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= maxLifetime)
            TeleportToStart();
    }




}
