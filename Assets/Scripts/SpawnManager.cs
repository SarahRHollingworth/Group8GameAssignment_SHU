using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemy;

    // Instantiate(spikeTrap, spikeTrapSpawnPoint, player.transform.rotation);

   


    //Coordinates
    //Top Gate: x = 0, y = 6
    //Left Gate: x = -11.7, y = 0
    //Bottom Gate: x = 0, y = -6
    //Right Gate = x = 11.7, y = 0
    Vector3[] gateCoordinates = new Vector3[4] { new Vector3(0.0f, 6f, 0.0f), new Vector3(-11.7f, 0.0f, 0.0f), new Vector3(0.0f, -6f, 0.0f), new Vector3(11.7f, 0.0f, 0.0f) };


    const float maxEnemySpawnTimer = 4.0f;
    float timeUntilNextSpawn;


    void Start()
    {

        timeUntilNextSpawn = maxEnemySpawnTimer;



    }

    // Update is called once per frame
    void Update()
    {

        timeUntilNextSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn < 0 ) { 
            int enemySpawnPoint = GetGateCoordinateIndex();
            GenerateEnemy(enemySpawnPoint);

            timeUntilNextSpawn = maxEnemySpawnTimer;
        
        }

        
    }

    int GetGateCoordinateIndex()
    {
        int randomNumber = Random.Range(0, 4);
        return randomNumber;
    }

    void GenerateEnemy(int enemySpawnPoint)
    {
        Instantiate(enemy, gateCoordinates[enemySpawnPoint], transform.rotation);
    }
}
