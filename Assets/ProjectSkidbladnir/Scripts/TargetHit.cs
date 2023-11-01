using UnityEngine;

public class TargetHit : MonoBehaviour
{

    [SerializeField] int point = 1;

    [SerializeField] ScoreCollector collector;

    void Start()
    {
        if (collector == null)
        {
            GameObject ship = GameObject.Find("Spaceship");
            collector = ship.GetComponent<ScoreCollector>();
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        LaserBullet bullet = collision.gameObject.GetComponent<LaserBullet>();

        if (bullet != null)
        {
            collector.IncreaseScore(point);
        }
    }
}
