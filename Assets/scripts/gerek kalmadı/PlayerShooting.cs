using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int bronzeCoins = 100;
    public KeyCode shootKey = KeyCode.Space;  // Bu tuşu Inspector’dan değiştireceğiz

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            if (bronzeCoins >= 100)
            {
                Shoot();
                bronzeCoins -= 100;
            }
            else
            {
                Debug.Log($"{gameObject.name} yeterli silver coin'e sahip değil!");
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.linearVelocity = direction * bulletSpeed;
    }
}
