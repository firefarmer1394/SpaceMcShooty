using UnityEngine;

public class Targeting : MonoBehaviour
{
    //[SerializeField] GameObject levelWall;
    [SerializeField, Range(0.1f, 5f)] float scaleMod = 1f;
    //[SerializeField] LayerMask raycastMask;
    //[SerializeField] float cursorDistance = 10f;
    //[SerializeField] float scaleRatio = 0.05f;

    Vector3 lastHit;
    Vector3 ogScale;
    Camera cam;

    float originalDist;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        originalDist = cam.farClipPlane; //(levelWall.transform.position - Camera.main.transform.position).magnitude;
        //float scaleComp = cursorDistance * scaleRatio;
        ogScale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Collider coll = levelWall.GetComponent<Collider>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit);
        transform.rotation = Quaternion.LookRotation(ray.direction);
        transform.position = hit.point;


        float actDist = (Camera.main.transform.position - transform.position).magnitude;

        float ratio = actDist / originalDist;

        transform.localScale = ogScale*ratio*scaleMod;
        

        //RaycastHit hit;

        
        //if (coll.Raycast(ray, out hit, 100f))
        //    transform.position = ray.GetPoint(cursorDistance);
       
    }
}
