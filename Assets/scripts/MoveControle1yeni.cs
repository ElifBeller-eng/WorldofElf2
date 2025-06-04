using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class MoveControle1yeni : MonoBehaviour
{
    public int goldCoin = 0;
    public int silverCoin = 0;
    public Canvas over;
    public TextMeshProUGUI score;
    public TextMeshProUGUI Highscore;
    private Rigidbody2D rb;
    public TextMeshProUGUI coinText;
    [SerializeField] private float moveSpeed, jumpForce;

    private bool move;
    public int coinamount = 0;
    public AudioSource coinSound;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private AudioSource jumpSound, deathSound;

    private bool grounded; // Compte les contacts avec le sol
    public Camera cam;

    //RESTARTTAN SONRAKİ SORUN İÇİN EKLEDİM
    private Vector3 initialPosition;
    public TextMeshProUGUI silverCoinText;  // Gümüş coin için
    public GameObject bulletPrefab;
    //public Transform firePoint;
    //public float bulletSpeed = 10f;
    public int life = 1; // başlangıçta sadece 1 can
    public TextMeshProUGUI lifeText;
    private bool canShoot = false;
    private float shootTimer = 0f;
    public float shootDuration = 60f; // Ateş etme süresi: 60 saniye
    public PlayerShooting1 shooter; // Inspector’dan bağlayacağız


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position; // Başlangıç pozisyonunu kaydet
    }

    private void Start()
    {
        UpdateGoldCoinUI();
        UpdateSilverCoinUI();
        UpdateLifeUI();
        canShoot = false;
        shootTimer = 0f;
    }

    public void ResetPlayer()
    {
        life = 1;             // Örneğin başlangıç canı 1
        UpdateLifeUI();       // UI'ı güncelle
        transform.position = initialPosition;
        rb.linearVelocity = Vector2.zero; // ← velocity yerine linearVelocity
        grounded = false;
        anim.SetBool("jump", false);
        anim.SetBool("walk", false);
        // Zemin kontrolü gecikmeli olarak çalıştır //RESTARTTAN SONRAKİ SORUN İÇİN EKLEDİM
        Invoke("EnableGroundCheck", 0.1f);
        
    }

    //RESTARTTAN SONRAKİ SORUN İÇİN EKLEDİM
    void EnableGroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("ground"));
        grounded = hit.collider != null;
        Debug.Log("EnableGroundCheck → grounded = " + grounded);
    }

    public void Update()
    {
        Debug.Log("Grounded: " + grounded); // Bu satırı ekleyin!

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (grounded)
            {
                anim.SetBool("walk", true);
            }
            Vector3 scale = transform.localScale;
            // sola bakması için
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || !grounded)
        {
            anim.SetBool("walk", false);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (grounded)
            {
                anim.SetBool("walk", true);
            }
            Vector3 scale = transform.localScale;
            // sağa bakması için
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || !grounded)
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            jumpSound.Play();
            anim.SetBool("jump", true);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
        }

        if (grounded)
        {
            anim.SetBool("jump", false);
        }

        if (!grounded)
        {
            anim.SetBool("jump", true);
        }
        Debug.Log("Gold: " + goldCoin + " | Silver: " + silverCoin);

        //if (Input.GetKeyDown(KeyCode.DownArrow)) // Sağ Ctrl ile ateş
        //{
        //FireBullet();
        //}
        // Ateş etme süresi kontrolü
        if (canShoot)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                canShoot = false;
                shootTimer = 0f;
                Debug.Log("Ateş etme süresi doldu!");
            }
        }

        if (canShoot && shooter != null && Input.GetKeyDown(KeyCode.DownArrow))
        {
            shooter.FireBullet();
        }


}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {

            grounded = true;

        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Danger"))
        {
            if (life > 1)
            {
                TakeDamage(1);
            }
            else
            {
                deathSound.Play();
                Time.timeScale = 0f;
                over.gameObject.SetActive(true);
                score.gameObject.GetComponent<Score>().final = true;
                Highscore.gameObject.GetComponent<HighScore>().final = true;
                GameManager2.Instance.ShowGameOverCharacter(0);
            }
        }

        if (collision.gameObject.CompareTag("death"))
        {
            deathSound.Play();
            Time.timeScale = 0f;
            over.gameObject.SetActive(true);
            score.gameObject.GetComponent<Score>().final = true;
            Highscore.gameObject.GetComponent<HighScore>().final = true;
            GameManager2.Instance.ShowGameOverCharacter(0);
        }

    }    

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("WeakSpot"))
        {
            Destroy(collision.transform.parent.gameObject);
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(0, jumpForce / 3, 0), ForceMode2D.Impulse);
        }

        // GOLD COIN toplama
        if (collision.gameObject.CompareTag("Coin"))
        {
            goldCoin++;
            UpdateGoldCoinUI();
            CheckForExtraLife();   // ← burası can verir
            if (coinSound != null)
                coinSound.Play(); // ses varsa
            //coinSound.Play();     
            Destroy(collision.gameObject);
        }

        // SILVER COIN toplama
        if (collision.gameObject.CompareTag("SilverCoin"))
        {
            silverCoin++;
            UpdateSilverCoinUI();
            coinSound.Play();      // ses varsa
            Destroy(collision.gameObject);
            // Her 10 silverCoin'de ateş etme yetkisi ver
            if (silverCoin >= 10) // 10 tane silverCoin topladığında ateş etme yetkisi ver
            {
                silverCoin -= 10; //10 gümüş azalt (kullanıldığı için)
                UpdateSilverCoinUI(); // UI’ı güncelle
                canShoot = true;
                shootTimer = shootDuration;
            }
        }
        Debug.Log("Çarpışma oldu: " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = false;
        }
    }

    private bool IsGrounded()
    {
        return grounded;
    }

    public void UpdateGoldCoinUI()
    {
        if (coinText != null)
            coinText.text = ":" + goldCoin.ToString();
    }

    public void UpdateSilverCoinUI()
    {
        if (silverCoinText != null)
            silverCoinText.text = ":" + silverCoin.ToString();
    }

    //void FireBullet()
    //{
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //float direction = transform.localScale.x > 0 ? 1f : -1f;
        //rb.linearVelocity = new Vector2(direction * bulletSpeed, 0);
        //Debug.Log("Mermi atıldı!");
    //}

    public void CheckForExtraLife()
    {
        if (goldCoin >= 10) //BUNU 100E ÇIKAR ŞİMDİLİK RAHAT DENEYEBİLMEK İÇİN!!!
        {
            goldCoin -= 10; //BUNU 100E ÇIKAR ŞİMDİLİK RAHAT DENEYEBİLMEK İÇİN!!!
            life++;
            UpdateLifeUI();
            UpdateGoldCoinUI(); // coin UI'ı da güncelle
            Debug.Log("Ekstra can kazanıldı! Toplam can: " + life);
        }
    }

    public void UpdateLifeUI()
    {
        if (lifeText != null)
            lifeText.text = ":" + life.ToString();
    }

    public void TakeDamage(int damageAmount)
    {   
        Debug.Log($"TakeDamage called: Current Life = {life}, Damage = {damageAmount}");
        life -= damageAmount;       // Canı azalt
        if (life < 0) life = 0;     // Can 0’ın altına düşmesin
        Debug.Log($"TakeDamage result: New Life = {life}");
        UpdateLifeUI();             // UI'ı güncelle
        if (life == 0)
        {
            Debug.Log("Oyuncu öldü! Game Over işlemleri burada yapılmalı.");
            // Buraya game over kodlarını ekleyebilirsin
        }
    }
    
}
