using UnityEngine;
using UnityEngine.UI;

public class MusicManager2 : MonoBehaviour
{
    public AudioSource musicSource;
    public Image iconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private AudioSource music;
    private bool isMuted;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (musicSource != null)
        {
            music = musicSource;
        }
        else
        {
            music = GetComponent<AudioSource>();
        }

        if (music == null)
        {
            Debug.LogError("AudioSource bulunamadı!");
            return;
        }

        // 🔄 Kaydedilen ayarı geri yükle
        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        ApplyMusicState();
    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save(); // Kalıcı olarak kaydet
        ApplyMusicState();
    }

    private void ApplyMusicState()
    {
        if (music == null) return;

        if (isMuted)
            music.Pause();
        else
            music.Play();

        if (iconImage != null)
            iconImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
