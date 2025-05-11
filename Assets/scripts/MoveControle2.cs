using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class MoveControle2 : MonoBehaviour
{    
     public Canvas over;
     public TextMeshProUGUI score;
     public TextMeshProUGUI Highscore;
    private Rigidbody2D rb;
    
    [SerializeField] private float moveSpeed, jumpForce;

    private bool move;
    public int coinamount = 0;
    public AudioSource coinSound;
   
    private Animator anim;
    
    private SpriteRenderer sprite;
    [SerializeField] private AudioSource jumpSound,deathSound;
    

    private bool grounded; // Compte les contacts avec le sol
    public  camera  cam;

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
       
       
    }

   

    public void Update()
    {
         Debug.Log("Grounded: " + grounded); // Bu satırı ekleyin!
         
        if(Input.GetKey(KeyCode.A))
        {
            if(grounded)
            {
                anim.SetBool("walk",true);
            }
            transform.position += new Vector3(-1*moveSpeed, 0, 0)*Time.deltaTime;
            transform.rotation = new Quaternion(0,0,0,0);
        }

        if(Input.GetKeyUp(KeyCode.A)|| !grounded )
        {
            anim.SetBool("walk",false);

        }

        if(Input.GetKey(KeyCode.D))
        {
            if(grounded)
            {
                anim.SetBool("walk",true);
            }
            transform.position += new Vector3(1*moveSpeed, 0, 0)*Time.deltaTime;
            transform.rotation = new Quaternion(0,180,0,0);
        }

        if(Input.GetKeyUp(KeyCode.D)|| !grounded)
        {
            anim.SetBool("walk",false);

        }

        if(Input.GetKeyDown(KeyCode.W)&& grounded)
        {
            
            jumpSound.Play();
            anim.SetBool("jump",true);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);

            
           
        }
        if(grounded)
        {
            anim.SetBool("jump",false);
        }
        if (!grounded)
        {
            anim.SetBool("jump",true);
        }
        
       
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {

           grounded=true;
        
            // Permet de sauter quand le personnage touche le sol
        }

        if (collision.gameObject.CompareTag("death"))
        {
            // Önce sesi çal ve UI'ı güncelle
            
            
            deathSound.Play();
            over.gameObject.SetActive(true);
            score.gameObject.GetComponent<Score>().final = true;
            Highscore.gameObject.GetComponent<HighScore>().final = true;

             // En son objeyi yok et
            
       }
       
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {

           grounded=true;
        
            // Permet de sauter quand le personnage touche le sol
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
       {
           coinSound.Play();
       }
       if (collision.gameObject.CompareTag("WeakSpot"))
       {
            Destroy(collision.transform.parent.gameObject);
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(0, jumpForce/3, 0), ForceMode2D.Impulse);
       }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
           
            grounded=false; // Réduit le nombre de contacts avec le sol
           
        }
    }

    private bool IsGrounded()
    {
        return grounded;
    }
}
