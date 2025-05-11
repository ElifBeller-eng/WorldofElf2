using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public AudioSource coinSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinSound.Play();
            Destroy(gameObject);
            collision.gameObject.GetComponent<MoveControle>().coinamount += 1;
            
        }
    }
}
