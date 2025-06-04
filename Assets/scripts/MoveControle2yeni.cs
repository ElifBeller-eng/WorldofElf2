using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class MoveControle2yeni : MonoBehaviour
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

    private bool grounded;
    public Camera cam;

    //RESTARTTAN SONRAKİ SORUN İÇİN EKLEDİM
    private Vector3 initialPosition;
    public TextMeshProUGUI silverCoinText;  // Gümüş coin için

    //public GameObject bulletPrefab;
    //public float bulletSpeed = 10f;
    public int life = 1; // başlangıçta sadece 1 can
    public TextMeshProUGUI lifeText;
    public Transform firePoint2;
    private Vector3 firePoint2OriginalLocalPosition;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position; // İlk pozisyonu burada kaydet
        firePoint2OriginalLocalPosition = firePoint2.localPosition;
    }


    private void Start()
    {
        UpdateGoldCoinUI();
        UpdateSilverCoinUI();
        UpdateLifeUI();
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

        if (Input.GetKey(KeyCode.A))
        {
            if (grounded)
            {
                anim.SetBool("walk", true);
            }
            Vector3 scale = transform.localScale;
            // sola bakması için
            scale.x = Mathf.Abs(scale.x); // sola bakmak için pozitif yap
            transform.localScale = scale;
            // firePoint'i sola kaydır
            firePoint2.localPosition = new Vector3(-Mathf.Abs(firePoint2OriginalLocalPosition.x), firePoint2OriginalLocalPosition.y, firePoint2OriginalLocalPosition.z);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            
        }

        if (Input.GetKeyUp(KeyCode.A) || !grounded)
        {
            anim.SetBool("walk", false);

        }

        if (Input.GetKey(KeyCode.D))
        {
            if (grounded)
            {
                anim.SetBool("walk", true);
            }
            Vector3 scale = transform.localScale;
            // sağa bakması için
            scale.x = -Mathf.Abs(scale.x);  // sağa bakmak için negatif yap
            transform.localScale = scale;
             // firePoint'i sağa kaydır
            firePoint2.localPosition = new Vector3(Mathf.Abs(firePoint2OriginalLocalPosition.x), firePoint2OriginalLocalPosition.y, firePoint2OriginalLocalPosition.z);

            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.D) || !grounded)
        {
            anim.SetBool("walk", false);

        }

        if (Input.GetKeyDown(KeyCode.W) && grounded)
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

        //if (Input.GetKeyDown(KeyCode.S)) // Oyuncu 2 → S tuşuyla ateş eder
        //{
            //FireBullet();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }

        if (collision.gameObject.CompareTag("death"))
        {
            // Önce sesi çal ve UI'ı güncelle
            deathSound.Play();
            // OYUNU DURDUR ⛔
            Time.timeScale = 0f;
            over.gameObject.SetActive(true);
            score.gameObject.GetComponent<Score2>().final = true;
            Highscore.gameObject.GetComponent<HighScore2>().final = true;
            GameManager2.Instance.ShowGameOverCharacter(1);
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
                score.gameObject.GetComponent<Score2>().final = true;
                Highscore.gameObject.GetComponent<HighScore2>().final = true;
                GameManager2.Instance.ShowGameOverCharacter(1);
            }
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
            CheckForExtraLife();   // ← ekstra can kontrolü
            coinSound.Play();      // varsa coin sesi
            Destroy(collision.gameObject);
        }
        // SILVER COIN toplama
        if (collision.gameObject.CompareTag("SilverCoin"))
        {
            silverCoin++;
            UpdateSilverCoinUI();
            coinSound.Play();      // varsa coin sesi
            Destroy(collision.gameObject);
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
        //GameObject bullet = Instantiate(bulletPrefab, firePoint2.position, Quaternion.identity);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //float direction = transform.localScale.x < 0 ? 1f : -1f;
        //rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);

        //Debug.Log("Mermi atıldı! Yön: " + (direction > 0 ? "Sağ" : "Sol"));
    //}


    public void CheckForExtraLife()
    {
        if (goldCoin >= 8) //BUNU 100E ÇIKAR ŞİMDİLİK RAHAT DENEYEBİLMEK İÇİN!!!
        {
            goldCoin -= 8; // 8 coin harca//BUNU 100E ÇIKAR ŞİMDİLİK RAHAT DENEYEBİLMEK İÇİN!!!
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