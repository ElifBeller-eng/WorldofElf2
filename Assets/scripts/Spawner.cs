using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject platform1;
    public GameObject platform2;
    public GameObject coin1;
    public GameObject bulut;
    public GameObject enemy1;
    public int uzaklık = 5;
    int positionoffset = 7;
    float mytime=0f;
    float constant = 1f;
    int oldxoffset = 0;
    int xoffset = 0;
    public GameObject bat;
    public GameObject darkPlatform1;
    public GameObject enemyDark;
    public GameObject bronzeCoin;


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



    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;



        if (sceneName == "DarkScene")
        {
            bulut = bat;
            platform1 = darkPlatform1;
            enemy1 = enemyDark;
        }


        int xoffset=3*Random.Range(-3, 3);
        while(xoffset==oldxoffset)
        {
            oldxoffset = xoffset;
            xoffset=3*Random.Range(-3, 3);
            
        }
        for(int i = 0;i<5;i++)
        {
            //Instantiate(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);
            InstantiateIfNotNull(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);
            //GOLD için
            if (Random.Range(0, 3) < 1)
            {
                //Instantiate(coin1, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
                InstantiateIfNotNull(coin1, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
            }
            // BRONZE COIN (örneğin daha az sıklıkla çıksın)
            if (Random.Range(0, 3) < 1)
            {
                //Instantiate(bronzeCoin, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
                InstantiateIfNotNull(bronzeCoin, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
            }
            if (Random.Range(0, 4) < 1)
            {
                //Instantiate(enemy1, new Vector3(xoffset + Random.Range(-1f, 1f), positionoffset + 2, 0), Quaternion.identity);
                InstantiateIfNotNull(enemy1, new Vector3(xoffset + Random.Range(-1f, 1f), positionoffset + 2, 0), Quaternion.identity);
            }
            
            int bulutxoffset=Random.Range(-2,2);
            int bulutyoffset=Random.Range(-7,7);
            if(3<Random.Range(0, 12))
            {
                //Instantiate(bulut, new Vector3(xoffset+bulutxoffset, 2*positionoffset+bulutyoffset+17, 0), Quaternion.identity);
                InstantiateIfNotNull(bulut, new Vector3(xoffset+bulutxoffset, 2*positionoffset+bulutyoffset+17, 0), Quaternion.identity);
                if (8 < Random.Range(0, 10))
                {
                    //Instantiate(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                    InstantiateIfNotNull(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                }
            }
            positionoffset += uzaklık;
            xoffset=3*Random.Range(-3, 3);
            Debug.Log("xoffset: "+xoffset);
            while(xoffset==oldxoffset)
            {
                
                xoffset=3*Random.Range(-3, 3);
                Debug.Log("while xoffset: "+xoffset);
                
            }
            oldxoffset = xoffset;
        }
        
    }

    // Update is called once per frame
    void Update()
    {  
        mytime+= Time.deltaTime;
        int xoffset=3*Random.Range(-3, 3);
        while(xoffset==oldxoffset)
        {
            
            xoffset=3*Random.Range(-3, 3);
            
        }
        
        if(mytime>constant)
        {
            //Instantiate(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);
            InstantiateIfNotNull(platform1, new Vector3(xoffset, positionoffset, 0), Quaternion.identity);
            if (Random.Range(0, 3) < 1)
            {
                //Instantiate(coin1, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
                InstantiateIfNotNull(coin1, new Vector3(xoffset + Random.Range(-2f, 2f), positionoffset + 2, 0), Quaternion.identity);
            }
            if (Random.Range(0, 4) < 1)
            {
                //Instantiate(enemy1, new Vector3(xoffset+Random.Range(-1f, 1f), positionoffset+2, 0), Quaternion.identity);
                InstantiateIfNotNull(enemy1, new Vector3(xoffset+Random.Range(-1f, 1f), positionoffset+2, 0), Quaternion.identity);
            }
            int bulutxoffset=Random.Range(-2,2);
            int bulutyoffset=Random.Range(-7,7);
            if(3<Random.Range(0, 12))
            {
                //Instantiate(bulut, new Vector3(xoffset+Random.Range(-2,2), 2*positionoffset+Random.Range(-7,7)+17, 0), Quaternion.identity);
                InstantiateIfNotNull(bulut, new Vector3(xoffset+Random.Range(-2,2), 2*positionoffset+Random.Range(-7,7)+17, 0), Quaternion.identity);
                if (3 < Random.Range(0, 10))
                {
                    //Instantiate(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                    InstantiateIfNotNull(bulut, new Vector3(xoffset + bulutxoffset + Random.Range(5, 15), 2 * positionoffset + bulutyoffset + 22, 0), Quaternion.identity);
                }
            }
            
            positionoffset += uzaklık;
            mytime = 0;
            oldxoffset = xoffset;
        }
    }
}
