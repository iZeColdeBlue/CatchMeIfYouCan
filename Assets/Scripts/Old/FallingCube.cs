using UnityEngine;

public class FallingCube : MonoBehaviour
{
    public float fallSpeed = 3f;

    private PlayerController playerController;
    private LifeTracker lifeTracker;

    private Vector3 spinSpeed = new Vector3(0, 90f, 0);

    void Start()
    {
        // Ensure the collider is set as a trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        // Find the PlayerController in the scene
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        // Find the LifeTracker in the scene
        lifeTracker = FindObjectOfType<LifeTracker>();

        for (int i = 0; i < playerController.counter / 10; i++)
        {
            fallSpeed += 0.5f;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        transform.Rotate(spinSpeed * Time.deltaTime);

        if (transform.position.y < -1f) // if object is below -1f(ground) destroy it
        {
            if (CompareTag("catchableFruit"))
            {
                lifeTracker.clearLife();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("catchableSweet"))
            {
                lifeTracker.clearLife();
            }

            //if player catches the falling cube, destroy the cube
            Destroy(gameObject);

            // Call IncreaseScore function in PlayerController
            if (playerController != null)
            {
                playerController.IncreaseScore();
            }
        }

        if (other.CompareTag("Environment"))
        {
            if (CompareTag("catchableFruit"))
            {
                lifeTracker.clearLife();
            }

            //if player catches the falling cube, destroy the cube
            Destroy(gameObject);
        }
    }
}
