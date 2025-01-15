using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject[] Fruits;
    public GameObject[] Sweets;

    public float spawnPeriod = 10;

    private int maxNumObjects = 10;
    private int currentNumObjects = 10;



    // Start is called before the first frame update
    void Start()
    {
        /*
        for(int i=0; i<maxNumObjects; i++)
        { 
            SpawnFruit();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //counts all objects that are catchable
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("catchableObject");
        currentNumObjects = allObjects.Length;
        Debug.Log(currentNumObjects);

        if (currentNumObjects < maxNumObjects)
        {
            //decides if spawned Object is a fruit or a sweet
            int randSelect = Random.Range(1,4);
            if(randSelect == 1) 
            { 
                SpawnSweet(); 
            } else { 
                SpawnFruit(); 
            }

        }
    }

    void SpawnFruit()
    {
        Debug.Log("Spawning a fruit!");
        GameObject newFruit = Fruits[Random.Range(0, Fruits.Length)]; //selects a random fruit from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, Random.Range(-4f, 4f));
        GameObject instantiatedFruit = Instantiate(newFruit, spawnPosition, Quaternion.identity);
        instantiatedFruit.tag = "catchableObject";
    }

    void SpawnSweet()
    {
        Debug.Log("Spawning a sweet!");
        GameObject newSweet = Sweets[Random.Range(0, Sweets.Length)]; //selects a random sweet from the array

        //spawn position is randomized & tag is assigned
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, Random.Range(-4f, 4f));
        GameObject instantiatedSweet = Instantiate(newSweet, spawnPosition, Quaternion.identity);
        instantiatedSweet.tag = "catchableObject";
    }
}
