using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f); // 2 saniye sonra otomatik yok olur
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
{
    Destroy(gameObject); // Ekrandan çıkınca da yok ol
}
}
