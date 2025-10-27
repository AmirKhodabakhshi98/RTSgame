using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class LevelEnd : MonoBehaviour
{
    
    
    public static LevelEnd instance;
    private int playerUnitsLive = 0;
    private int playerUnitsTotal = 0;
    private int totalScore = 0;
    private int score = 0;
    public GameObject missionOver;
    private bool won;
    public TextMeshProUGUI title;
    public TextMeshProUGUI button;
    public TextMeshProUGUI debrief;
    public Image bronze;
    public Image silver;
    public Image gold;
    
    private float startTime;
    private float elapsedTime;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        startTime = Time.time;
    }

   
    private void LevelOver(bool won)
    {
        
        this.won = won;
        Time.timeScale = 0f;
        elapsedTime = Time.time - startTime;
        score = ScoreManager.instance.getScore();
        totalScore = ScoreManager.instance.getTotalScore();
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        string time = "Time: ";

        time += $"{minutes:00}:{seconds:00}";
  
        
        
        string titleText;
        string buttonText;
        string debriefText = time + "\n";//elapsedTime.ToString("F1") + "\n";
        int wonLevel = 0;
        
        if (won)
        {
            if (score == totalScore)
            {
                wonLevel++;
                if (playerUnitsLive == playerUnitsTotal)
                {
                    wonLevel++;
                }
            }
            
            titleText = "Evacuation Complete";
            buttonText = "NEXT LEVEL";
            debriefText += score + " / " + totalScore + " civilians saved." + "\n" +
                          playerUnitsLive + " / " + playerUnitsTotal + " units survived.";
            
            /*
            if (wonLevel == 0)
            {
                debriefText += "\n At least some survived.";
            }else if (wonLevel == 1)
            {
                debriefText += "\n Sacrifices had to be made.";
            }
            else
            {
                debriefText += "\n Perfect!";
            }
            */

            
            bronze.color = Color.white;
            if (wonLevel >= 1)
            {
                silver.color = Color.white;
                if (wonLevel >= 2)
                {
                    gold.color = Color.white;
                }
            }

            if (SceneManager.GetActiveScene().buildIndex ==
                SceneManager.sceneCountInBuildSettings - 1) //case final level
            {
                titleText = "All evaluations complete";
                buttonText = "FINISH GAME";
            }
            
                          
        }
        else
        {
            titleText = "Mission Failed";
            buttonText = "RESTART LEVEL";
            debriefText += "No one was rescued. \n All units lost.";
        }
        
        
        title.text = titleText;
        button.text = buttonText;
        debrief.text = debriefText;
        
        missionOver.SetActive(true);
    }


    public void LevelOverButton()
    {
        Time.timeScale = 1f;
        if (won)
        {
            if (SceneManager.GetActiveScene().buildIndex ==
                SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(0);
            }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    


    public void Register()
    {
        playerUnitsLive++;
        playerUnitsTotal++;
    }

    public void Unregister()
    {
        playerUnitsLive--;
        if (playerUnitsLive <= 0)
        {
            LevelOver(false);
        }
    }

    public bool canEvac()
    {
        return ScoreManager.instance.getScore() >= ScoreManager.instance.evacScore;
    }

    public void Evacuated()
    {
        LevelOver(true);
    }
    
}
