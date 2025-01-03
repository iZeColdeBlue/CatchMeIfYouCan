using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 2f;

    void Start()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("cubePrefab is not assigned in the Inspector!");
            return;
        }

        InvokeRepeating("SpawnCube", 1f, spawnInterval);
    }

    void SpawnCube()
    {
        Debug.Log("Spawning a cube!");

        if (cubePrefab == null)
        {
            Debug.LogError("cubePrefab is missing!");
            return;
        }

        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 10f, 0);
        Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }
}
