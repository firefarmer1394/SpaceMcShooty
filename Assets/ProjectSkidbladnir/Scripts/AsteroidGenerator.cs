using UnityEngine;

class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrototype;

    [SerializeField, Range(1, 10)] int numberOfAsteroidsAtStart = 2;

    [SerializeField, Range(0, 10)] int numberAtOnceMin = 1;
    [SerializeField, Range(0, 10)] int numberAtOnceMax = 3;
    int AsteroidsAtOnce;
    

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
            AsteroidsAtOnce = Random.Range(numberAtOnceMin, numberAtOnceMax);
            for (int i = 1; i <= AsteroidsAtOnce; i++)
            {
                GameObject newGameObject = Instantiate(asteroidPrototype);
            }
            timeLeft = secondsTillNewWave;
        }
    }
}
