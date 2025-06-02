using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    bool started = false;
    public bool final = false;
    public GameObject player;
    private int coinamount1 = 0;

    private TextMeshProUGUI textComponent;
    private MoveControle1yeni moveScript;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
         if (textComponent == null)
        {   
        Debug.LogError("Score scriptine bağlı GameObject üzerinde TextMeshProUGUI componenti bulunamadı!");
        }

        if (player != null)
        {
            moveScript = player.GetComponent<MoveControle1yeni>();

            if (moveScript == null)
                Debug.LogError("MoveControle1yeni scripti player objesine eklenmemiş!");
        }
        else
        {
            Debug.LogError("Player GameObject atanmamış!");
        }
    }

    void Update()
    {
        if (!final && moveScript != null && moveScript.coinamount != coinamount1)
        {
            coinamount1 = moveScript.coinamount;
            textComponent.text = ":" + coinamount1.ToString();
            Debug.Log("UI Güncellendi: " + coinamount1);
        }
    }

}

