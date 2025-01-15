using UnityEngine;


public class CubeSpawner : MonoBehaviour
{

    public GameObject FallingCube;
    public float spawnInterval = 2f;

    public int MaxFallingObjects = 10;
    private int CurrentFallingObjects = 0;


    void Start()
    {
        for (int i = 0; i < MaxFallingObjects; i++)
        {
            SpawnCube();
            CurrentFallingObjects++;
        }

    }

    void Update()
    {

        if (CurrentFallingObjects <= MaxFallingObjects)
        {
            SpawnCube();
            CurrentFallingObjects++;
        }

    }

    void SpawnCube()
    {
        Debug.Log("Spawning a cube!");

        if (FallingCube == null)
        {
            Debug.LogError("cubePrefab is missing!");
            return;
        }

        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, 0);
        Instantiate(FallingCube, spawnPosition, Quaternion.identity);
    }
}
