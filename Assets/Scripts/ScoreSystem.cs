using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _scoreMultiplier = 10;

    public Car car;

    private float _score;

    public const string HighScoreKey = "HighScore";

    private void Start() 
    {
        GameObject carContainer = GameObject.Find("CarContainer");
        car = carContainer.GetComponent<Car>();
    }
   
    void Update()
    {
        if(!car.GetIsMoving())
        {
            return;
        }
        _score += Time.deltaTime * _scoreMultiplier;
        _scoreText.text = $"Score: {Mathf.FloorToInt(_score).ToString()}";
    }

    private void FixedUpdate() 
    {
        updateHighScore();
    }

    private void updateHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (_score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(_score));
        }
    }

    public void AddBonusPoints()
    {
        _score += 100;
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
