using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class UIController : MonoBehaviour {

    public int playerHealth;
    public Text healthText;
    public Text timerText;
    public Text directionText;
    private EnemySpawner enemy;
    public Text scoreText;
    public Text powerupText;
    public RectTransform gameOver;
    public GameObject player;
    public InputController input;
    public Text finalScore;


    //Scoring 

    public int score;

    

    void Start ()
    {
        Time.timeScale = 1;
        HideGameOver();
        Cursor.visible = false;
        gameOver.sizeDelta = new Vector2(Screen.width, Screen.height);
        enemy = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        score = 0;
    }

    void ShowGameOver()
    {
        finalScore.text = "Final Score: " + score;
        gameOver.anchoredPosition = new Vector2(0.0f, 0.0f);

    }

    void HideGameOver()
    {
        gameOver.anchoredPosition = new Vector2(0, -1200);
    }
	
    
	
	void Update ()
    {
        UpdateTimerText();

        if (playerHealth <= 0)
        {
            Die();
        }
	}

    void Die()
    {
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        input.GetComponent<InputController>().enabled = false;
        
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        ShowGameOver();
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(0);
    }

    

    

    public void Quitgame()
    {
        Application.Quit();
    }

    public void UpdateScore()
    {

        score += 1;
        UpdatePlayerScore(score);

    }

    void UpdateTimerText()
    {
        
            timerText.text = "Wave spawns in: " + enemy.timer.ToString("F2");
        
        

    }

    public void UpdatePlayerHealth(int newHealth)
    {
        healthText.text = ("Health: " + newHealth);
        if (newHealth <= 0)
        {
            Time.timeScale = 0;
            //deathScreen enabled
        }
    }

    public void UpdatePlayerScore(int newScore)
    {
        scoreText.text = ("Score: " + newScore);
    }

    public void UpdatePowerupText(string newText)
    {
        powerupText.text = newText;
    }

    public void UpdateDirectionText(int directionsCollected)
    {
        if (directionsCollected > 6)
        {
            directionText.text = ("Directions: " + directionsCollected + "6/6");
        }

        else
        {
            directionText.text = ("Directions: " + directionsCollected + "/6");
        }
        
    }
}
