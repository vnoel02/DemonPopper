using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    public TextMeshProUGUI scoreText;

    // public TextMestProUGUI levelText;
     public const int DEFAULT_POINTS = 1;
     [SerializeField] int level;

     [SerializeField] int newPoints = 0;

     
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            PersistentData.Instance.ResetPlayerData();
       }
        scoreText = GetComponent<TextMeshProUGUI>();
        score = PersistentData.Instance.GetScore();
        level = SceneManager.GetActiveScene().buildIndex + 1;
        DisplayScore();
        // DisplayScene();
    }

    // Update is called once per frame
    void Update()
    {

       
        
       GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Monster");

         if(objectsWithTag.Length == 0) {
            score += newPoints;
             PersistentData.Instance.SetScore(score);
            SceneManager.LoadScene(level);
         }   
    }

     public void AddPoints(int points)
    {
        newPoints += points;
       
        DisplayScore();

        // if(score >= SCORE_THRESHOLD)
        // {
        //     SceneManager.LoadScene(level); //level is currently set at 1 greater than the index
        //     //so when we advance we can use level.
        // }
    }

    public void AddPoints()
    {
        AddPoints(DEFAULT_POINTS);
    }

     private void DisplayScore()
    {
        string text = "Score:" + score + "\nLevel:" + (level-1);
        scoreText.SetText(text);
        
    }

    // private void DisplayScene()
    // {
    //     levelText.SetText("Level: " + level);
    // }

}
