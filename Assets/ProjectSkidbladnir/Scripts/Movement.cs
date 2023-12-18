using UnityEngine;

class Movement : MonoBehaviour
{
    [SerializeField, Range(1, 5)] float speed = 1f;
    [SerializeField, Range(1.0f, 3f)] float speedMultiplier;
    [SerializeField, Range(5f, 40f)] float maxLeaningAng;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask levelWall;
    [SerializeField, Range(100, 360)] float eulerRotIncr = 175f;

    Camera cam;
    float aspectRatio;

    Vector3 direction;

    Vector2 edgeOfScreen;
    [SerializeField, Range(0f, 5f)] float barrierWidth = 1f;

    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 rotation;

    void Start()
    {
        Cursor.visible = false; //Kurzor elrejtese

        if (cam == null)
            cam = Camera.main;

        aspectRatio = cam.aspect;

        if (target == null)
        {
            target = GameObject.Find("LaserTarget");
        }
        
    }
    void Update()
    {
        rotation = transform.rotation.eulerAngles;

        bool maneuvering = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); //Increase movement speed

        //Points the ship towards crosshair
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, cam.farClipPlane+1, levelWall);
        Vector3 lookThere = hit.point;
        direction = lookThere - transform.position;
        Quaternion lookRot;
        Vector3 lookRotEuler = new Vector3(0, 0, 0);

        if (direction != Vector3.zero)
        {
            lookRot = Quaternion.LookRotation(direction);
            lookRotEuler = lookRot.eulerAngles;
        }

        //Set the boundaries of ship movement based on distance from camera
        float distanceFromCam = (transform.position - cam.transform.position).z;
        edgeOfScreen.y = distanceFromCam * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2) - barrierWidth;
        edgeOfScreen.x = aspectRatio * edgeOfScreen.y - barrierWidth;

        float z = Input.GetAxis("Horizontal"); //Horizontal input

        float tgtAngZ;
        if (z < 0) //left
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else if (z > 0) //right
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 360f - maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else //No horizontal input
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 0f, eulerRotIncr * Time.deltaTime);
        }
        lookRotEuler.z = tgtAngZ;
        transform.localRotation = Quaternion.Euler(lookRotEuler);

        //Check of horizontal boundaries
        if (((transform.position.x >= edgeOfScreen.x) && z > 0) || ((transform.position.x <= -edgeOfScreen.x) && z < 0))
            z = 0;

        float y = Input.GetAxis("Vertical");   // Vertical input
        //Check of vertical boundaries
        if (((transform.position.y >= edgeOfScreen.y) && y > 0) || ((transform.position.y <= (-edgeOfScreen.y)) && y < 0))
            y = 0;

        velocity = new Vector3(z, y, 0);

        velocity.Normalize(); //Normalize velocity for the same speed by diagonal movement

        if (maneuvering)
            velocity *= (speed * speedMultiplier); 
        else
            velocity *= speed;

        transform.position += (velocity * Time.deltaTime);
    }
}
