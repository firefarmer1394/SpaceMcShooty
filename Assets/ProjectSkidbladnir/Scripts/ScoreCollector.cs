using UnityEngine;
using TMPro;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] int points = 0;
    [SerializeField, Range(1, 2000)] int winnerScore;
    [SerializeField] TMP_Text score, finalScore;
    [SerializeField] float scoreFreezeTime = 0.01f;

    float freezeTime;
    void Start()
    {
        freezeTime = 0;
        UpdateText();
    }
    public void IncreaseScore(int value)
    {
        if (freezeTime > 0)
            return;
        points += value;
        freezeTime = scoreFreezeTime;
        points = Mathf.Min(points, winnerScore);
        UpdateText();
    }
    private void UpdateText()
    {
        if (score != null)
            score.text = $"Points: {points}";
        if (finalScore != null)
            finalScore.text = $"Your Score: {points}";
    }
    void Update()
    {
        if (freezeTime > 0)
            freezeTime -= Time.deltaTime;
    }
}
