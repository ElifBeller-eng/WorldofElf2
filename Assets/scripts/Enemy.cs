using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    float startpointx;
    float startpointy;
    bool direction = true;
    public float speed = 0.5f;
    public float range= 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite darkSprite;


    void Start()
    {
        startpointx = gameObject.transform.position.x;
        startpointy = gameObject.transform.position.y;
        // SADECE DarkScene'deysek sprite'ı değiştir
        if (SceneManager.GetActiveScene().name == "DarkScene" && darkSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = darkSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x >= startpointx + range)
        {
            direction = false;
        }
        if(gameObject.transform.position.x <= startpointx - range)
        {
            direction = true;
        }
        if(direction)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed*Time.deltaTime, gameObject.transform.position.y, 0);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - speed*Time.deltaTime, gameObject.transform.position.y, 0);
        }
    }
}
