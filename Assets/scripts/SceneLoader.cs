using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadDarkScene()  // <-- BU ŞART
    {
        SceneManager.LoadScene("DarkScene");
    }
}
