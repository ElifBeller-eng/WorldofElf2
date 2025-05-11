using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;

    public GameObject ground;
    bool started = false;
    public int speed = 3;
    public int xspeed = 10;
    void Start()
    {

       
    }
    
    // Update is called once per frame
   void Update()
{
    // Space tuşuna basıldığında oyunu başlat
    if (Input.GetKeyDown(KeyCode.Space))
    {
        started = true;
        Time.timeScale = 1; // Oyun zamanını başlat
        ground.SetActive(true); // Oyun başladığında ground objesini görünür yap
    }

   
}

    void LateUpdate()
    {
        if (started)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            if (player != null && player.transform.position.y > transform.position.y + 5)
            {
                transform.position += new Vector3(0, xspeed, 0) * Time.deltaTime;
            }
        }

    }
}
