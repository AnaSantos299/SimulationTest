using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnAlvo : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;
    public Transform[] spawnPositions;
    private float gameDuration = 20f;
    private float delay = 2f;
    private float speed = 1f;
    private float nextTimeToSpawn;
    private float timer;

    public TextMeshProUGUI timerText;

    void Start()
    {
        nextTimeToSpawn = Time.time;
        timer = gameDuration;
    }

    private void Update()
    {
        //if timer 0s is less then the game duration
        if (timer > 0)
        { //if the current time is greater then the next time to spawn
            if (Time.time > nextTimeToSpawn)
            { //Set the next spawn to the Time plus the delay time 
                nextTimeToSpawn = Time.time + delay;
                //Objects to spwan list
                int randomObjectIndex = Random.Range(0, ObjectsToSpawn.Length);
                //object spawn list
                int randomSpawnPosition = Random.Range(0, spawnPositions.Length);
                //Instantiates a new object from a random prefab at a random position wuth no rotation and assigns it to the "go" variable.
                GameObject go = Instantiate(ObjectsToSpawn[randomObjectIndex], spawnPositions[randomSpawnPosition].position, Quaternion.identity);
                //attach the component "Move" to the gameobject.
                go.AddComponent<Move>();
                //ensures that the speed of the movement for the newly spawned object "go" is set to the value specified in the "spawnAlvo" script
                go.GetComponent<Move>().speed = speed;
            }
            //
            timer -= Time.deltaTime;

            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        // Ensure the timer value is non-negative
        float nonNegativeTimer = Mathf.Max(timer, 0);

        // Format the timer value as "mm:ss"
        string formattedTime = Mathf.Floor(nonNegativeTimer / 60).ToString("00") + ":" + Mathf.Floor(nonNegativeTimer % 60).ToString("00");

        // Update the UI text with the formatted time
        timerText.text = formattedTime;
    }

    private class Move : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

       private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
