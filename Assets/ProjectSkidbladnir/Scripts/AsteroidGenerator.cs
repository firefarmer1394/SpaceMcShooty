using UnityEngine;

class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrototype;

    [SerializeField, Range(1, 10)] int numberOfAsteroidsAtStart = 2;

    [SerializeField, Range(1, 10)] int AsteroidsAtLOnce = 2;

    [SerializeField, Range(1, 60)] float secondsTillNewWave = 8;

    float timeLeft;
    void Start()
    {
        for (int i = 1; i <= numberOfAsteroidsAtStart; i++)
        {
            GameObject newGameObject = Instantiate(asteroidPrototype);
        }
        timeLeft = secondsTillNewWave;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            for (int i = 1; i <= AsteroidsAtLOnce; i++)
            {
                GameObject newGameObject = Instantiate(asteroidPrototype);
            }
            timeLeft = secondsTillNewWave;
        }
    }
}
