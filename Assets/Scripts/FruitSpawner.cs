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

    private bool hasIncreased = false;
    private bool haveSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //counts all objects that are catchable
        GameObject[] catchableFruits = GameObject.FindGameObjectsWithTag("catchableFruit");
        GameObject[] catchableSweets = GameObject.FindGameObjectsWithTag("catchableSweet");
        GameObject[] allObjects = new GameObject[catchableFruits.Length + catchableSweets.Length];
        catchableFruits.CopyTo(allObjects, 0);
        catchableSweets.CopyTo(allObjects, catchableFruits.Length);
        currentNumObjects = allObjects.Length;
        // Debug.Log(currentNumObjects);

        if (currentNumObjects < maxNumObjects && !haveSpawned)
        {
            for (int i = 0; i < maxNumObjects - 1; i++)
            {
                SpawnSweet();
            }
            SpawnFruit();

            haveSpawned = true;

        }else if (currentNumObjects == 0)
        {
            haveSpawned = false;
        }

        // Increase maxNumObjects and fallSpeed only if counter is divisible by 10 and maxNumObjects is less than a certain limit
        if (playerController.counter % 10 == 0 && playerController.counter != 0 && !hasIncreased)
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

        Debug.Log("Max Num Objects: " + maxNumObjects);
    }


    void SpawnFruit()
    {
        GameObject newFruit = Fruits[Random.Range(0, Fruits.Length)]; //selects a random fruit from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, Random.Range(-4f, 4f));
        GameObject instantiatedFruit = Instantiate(newFruit, spawnPosition, Quaternion.identity);
        instantiatedFruit.tag = "catchableFruit";
    }

    void SpawnSweet()
    {
        GameObject newSweet = Sweets[Random.Range(0, Sweets.Length)]; //selects a random sweet from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, Random.Range(-4f, 4f));
        GameObject instantiatedSweet = Instantiate(newSweet, spawnPosition, Quaternion.identity);
        instantiatedSweet.tag = "catchableSweet";
    }
}