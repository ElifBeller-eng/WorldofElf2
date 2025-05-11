using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public GameObject characterPrefab;
        public Sprite characterIcon;
        public string characterName;
    }

    [Header("Personnages disponibles")]
    public CharacterData[] characters;

    [Header("Références")]
    [SerializeField] private Transform spawnPoint;
    
    private GameObject currentCharacter;

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        SpawnCharacter(selectedIndex);
    }

    public void SpawnCharacter(int characterIndex)
    {
        // Détruit l'ancien personnage s'il existe
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // Crée le nouveau personnage
        if (characterIndex >= 0 && characterIndex < characters.Length)
        {
            currentCharacter = Instantiate(
                characters[characterIndex].characterPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            
            Debug.Log($"Personnage {characters[characterIndex].characterName} chargé");
        }
    }
}