using UnityEngine;

public class test : MonoBehaviour
{
    public AudioClip testClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource.PlayClipAtPoint(testClip, Camera.main.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
