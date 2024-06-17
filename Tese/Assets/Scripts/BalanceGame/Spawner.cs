using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject[] ObjectsToSpawn;
    public Transform[] spawnPositions;
    public float delay = 2f;
    public float speed = 3f;
    float nextTimeToSpawn;

    void Start()
    {
        nextTimeToSpawn = Time.time;   
    }

    private void Update()
    {
        if (Time.time > nextTimeToSpawn)
        {
            nextTimeToSpawn = Time.time + delay;
            int randomObjectIndex = Random.Range(0, ObjectsToSpawn.Length);
            int randomSpawnPosition = Random.Range(0, spawnPositions.Length);
            GameObject go = Instantiate(ObjectsToSpawn[randomObjectIndex], spawnPositions[randomSpawnPosition].position, Quaternion.identity);
            go.AddComponent<Move>();
            go.GetComponent<Move>().speed = speed;
        }
    }

    private class Move : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}