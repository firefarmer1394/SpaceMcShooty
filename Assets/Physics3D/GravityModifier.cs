using UnityEngine;

class GravityModifier : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float liftingForce;
    Rigidbody[] rigidbodies;
    Vector3 pointer;
    [SerializeField] Transform GravField;
    Vector3 rescale = new Vector3(1f, 1f, 1f);
    [SerializeField] bool dobbanto;
    [SerializeField] bool gravity;
    [SerializeField] Vector3 bubbleScale;
    [SerializeField] float howFar;
    [SerializeField] float distance;

    // Start is called before the first frame update


    void Start()
    {
        rigidbodies = FindObjectsOfType<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        pointer = hit.point;
        GravField.position = pointer;
        GravField.localScale = rescale * range*2;
        bubbleScale = GravField.localScale;

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody == this) continue;
            Vector3 vector = rigidbody.transform.position - pointer;
            distance = vector.magnitude;
            howFar = 0;
            if (distance < range)
            {
                howFar = 1 - distance / range;

                if (dobbanto && !gravity)
                {
                    if (Input.GetMouseButtonDown(0))
                        rigidbody.AddForce(transform.up * (howFar * liftingForce), ForceMode.Impulse);
                }
                if (gravity && !dobbanto)
                {
                    if (Input.GetMouseButton(0))
                        rigidbody.AddForce(transform.up * (howFar * liftingForce), ForceMode.Acceleration);
                }
            }
        }

    }

    void FixedUpdate()
    {


    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(pointer, 0.3f);
    }
}
