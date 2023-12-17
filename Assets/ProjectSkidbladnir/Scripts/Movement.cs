using UnityEngine;

class Movement : MonoBehaviour
{
    [SerializeField, Range(1, 5)] float speed = 1f;
    [SerializeField, Range(1.5f, 3f)] float speedMultiplier;
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
    Vector3 euler;

    float angVel;
    float ogSpeed;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        aspectRatio = cam.aspect;

        if (target == null)
        {
            target = GameObject.Find("LaserTarget");
        }


        ogSpeed = speed;
        angVel = speed * 1f;
    }
    void Update()
    {
        rotation = transform.rotation.eulerAngles;
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
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

        float z = Input.GetAxis("Horizontal");  // Amount of horizontal movement

        /*if (right || left)
        {
            if (euler.z >= maxLeaningAng)
            {
                if (z > 0)
                    euler.z -= angVel * z;
            }
            else
            {
                if (euler.z <= -maxLeaningAng)
                {
                    if (z < 0)
                        euler.z -= angVel * z;
                }
                else
                    euler.z -= angVel * z;
            }
        }
        else
        {
            float rotIncrement = Time.deltaTime * 100;
            if (rotIncrement > Mathf.Abs(euler.z))
                euler.z -= euler.z;
            else
            {
                if (euler.z >= 0)
                    euler.z -= rotIncrement;
                else
                    euler.z += rotIncrement;
            }
        }*/
        float tgtAngZ;
        if (right)
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 360f - maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else if (left)
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else 
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 0f, eulerRotIncr * Time.deltaTime);
        }
        //Vector3 tgtAng = transform.localRotation.eulerAngles;
        lookRotEuler.z = tgtAngZ;
        transform.localRotation = Quaternion.Euler(lookRotEuler);
        /*euler.x = lookRotEuler.x;
        euler.y = lookRotEuler.y;
        Quaternion q = Quaternion.Euler(euler);
        transform.localRotation = q;*/


        if (((transform.position.x >= edgeOfScreen.x) && z > 0) || ((transform.position.x <= -edgeOfScreen.x) && z < 0))
            z = 0;

        float y = Input.GetAxis("Vertical");   // Amount of vertical movement

        if (((transform.position.y >= edgeOfScreen.y) && y > 0) || ((transform.position.y <= (-edgeOfScreen.y)) && y < 0))
            y = 0;

        velocity = new Vector3(z, y, 0);

        velocity.Normalize(); //Normalize velocity for the same speed by diagonal movement

        if (maneuvering)
            speed = ogSpeed * speedMultiplier;
        else
            speed = ogSpeed;

        velocity *= speed;  // Speed sensitivity
        velocity *= Time.deltaTime;  //Velocity is not affected by FPS
        transform.position += velocity;
    }
}
