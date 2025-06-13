using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject ground;

    private bool started = false;
    public float speed = 3f;
    public float catchUpSpeed = 6f;
    public float followThreshold = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            Time.timeScale = 1;
            ground.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if (!started || (player1 == null && player2 == null)) return;

        Vector3 pos = transform.position;

        // Her zaman sabit yukarı çık

        pos.y += speed * Time.deltaTime;

        // En yüksek oyuncuya göre kamera pozisyonu ayarla

        float highestY = pos.y;
        Debug.Log($"Player1 Y: {player1.transform.position.y}, Camera Y: {pos.y}, Threshold: {followThreshold}");
        if (player1 != null && player1.transform.position.y > highestY + followThreshold)
        {
            highestY = player1.transform.position.y - followThreshold;
        }

        if (player2 != null && player2.transform.position.y > highestY + followThreshold)
        {
            highestY = player2.transform.position.y - followThreshold;
        }

        if (highestY > pos.y)
        {
            pos.y = Mathf.Lerp(pos.y, highestY, catchUpSpeed * Time.deltaTime);
            Debug.Log("Catching up to player position: " + pos.y);
        }

        transform.position = pos;
    }
}
