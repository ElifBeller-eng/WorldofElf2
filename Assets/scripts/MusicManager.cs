using UnityEngine;

public class MusicManager2 : MonoBehaviour
{

    public AudioSource musicSource; // Reference to the AudioSource component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Awake()
   {


       DontDestroyOnLoad(gameObject); // This will make sure the music manager persists across scenes
   }
   

}
