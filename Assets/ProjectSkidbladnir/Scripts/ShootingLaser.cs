using UnityEngine;

public class ShootingLaser : MonoBehaviour
{
    [SerializeField] GameObject laserBeamPrototype;
    [SerializeField] KeyCode button1, button2;

    void Update()
    {
        if (Input.GetKeyDown(button1) || Input.GetKeyDown(button2))
        {
            Instantiate(laserBeamPrototype, transform.position, transform.rotation);
        }
        //if (coolDown > 0)
        //    coolDown -= Time.deltaTime;
    }
}
