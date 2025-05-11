using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;
    public AudioClip[] musicClip; //Tablo oluşturma
    public AudioSource audioSource;
    private int currentTrackIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }
    void PlayNextTrack()
    {
        if (musicClip.Length > 0) return;
        {
            audioSource.PlayOneShot(musicClip[currentTrackIndex]);
            audioSource.Play();
            currentTrackIndex = (currentTrackIndex + 1) % musicClip.Length;
            
        }
    }
}
