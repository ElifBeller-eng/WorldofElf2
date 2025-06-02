using UnityEngine;

public class Player1 : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Transform spawnPoint; // ➕ Ajoutez ce champ dans l’inspecteur

    private int selectedOption = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }

        SpawnCharacter(selectedOption);
    }

    void SpawnCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);

        if (character.prefab != null)
        {
            Instantiate(character.prefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Le prefab du personnage est manquant !");
        }
    }

    void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
