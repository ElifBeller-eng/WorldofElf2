//FindObjectsOfType<GameObject>(); kullanılmıyo
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Active Scene: " + SceneManager.GetActiveScene().name);

        // Yeni API ile sahnedeki tüm GameObject'leri bul
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            Debug.Log("🧩 Object: " + obj.name + " | Scene: " + obj.scene.name);
        }
    }
}

