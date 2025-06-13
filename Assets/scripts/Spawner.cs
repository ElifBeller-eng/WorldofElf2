using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject coin;
    public GameObject bulut;
    public GameObject enemy1;
    public int uzaklık = 5;
    public GameObject darkPlatform1;
    public GameObject enemyDark;
    public GameObject SilverCoin;

    float screenHalfWidthInWorldUnits;
    float platformWidth = 5f;
    float mytime = 0f;
    float constant = 1f;
    float oldxoffset = 0f;
    int positionoffset = 10;

    void InstantiateIfNotNull(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab != null)
        {
            Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.LogWarning("Prefab atanmamış! Pozisyon: " + position);
        }
    }

    void UpdateScreenWidth()
    {
        float orthoSize = Camera.main.orthographicSize;
        float aspect = (float)Screen.width / Screen.height;
        screenHalfWidthInWorldUnits = orthoSize * aspect;
    }

    void Start()
    {
        UpdateScreenWidth();

        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName == "DarkScene")
        {
            platform1 = darkPlatform1;
            enemy1 = enemyDark;
        }

        float minX = -screenHalfWidthInWorldUnits + platformWidth / 2f;
        float maxX = screenHalfWidthInWorldUnits - platformWidth / 2f;

        float xoffset = Random.Range(minX, maxX);
        while (Mathf.Abs(xoffset - oldxoffset) < 1f)
        {
            xoffset = Random.Range(minX, maxX);
        }

        for (int i = 0; i < 5; i++)
        {
            InstantiateIfNotNull(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);

            if (Random.Range(0, 3) < 1)
            {
                float coinX = Mathf.Clamp(xoffset + Random.Range(-2f, 2f), minX, maxX);
                InstantiateIfNotNull(coin, new Vector3(coinX, positionoffset + 2, 0), Quaternion.identity);
            }

            if (Random.Range(0, 3) < 1)
            {
                float silverCoinX = Mathf.Clamp(xoffset + Random.Range(-2f, 2f), minX, maxX);
                InstantiateIfNotNull(SilverCoin, new Vector3(silverCoinX, positionoffset + 2, 0), Quaternion.identity);
            }

            if (Random.Range(0, 5) < 1)
            {
                float enemyX = Mathf.Clamp(xoffset + Random.Range(-1f, 1f), minX, maxX);
                InstantiateIfNotNull(enemy1, new Vector3(enemyX, positionoffset + 2, 0), Quaternion.identity);
            }

            int bulutxoffset = Random.Range(-2, 2);
            int bulutyoffset = Random.Range(-7, 7);
            if (3 < Random.Range(0, 12))
            {
                InstantiateIfNotNull(bulut, new Vector3(xoffset + bulutxoffset, 2 * positionoffset + bulutyoffset + 17, 0), Quaternion.identity);
                if (8 < Random.Range(0, 10))
                {
                    InstantiateIfNotNull(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                }
            }

            positionoffset += uzaklık;
            oldxoffset = xoffset;

            xoffset = Random.Range(minX, maxX);
            while (Mathf.Abs(xoffset - oldxoffset) < 1f)
            {
                xoffset = Random.Range(minX, maxX);
            }
        }
    }

    void Update()
    {
        UpdateScreenWidth();

        mytime += Time.deltaTime;

        if (mytime > constant)
        {
            float minX = -screenHalfWidthInWorldUnits + platformWidth / 2f;
            float maxX = screenHalfWidthInWorldUnits - platformWidth / 2f;

            float xoffset = Random.Range(minX, maxX);
            while (Mathf.Abs(xoffset - oldxoffset) < 1f)
            {
                xoffset = Random.Range(minX, maxX);
            }

            InstantiateIfNotNull(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);

            if (Random.Range(0, 3) < 1)
            {
                float coinX = Mathf.Clamp(xoffset + Random.Range(-2f, 2f), minX, maxX);
                InstantiateIfNotNull(coin, new Vector3(coinX, positionoffset + 2, 0), Quaternion.identity);
            }

            if (Random.Range(0, 6) < 1)
            {
                float enemyX = Mathf.Clamp(xoffset + Random.Range(-1f, 1f), minX, maxX);
                InstantiateIfNotNull(enemy1, new Vector3(enemyX, positionoffset + 2, 0), Quaternion.identity);
            }

            if (Random.Range(0, 3) < 1)
            {
                float silverCoinX = Mathf.Clamp(xoffset + Random.Range(-2f, 2f), minX, maxX);
                InstantiateIfNotNull(SilverCoin, new Vector3(silverCoinX, positionoffset + 2, 0), Quaternion.identity);
            }

            int bulutxoffset = Random.Range(-2, 2);
            int bulutyoffset = Random.Range(-7, 7);
            if (3 < Random.Range(0, 12))
            {
                InstantiateIfNotNull(bulut, new Vector3(xoffset + Random.Range(-2, 2), 2 * positionoffset + Random.Range(-7, 7) + 17, 0), Quaternion.identity);
                if (3 < Random.Range(0, 10))
                {
                    InstantiateIfNotNull(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                }
            }

            positionoffset += uzaklık;
            mytime = 0;
            oldxoffset = xoffset;
        }
    }
}
