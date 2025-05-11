using UnityEngine;
using UnityEngine.SceneManagement;
public class button1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame() 
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
