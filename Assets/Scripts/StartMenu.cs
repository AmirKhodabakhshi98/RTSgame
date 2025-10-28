using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    int dropDownLevel = 0;
    
    public void OnStartClick()
    {
        SceneManager.LoadScene(3+dropDownLevel);
    }
    
    public void OnTutorialClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnControlsClick()
    {
        SceneManager.LoadScene(2);
    }
    
    
    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }

    
    
    public void OnLevelSelectClick(int level)
    {
        dropDownLevel = level;
    }
    
    public void OnExitClick()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
}
