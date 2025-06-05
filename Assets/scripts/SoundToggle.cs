using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [Header("Ses Kaynağı")]
    [SerializeField] private AudioSource audioSource; // Tek kaynak
    [SerializeField] private bool startWithSoundOn = true;

    [Header("Buton Görseli")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private void Start()
    {
        // AudioSource kontrolü
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogError("AudioSource eklenmedi! Otomatik eklendi.");
        }

        // Başlangıç durumu
        audioSource.playOnAwake = false; // Elle kontrol edeceğiz
        audioSource.loop = true;

        // Kayıtlı ayarı yükle
        bool shouldPlay = PlayerPrefs.GetInt("SoundEnabled", startWithSoundOn ? 1 : 0) == 1;
        
        if (shouldPlay && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        UpdateButtonImage();
    }

    public void ToggleSound()
    {
        bool willPlay = !audioSource.isPlaying;
        
        if (willPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        PlayerPrefs.SetInt("SoundEnabled", willPlay ? 1 : 0);
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = audioSource.isPlaying ? soundOnSprite : soundOffSprite;
        }
    }
}