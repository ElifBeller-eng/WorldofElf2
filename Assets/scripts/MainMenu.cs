using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Sahne adını uygun şekilde yaz
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
