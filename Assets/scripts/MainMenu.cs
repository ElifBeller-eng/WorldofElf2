using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single); // bu default ama açıkça belirt istersen
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
