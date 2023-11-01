using UnityEngine;
using System;

class ClickExplosion : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float maxExplosionForce;
    [SerializeField] ParticleSystem particleSys;

    [SerializeField] LayerMask raycastMask;

    Rigidbody[] rigidbodies;
    Vector3 lastHit;

    void Start()
    {
        rigidbodies = FindObjectsOfType<Rigidbody>();
    }
    void Update()
    {
        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        bool isHit = Physics.Raycast(ray, out RaycastHit hit, 100, raycastMask);

        if (isHit)
        {
            //Debug.Log($"HIT: {hit.collider.name}, {hit.point}");
            lastHit = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                Explosion(hit.point);
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log(hit.collider.gameObject.layer);
            }
        }

        void Explosion(Vector3 point)
        {
            transform.position = point;
            particleSys.Play();
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                Rigidbody rb = rigidbodies[i];
                ExplodeOne(rb, point);
            }
        }

        void ExplodeOne(Rigidbody rb, Vector3 p)
        {
            Vector3 direction = rb.transform.position - p;
            float distance = direction.magnitude;
            direction.Normalize();

            if (distance >= range)
                return;

            float forceMultiplier = 1 - (distance / range);
            Vector3 force = forceMultiplier * direction * maxExplosionForce;
            rb.AddForce(force, ForceMode.Impulse);
        }


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastHit, 0.2f);
    }
}
