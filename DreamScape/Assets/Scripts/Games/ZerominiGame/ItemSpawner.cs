using UnityEngine;
using Yarn.Unity;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Assign essential and non-essential prefabs
    public float spawnInterval = 1.0f;
    public float spawnRangeX = 8.0f; // Horizontal range for spawning items
    public float spawnHeight = 6.0f; // Height at which items spawn
    public GameObject Note;

    [YarnCommand("startGame")]
    public void StartGame()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
        Note.SetActive(false);
    }
    void SpawnItem()
    {
        // Choose a random item prefab
        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        // Instantiate the item and set its position
        GameObject item = Instantiate(itemPrefab);
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0);
        item.transform.position = spawnPosition;
    }
}
