using UnityEngine;

public class ShootingLaser : MonoBehaviour
{
    [SerializeField] GameObject laserBeamPrototype;
    [SerializeField] KeyCode button1, button2;


    [SerializeField] float fireRate;
    [SerializeField] float coolDown;
    [SerializeField] float overheatRate;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(button1) || Input.GetKeyDown(button2))
        {
            GameObject newGameObject = Instantiate(laserBeamPrototype, transform.position, transform.rotation);
        }
        //if (coolDown > 0)
        //    coolDown -= Time.deltaTime;

    }
}
