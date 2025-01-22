using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] Fruits;
    public GameObject[] Sweets;

    public float spawnPeriod = 10;
    public int counter;

    private int maxNumObjects = 5;
    private int currentNumObjects = 0;

    private PlayerController playerController;
    private LifeTracker lifeTracker;

    private bool hasIncreased = false;
    private bool haveSpawned = false;

    private List<Vector3> usedPositions = new List<Vector3>();
    private float minDistance = 0.75f; // Mindestabstand zwischen den Objekten

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        lifeTracker = FindObjectOfType<LifeTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        countObjects(); //counts all objects that are catchable

        if (playerController.isActive)
        {
            spawnWave(); //spawns a wave of catchable objects
        }

        increaseDifficulty(); //increases difficulty by increasing maxNumObjects and fallSpeed

        Debug.Log("Max Num Objects: " + maxNumObjects);
    }

    void SpawnFruit()
    {
        GameObject newFruit = Fruits[Random.Range(0, Fruits.Length)]; //selects a random fruit from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = GetUniqueSpawnPosition();
        GameObject instantiatedFruit = Instantiate(newFruit, spawnPosition, Quaternion.identity);
        instantiatedFruit.tag = "catchableFruit";
    }

    void SpawnSweet()
    {
        GameObject newSweet = Sweets[Random.Range(0, Sweets.Length)]; //selects a random sweet from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = GetUniqueSpawnPosition();
        GameObject instantiatedSweet = Instantiate(newSweet, spawnPosition, Quaternion.identity);
        instantiatedSweet.tag = "catchableSweet";
    }

    Vector3 GetUniqueSpawnPosition()
    {
        Vector3 spawnPosition;
        bool positionIsValid;

        do
        {
            spawnPosition = new Vector3(Random.Range(-4f, 4f), 8f, Random.Range(-4f, 4f));
            positionIsValid = true;

            foreach (var usedPosition in usedPositions)
            {
                if (Vector3.Distance(spawnPosition, usedPosition) < minDistance)
                {
                    positionIsValid = false;
                    break;
                }
            }
        } while (!positionIsValid);

        usedPositions.Add(spawnPosition);
        return spawnPosition;
    }

    void countObjects()         //counts all objects that are catchable
    {
        GameObject[] catchableFruits = GameObject.FindGameObjectsWithTag("catchableFruit");
        GameObject[] catchableSweets = GameObject.FindGameObjectsWithTag("catchableSweet");
        GameObject[] allObjects = new GameObject[catchableFruits.Length + catchableSweets.Length];

        catchableFruits.CopyTo(allObjects, 0);
        catchableSweets.CopyTo(allObjects, catchableFruits.Length);
        currentNumObjects = allObjects.Length;

        // Debug.Log(currentNumObjects);
    }

    void increaseDifficulty()
    {
        // Increase maxNumObjects and fallSpeed only if counter is divisible by 10 and maxNumObjects is less than a certain limit
        if (playerController.counter % 5 == 0 && playerController.counter != 0 && !hasIncreased)
        {
            if (maxNumObjects <= 10)
            {
                maxNumObjects++;
                Debug.Log("Increasing  Max Num Objects");
            }

            hasIncreased = true;
            Debug.Log(hasIncreased);
        }

        if (playerController.counter % 10 != 0 && hasIncreased)
        {
            hasIncreased = false;
        }
    }

    void spawnWave()
    {
        if (currentNumObjects < maxNumObjects && !haveSpawned)
        {
            usedPositions.Clear(); // Clear used positions before spawning a new wave

            for (int i = 0; i < maxNumObjects - 1; i++)
            {
                SpawnSweet();
            }
            SpawnFruit();

            haveSpawned = true;
        }
        else if (currentNumObjects == 0)
        {
            haveSpawned = false;
            lifeTracker.lostLife = false;
        }
    }
}

