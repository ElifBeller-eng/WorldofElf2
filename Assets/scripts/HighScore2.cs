using TMPro;
using UnityEngine;

public class HighScore2 : MonoBehaviour
{
    bool started = false;
    public bool final = false;
    public GameObject player2;

    void Update()
    {
        if (final)
        {
            // "HighScore2" anahtarı ikinci oyuncu için ayrı tutuluyor
            if (PlayerPrefs.GetInt("HighScore2") < player2.GetComponent<MoveControle2yeni>().coinamount)
            {
                PlayerPrefs.SetInt("HighScore2", player2.GetComponent<MoveControle2yeni>().coinamount);
                gameObject.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + PlayerPrefs.GetInt("HighScore2").ToString();
            }

            gameObject.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + PlayerPrefs.GetInt("HighScore2").ToString();
        }
        Debug.Log("Coin Amount Player 2: " + player2.GetComponent<MoveControle2yeni>().coinamount);
    }
}
