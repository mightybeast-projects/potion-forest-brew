using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int pointsOnMonsterDeath = 10;
    [SerializeField] private Text gameScore;
    [SerializeField] private Text deathScreenScore;

    private int _currentScore;

    public void IncreaseScore()
    {
        _currentScore += pointsOnMonsterDeath;
        UpdateScore();
    }

    private void UpdateScore()
    {
        gameScore.text = _currentScore.ToString();
        deathScreenScore.text = _currentScore.ToString();
    }
}