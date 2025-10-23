using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    
    
    public static LevelEnd instance;
    private int playerUnitsLive = 0;
    
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void Register()
    {
        playerUnitsLive++;
        Debug.Log(playerUnitsLive);
    }

    public void Unregister()
    {
        playerUnitsLive--;
        if (playerUnitsLive <= 0)
        {
            Debug.Log(playerUnitsLive);
        }
    }

    public void Evacuated()
    {
        Debug.Log("evac");
    }
    
}
