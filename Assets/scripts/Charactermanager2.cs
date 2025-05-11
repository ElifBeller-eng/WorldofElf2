using UnityEngine;
using UnityEngine.UI;

public class Charactermanager2 : MonoBehaviour
{
   public CharacterDatabase characterDB;

   public Text nametext;
   public SpriteRenderer artworkSprite;

   private int selectedOption = 0;

    void Start()
    {
        UpdateCharacter(selectedOption);

    }
    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        nametext.text = character.name;
        artworkSprite.sprite = character.characterSprite;
    }
}
