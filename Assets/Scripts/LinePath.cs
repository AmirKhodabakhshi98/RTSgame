using UnityEngine;

public class LinePath : MonoBehaviour
{
    public AIPathCustom playerAIPath;
    public LineRenderer myLineRend;
    private bool selected = false;
    private Color visible;
    private Color invisible;
    
    
    void Start()
    {
        visible = new Color(0, 16*0.5f, 120*0.5f, 1);
        invisible = new Color(0, 16, 120, 0);
        selected = false;
        InvokeRepeating ("ResetLine", 0, .1f);
    }
    
    public void setSelected(bool selected)
    {
        this.selected = selected;
        if (selected)
        {
            myLineRend.material.color = visible;
        }
        else
        {
            myLineRend.material.color = invisible;
        }
    }

    public void ResetLine() {
        //print ("linePath resetLine");
        
        if (selected && playerAIPath.path.hasPath) {
            //print (playerAIPath.path.vectorPath.Count);
            myLineRend.positionCount = playerAIPath.GetPathLength();

            for (int i = 0; i < playerAIPath.GetPathLength(); i++) {
                myLineRend.SetPosition (i, playerAIPath.GetPathPosition(i));
            }

        } else {
            Debug.Log("no path", gameObject);
        }
    }
    
}
