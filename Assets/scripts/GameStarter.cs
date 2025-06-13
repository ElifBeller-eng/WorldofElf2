using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [Header("Player Prefabs")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public GameObject camera1;

    [Header("Spawn Points")]
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    void Start()
    {
        if (player1Prefab != null && player1SpawnPoint != null)
        {
            camera1.GetComponent<CameraController>().player1 = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        }

        if (player2Prefab != null && player2SpawnPoint != null)
        {
            camera1.GetComponent<CameraController>().player2 = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);
        }
    }
}
