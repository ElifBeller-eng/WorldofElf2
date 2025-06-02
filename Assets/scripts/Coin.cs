using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum CoinType { Gold, Silver }
    public CoinType coinType = CoinType.Gold;

    public AudioSource coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Çarpışan obje: " + collision.gameObject.name + " | Coin tipi: " + coinType);

        if (collision.CompareTag("Player1"))
        {
            var player = collision.GetComponent<MoveControle1yeni>();
            if (coinType == CoinType.Gold)
            {
                player.goldCoin += 1;
                player.UpdateGoldCoinUI();
                player.CheckForExtraLife();
            }
            else if (coinType == CoinType.Silver)
            {
                player.silverCoin += 1;
                player.UpdateSilverCoinUI();
            }
        }
        else if (collision.CompareTag("Player2"))
        {
            var player = collision.GetComponent<MoveControle2yeni>();
            if (coinType == CoinType.Gold)
            {
                player.goldCoin += 1;
                player.UpdateGoldCoinUI();
                player.CheckForExtraLife();
            }
            else if (coinType == CoinType.Silver)
            {
                player.silverCoin += 1;
                player.UpdateSilverCoinUI();
            }
        }

        PlayCoinSound(); // Ses çal
        Destroy(gameObject); // Coin objesini yok et
    }

    // Bu eksik metodu ekledik!
    private void PlayCoinSound()
    {
        if (coinSound != null && coinSound.clip != null)
        {
            GameObject soundObj = new GameObject("CoinSound");
            AudioSource aSource = soundObj.AddComponent<AudioSource>();

            aSource.clip = coinSound.clip;
            aSource.volume = coinSound.volume;
            aSource.pitch = coinSound.pitch;
            aSource.spatialBlend = 0f; // 2D ses
            aSource.Play();

            Destroy(soundObj, coinSound.clip.length);
        }
    }
}
