using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    bool started = false;
    public bool final = false;
    public GameObject player;
    public int coinamount1 = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            started = true;
        }
        if(started&&!final&&player.GetComponent<MoveControle>().coinamount!= coinamount1)
        {
            coinamount1 = player.GetComponent<MoveControle>().coinamount;
            gameObject.GetComponent<TextMeshProUGUI>().text =":"+ coinamount1.ToString();
        }
    }
}
