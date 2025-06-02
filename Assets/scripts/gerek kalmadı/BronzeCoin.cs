using UnityEngine;

public class BronzeCoin : MonoBehaviour
{
    public int coinValue = 100; // Coin başına kaç bronze coin verilecek

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncuda PlayerShooting scripti var mı kontrol et (hem Player1 hem Player2 desteklenir)
        PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();

        if (playerShooting != null)
        {
            // Bronze coin miktarını artır
            playerShooting.bronzeCoins += coinValue;

            // İsteğe bağlı: debug log
            Debug.Log($"{collision.gameObject.name} {coinValue} bronze coin aldı. Toplam: {playerShooting.bronzeCoins}");

            // Coin nesnesini yok et
            Destroy(gameObject);
        }
    }
}
