using UnityEngine;
using UnityEngine.SceneManagement;

public class Button
{
    internal object onClick;

    public void RestartGame() {
    		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    	}
}
