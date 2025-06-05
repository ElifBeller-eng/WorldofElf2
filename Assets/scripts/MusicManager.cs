using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource sampleAudio;
    public AudioSource darkAudio;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private static MusicManager instance;
    private Image iconImage;
    private bool isMuted;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        iconImage = GameObject.Find("IconImage")?.GetComponent<Image>();
        UpdateIcon();
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        sampleAudio.Stop();
        darkAudio.Stop();

        if (isMuted) return;

        if (sceneName == "SampleScene")
        {
        if (!sampleAudio.gameObject.activeSelf)
            sampleAudio.gameObject.SetActive(true);

        sampleAudio.Play();
        }
        else if (sceneName == "DarkScene")
        {
        if (!darkAudio.gameObject.activeSelf)
            darkAudio.gameObject.SetActive(true);

        darkAudio.Play();
        }

    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        if (isMuted)
        {
            sampleAudio.Pause();
            darkAudio.Pause();
        }
        else
        {
            PlayMusicForScene(SceneManager.GetActiveScene().name);
        }

        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (iconImage != null)
            iconImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
