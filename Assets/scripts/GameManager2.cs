using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    [HideInInspector]
    public int selectedCharacterIndex = 0;

    public Sprite elf1LyingSprite;
    public Sprite elf2LyingSprite;

    public Image GameOverCharacterImage;

    public GameObject player1Character;
    public GameObject player2Character;

    // Prefablar kaldırıldı, sahnede hazır oyuncular var varsayıyoruz
    // public GameObject player1Prefab;
    // public GameObject player2Prefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadCharacterSelection();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        player1Character = GameObject.FindWithTag("Player1");
        player2Character = GameObject.FindWithTag("Player2");

        if (player1Character != null)
            ResetPhysics(player1Character);

        if (player2Character != null)
            ResetPhysics(player2Character);

        if (GameOverCharacterImage != null)
            GameOverCharacterImage.gameObject.SetActive(false);
        else
        {
            GameOverCharacterImage = GameObject.Find("GameOverCharacterImage")?.GetComponent<Image>();
            if (GameOverCharacterImage == null)
            {
                Debug.LogWarning("GameOverCharacterImage bulunamadı!");
            }
            else
            {
                GameOverCharacterImage.gameObject.SetActive(false);
            }
        }
    }

    void ResetPhysics(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.Sleep();
            rb.WakeUp();
        }
    }

    public void SaveCharacterSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
    }

    public void LoadCharacterSelection()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
    }

    public void ShowGameOverCharacter(int deadPlayerIndex)
    {
        Debug.Log("ShowGameOverCharacter çalıştı! Index: " + deadPlayerIndex);
        if (GameOverCharacterImage == null)
        {
            Debug.LogWarning("GameOverCharacterImage referansı yok! Yeniden bulmaya çalışılıyor...");
            GameOverCharacterImage = GameObject.Find("GameOverCharacterImage")?.GetComponent<Image>();
            if (GameOverCharacterImage == null)
            {
                Debug.LogError("GameOverCharacterImage hâlâ null! İsim hatalı olabilir.");
                return;
            }
        }

        if (player1Character != null)
            player1Character.SetActive(false);

        SpriteRenderer sr1 = player1Character?.GetComponent<SpriteRenderer>();
        if (sr1 != null)
            sr1.enabled = false;

        if (player2Character != null)
            player2Character.SetActive(false);

        SpriteRenderer sr2 = player2Character?.GetComponent<SpriteRenderer>();
        if (sr2 != null)
            sr2.enabled = false;

        if (deadPlayerIndex == 0)
        {
            GameOverCharacterImage.sprite = elf1LyingSprite;
            Debug.Log("Elf 1 sprite atandı.");
        }
        else
        {
            GameOverCharacterImage.sprite = elf2LyingSprite;
            Debug.Log("Elf 2 sprite atandı.");
        }

        GameOverCharacterImage.gameObject.SetActive(true);
        Debug.Log("GameOverCharacterImage gösterildi.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Sahne yeniden yüklendi.");
    }
}
