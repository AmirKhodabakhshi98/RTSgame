using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnTutorialClick()
    {
        SceneManager.LoadScene(0);
    }
    
    public void OnExitClick()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
}
