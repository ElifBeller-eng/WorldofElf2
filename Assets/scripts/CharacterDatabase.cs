using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;


[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public Character[] characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int CharacterCount
    {
        
        get
        {
            return characters.Length;
        }


    }

    // Update is called once per frame
    public Character GetCharacter(int index)
    {
        


        return characters[index];
    }
}
