using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] Fruits;
    public GameObject[] Sweets;

    public float spawnPeriod = 10;
    public float fallSpeed;
    public int counter;

    private int maxNumObjects = 5;
    private int currentNumObjects = 0;
    private PlayerController playerController;
    private bool hasIncreased = false;

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
        Debug.Log(currentNumObjects);

        if (currentNumObjects < maxNumObjects)
        {
            //decides if spawned Object is a fruit or a sweet
            int randSelect = Random.Range(1, 4);
            if (randSelect == 1)
            {
                SpawnSweet();
            }
            else
            {
                SpawnFruit();
            }
        }

        // Increase maxNumObjects and fallSpeed only if counter is divisible by 10 and maxNumObjects is less than a certain limit
        if (playerController.counter % 10 == 0 && playerController.counter != 0 && !hasIncreased && maxNumObjects <= 10)
        {
            maxNumObjects++;
            fallSpeed += 10f;
            hasIncreased = true; // Setze die Variable auf true, um anzuzeigen, dass die Erhöhung stattgefunden hat
        }
        else if (playerController.counter % 10 != 0)
        {
            hasIncreased = false; // Setze die Variable zurück, wenn der counter nicht mehr durch 10 teilbar ist
        }
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