using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelector : MonoBehaviour
{
    [Header("Références UI")]
    [SerializeField] private UnityEngine.UI.Button character1Button;
    [SerializeField] private UnityEngine.UI.Button character2Button;
    [SerializeField] private Image character1Image;
    [SerializeField] private Image character2Image; 

    [Header("Événement de sélection")]
    public UnityEvent<int> OnCharacterSelected;

    void Start()
    {
        // Configure les boutons
        character1Button.onClick.AddListener(() => SelectCharacter(0));
        character2Button.onClick.AddListener(() => SelectCharacter(1));
        
        // Met à jour l'affichage initial
        UpdateButtonVisuals(PlayerPrefs.GetInt("SelectedCharacter", 0));
    }

    public void SetupCharacterIcons(Sprite icon1, Sprite icon2)
    {
        character1Image.sprite = icon1;
        character2Image.sprite = icon2;
    }

    private void SelectCharacter(int index)
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
        PlayerPrefs.Save();
        
        UpdateButtonVisuals(index);
        OnCharacterSelected.Invoke(index);
    }

    private void UpdateButtonVisuals(int selectedIndex)
    {
        character1Button.interactable = (selectedIndex != 0);
        character2Button.interactable = (selectedIndex != 1);
    }


}