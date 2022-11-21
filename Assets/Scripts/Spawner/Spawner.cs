using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AudioClip beginWave;
    public AudioSource source;

    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public int waveValueMultiplier = 10;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDurationLvl5;
    public int waveDurationLvl10;
    public int waveDurationLvl20;
    public int waveDuration;
    public float waveTimer;
    private float spawnInterval;
    private float maxSpawnInterval = 1;
    public float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            //check if an enemy needs to be spawned
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity); // spawn first enemy in the list
                enemiesToSpawn.RemoveAt(0); // and remove it from the list
                spawnedEnemies.Add(enemy); // add it to spawned enemies list
                spawnTimer = spawnInterval;

                // alternating between spawns
                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer <= 0)
                waveTimer -= Time.fixedDeltaTime;
        }

        
        // If the waveTimer is lower than 0 and there are no enemies alive...
        if (waveTimer <= 0 && spawnedEnemies.Count <= 0) 
        {
            //... Go to the next wave
            currWave++;
            source.PlayOneShot(beginWave);
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        // different waveDurations after certain levels
        if (currWave < 5)
            waveDuration = waveDurationLvl5;

        else if (currWave < 10)
            waveDuration = waveDurationLvl10;

        else if (currWave < 20)
            waveDuration = waveDurationLvl20;

        else
            waveDuration = 150;

        waveValue = currWave * waveValueMultiplier;
        GenerateEnemies();

        waveTimer = waveDuration;

        // gives a fixed time between each enemies
        spawnInterval = waveDuration / enemiesToSpawn.Count;

        if (spawnInterval < 1)// doesn't go below 0, otherwise the enemies would spawn instantly
            spawnInterval = maxSpawnInterval;
        else
            spawnInterval = waveDuration / enemiesToSpawn.Count;
    }

    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // in a loop grab a random enemy         
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int EnemyId = Random.Range(0, enemies.Count);
            int EnemyCost = enemies[EnemyId].cost;

            if (waveValue - EnemyCost >= 0) // see if we can afford it, if we can, add it to the list, and deduct the cost.
            {
                generatedEnemies.Add(enemies[EnemyId].enemyPrefab);
                waveValue -= EnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
