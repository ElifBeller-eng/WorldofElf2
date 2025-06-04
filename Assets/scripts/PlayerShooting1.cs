using UnityEngine;

public class PlayerShooting1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    //public AudioSource shootSound;

    //void Update()
    //{
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
           // FireBullet();
        //}
    //}

    public void FireBullet()
    {
        
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Karakterin bakış yönüne göre ateşle (scale.x < 0 sağa bakıyor)
            float direction = transform.localScale.x < 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * bulletSpeed, 0);

            //if (shootSound != null)
            //{
            //shootSound.Play();
            //}

            Debug.Log("Kurşun ateşlendi.");
        }
        else
        {
            Debug.LogWarning("BulletPrefab veya FirePoint eksik!");
        }
    }
}
