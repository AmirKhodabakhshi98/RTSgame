using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
       
    }

   
    private void LevelOver(bool won)
    {
        this.won = won;
        Time.timeScale = 0f;
        score = ScoreManager.instance.getScore();
        totalScore = ScoreManager.instance.getTotalScore();

        string titleText;
        string buttonText;
        string debriefText;
        
        
        if (won)
        {
            
            titleText = "Evacuation Complete";
            buttonText = "NEXT LEVEL";
            debriefText = score +  " / " + totalScore + " civilians saved." + "\n" + 
                          playerUnitsLive + " / " + playerUnitsTotal + " units survived.";
        }
        else
        {
            titleText = "Mission Failed";
            buttonText = "RESTART LEVEL";
            debriefText = "No one was rescued. \n All units lost.";
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
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        return score >= ScoreManager.instance.evacScore;
    }

    public void Evacuated()
    {
        
        LevelOver(true);
    }
    
}
