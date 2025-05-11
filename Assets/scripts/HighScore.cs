using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    bool started = false;
    public bool final = false;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(final)
        {
            if (PlayerPrefs.GetInt("HighScore") < player.GetComponent<MoveControle>().coinamount)
            {
                PlayerPrefs.SetInt("HighScore", player.GetComponent<MoveControle>().coinamount);
                gameObject.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + PlayerPrefs.GetInt("HighScore").ToString();
                
            }
            gameObject.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + PlayerPrefs.GetInt("HighScore").ToString();
            
        }
        Debug.Log("Highest Score: " + PlayerPrefs.GetInt("HighScore").ToString());
        Debug.Log("selam");
        
        gameObject.GetComponent<TextMeshProUGUI>().text = "Highest Score: " + PlayerPrefs.GetInt("HighScore").ToString();

    }
}