using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;       

public class MainMenu : MonoBehaviour
{
      public void PlayGame()
      {
            SceneManager.LoadScene(1);
      }

      public void ChooseCharacterMenu()
      {

            SceneManager.LoadScene(2);


      }
}
