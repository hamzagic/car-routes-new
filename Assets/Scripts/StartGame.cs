using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoretext;

    private void Start() 
    {
       int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
       _highScoretext.text = $"High Score: {highScore}";
    }
   public void StartGameAgain()
   {
       SceneManager.LoadScene(1);
   }
}
