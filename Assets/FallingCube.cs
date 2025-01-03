using UnityEngine;

public class FallingCube : MonoBehaviour
{
    public float fallSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -1f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
{
    if (CompareTag("FallingCube"))
    {
        Debug.Log("Player touched FallingCube. Destroying the cube.");
        Destroy(gameObject); // Zničí padajúcu kocku
    }
}
   
}
