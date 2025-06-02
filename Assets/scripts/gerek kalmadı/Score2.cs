using TMPro;
using UnityEngine;

public class Score2 : MonoBehaviour
{
    public bool final = false;
    public GameObject player2;
    private int coinamount2 = 0;

    private TextMeshProUGUI textComponent;
    private MoveControle2yeni moveScript;
    //private bool started = true; // test amaçlı doğrudan aktif

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (player2 != null)
        {
            moveScript = player2.GetComponent<MoveControle2yeni>();

            if (moveScript == null)
                Debug.LogError("MoveControle2yeni scripti player2 objesine eklenmemiş!");
        }
        else
        {
            Debug.LogError("player2 GameObject atanmamış!");
        }
    }

    void Update()
    {
        if (moveScript != null)
    {
        if (coinamount2 != moveScript.coinamount)
        {
            coinamount2 = moveScript.coinamount;
            textComponent.text = coinamount2.ToString();
            Debug.Log("UI Güncellendi: " + coinamount2);
        }
    }
    }
}
