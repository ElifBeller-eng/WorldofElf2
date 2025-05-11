using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Square : MonoBehaviour
{
    bool grounded = false;
    public TextMeshProUGUI score;
    public float speed = 5f;
    public float force = 15f;
    public Canvas over;
    [SerializeField]public AudioSource jumpsound;

    Rigidbody2D rb;
    Transform t;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="ground")
        {
            Debug.Log("grounded");
            grounded = true;
        }
        if(collision.gameObject.tag=="death")
        {
            
            over.gameObject.SetActive(true);
            score.gameObject.GetComponent<Score>().final = true;
            Destroy(gameObject);
        }
        
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="ground")
        {
            Debug.Log("not grounded");
            grounded = false;
        }
        
    }
    void Start()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1*speed, 0, 0)*Time.deltaTime;
            transform.rotation = new Quaternion(0,0,0,0);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1*speed, 0, 0)*Time.deltaTime;
            transform.rotation = new Quaternion(0,180,0,0);
        }
        if(Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
            jumpsound.Play();
            rb.AddForce(new Vector3(0, force, 0), ForceMode2D.Impulse);
           
        }

    }
}
