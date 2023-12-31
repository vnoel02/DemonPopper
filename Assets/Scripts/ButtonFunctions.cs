using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using Unity.UI;
using TMPro;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Directions");
    }


    public void PlayGame()
    {
        string s = nameInput.text;
        Debug.Log("your name is: " + s);
        //store in persistent data
        PersistentData.Instance.SetName(s);
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        PersistentData.Instance.ResetPlayerData();
        Time.timeScale= 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSettings() {
        SceneManager.LoadScene("Settings");
    }

    public void GoToHighScores() {
        SceneManager.LoadScene("HighScores");
    }
}
