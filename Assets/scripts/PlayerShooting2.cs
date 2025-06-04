using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public KeyCode shootKey = KeyCode.S;

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Karakter sağa bakıyorsa sağa, sola bakıyorsa sola atış yapar
        Vector2 direction = transform.localScale.x < 0 ? Vector2.right : Vector2.left;
        rb.linearVelocity = direction * bulletSpeed;

        Debug.Log($"Elf2 ateş etti! Yön: {(direction == Vector2.right ? "Sağ" : "Sol")}");
    }
}

