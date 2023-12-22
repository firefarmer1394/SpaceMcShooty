using UnityEngine;
using System.Collections;

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

    Vector3 lookDir;
    Vector3 dashDir;

    Vector2 edgeOfScreen;
    [SerializeField, Range(0f, 5f)] float barrierWidth = 1f;

    [SerializeField] Vector3 moveDir;
    [SerializeField] Vector3 rotation;
    
    [SerializeField] float dashingSpeed = 3f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashTimer;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float cooldown;
    [SerializeField] TrailRenderer tr;

    [SerializeField] bool canDash = true;

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

        canDash = true;        
    }
    void Update()
    {
        rotation = transform.rotation.eulerAngles;

        //Points the ship towards crosshair
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, cam.farClipPlane+1, levelWall);
        Vector3 target = hit.point;
        lookDir = target - transform.position;
        Quaternion lookRot;
        Vector3 lookRotEuler = new Vector3(0, 0, 0);

        if (lookDir != Vector3.zero)
        {
            lookRot = Quaternion.LookRotation(lookDir);
            lookRotEuler = lookRot.eulerAngles;
        }

        //Set the boundaries of ship movement based on distance from camera
        float distanceFromCam = (transform.position - cam.transform.position).z;
        edgeOfScreen.y = distanceFromCam * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2) - barrierWidth;
        edgeOfScreen.x = aspectRatio * edgeOfScreen.y - barrierWidth;

        float y = Input.GetAxis("Vertical");   // Vertical input
        float x = Input.GetAxis("Horizontal"); //Horizontal input

        float tgtAngZ;
        if (x < 0) //left
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else if (x > 0) //right
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 360f - maxLeaningAng, eulerRotIncr * Time.deltaTime);
        }
        else //No horizontal input
        {
            tgtAngZ = Mathf.MoveTowardsAngle(rotation.z, 0f, eulerRotIncr * Time.deltaTime);
        }
        lookRotEuler.z = tgtAngZ;
        transform.localRotation = Quaternion.Euler(lookRotEuler);

        moveDir = new Vector3(x, y, 0);

        //Check of horizontal boundaries
        if (((transform.position.x >= edgeOfScreen.x) && x > 0) || ((transform.position.x <= -edgeOfScreen.x) && x < 0))
        {
            moveDir.x = 0;
        }

        //Check of vertical boundaries
        if (((transform.position.y >= edgeOfScreen.y) && y > 0) || ((transform.position.y <= (-edgeOfScreen.y)) && y < 0))
        {
            moveDir.y = 0;
        }        

        moveDir.Normalize(); //Normalize velocity for the same speed by diagonal movement

        if (cooldown <= 0)
            canDash = true;
        else
        {
            canDash = false;
            cooldown -= Time.deltaTime;
        }
                

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dashDir = moveDir;
            dashTimer = dashingTime;
            canDash = false;
            cooldown = dashingCooldown + dashingTime;
        }
        if (dashTimer > 0)
        {
            //Check of horizontal boundaries
            if ((transform.position.x >= edgeOfScreen.x) || (transform.position.x <= -edgeOfScreen.x))
            {
                dashDir.x = 0;
            }
            //Check of vertical boundaries
            if ((transform.position.y >= edgeOfScreen.y) || (transform.position.y <= -edgeOfScreen.y))
            {
                dashDir.y = 0;
            }
            dashDir.Normalize();

            float dashSpeed = Mathf.SmoothStep(speed, dashingSpeed, dashTimer/dashingTime);

            transform.position += (dashDir * dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;
            tr.emitting = true;
            return;
        }
        else
        {
            tr.emitting = false;
        }

        transform.position += (moveDir * speed * Time.deltaTime);

        /*if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }*/

    }

    /*IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector3 dashDir = velocity;
        dashDir.Normalize();
        rb.isKinematic = false;
        rb.velocity =  dashDir * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.isKinematic = true;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }*/
}
