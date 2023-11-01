using UnityEngine;

public class CollectableGenerator : MonoBehaviour
{
    [SerializeField] GameObject collectablePrototype;

    [SerializeField, Range(5, 60)] float minTimeToSpawn = 10;
    [SerializeField, Range(5, 60)] float maxTimeToSpawn = 20;

    float timeLeft;
    void Start()
    {
        timeLeft = Random.Range(minTimeToSpawn,maxTimeToSpawn);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject newGameObject = Instantiate(collectablePrototype);
            timeLeft = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        }
    }
}
