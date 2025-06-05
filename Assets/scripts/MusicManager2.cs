using UnityEngine;
using UnityEngine.UI;

public class MusicManager2 : MonoBehaviour
{
    public AudioSource musicSource;
    public Image iconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isMuted;

    void Start()
    {
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        isMuted = false;
        ApplyMusicState();
    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        ApplyMusicState();
    }

    private void ApplyMusicState()
    {
        if (isMuted)
            musicSource.Pause();
        else
            musicSource.Play();

        if (iconImage != null)
            iconImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
