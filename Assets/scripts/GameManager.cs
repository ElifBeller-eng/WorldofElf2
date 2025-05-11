using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [HideInInspector]
    public int selectedCharacterIndex = 0; // 0 par défaut

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       LoadCharacterSelection();
    }



// Ajoutez ces méthodes au GameManager
public void SaveCharacterSelection()
{
    PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
}

public void LoadCharacterSelection()
{
    selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
}

// Appelez LoadCharacterSelection() dans Awake() et SaveCharacterSelection() quand on change de personnage
}