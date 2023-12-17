using UnityEngine;

class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrototype;

    //[SerializeField, Range(1, 10)] int numberOfAsteroidsAtStart = 2;

    [SerializeField, Range(0, 10)] int numberAtOnceMin = 1;
    [SerializeField, Range(0, 10)] int numberAtOnceMax = 3;
    int AsteroidsAtOnce;    

    [SerializeField, Range(1, 60)] float secTillNewWaveStart = 8;
    [SerializeField, Range(1, 60)] float secTillNewWaveMin = 1;
    [SerializeField, Range(0, 2)] float speedUpRate = 1; //secTillNewWave decreases this much second in each minute

    [SerializeField] float secTillNewWave;
    [SerializeField] float timeLeft;
    void Start()
    {
        secTillNewWave = secTillNewWaveStart;
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
            timeLeft = secTillNewWave;
        }
        secTillNewWave = Mathf.MoveTowards(secTillNewWave, secTillNewWaveMin, speedUpRate/60f*Time.deltaTime);
    }
}
