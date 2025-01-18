using UnityEngine;

public class FallingCube : MonoBehaviour
{
    public float fallSpeed = 10f; //make this random so Objects fall at different speeds
    //would be cool, if they would spin

    private PlayerController playerController;

    private Vector3 spinSpeed;

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

        spinSpeed = new Vector3(0, 90f, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        transform.Rotate(spinSpeed * Time.deltaTime);

        if (transform.position.y < -1f) // if object is below -1f(ground) destroy it
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if player catches the falling cube, destroy the cube
            Debug.Log("Player touched FallingCube. Destroying the cube.");
            Destroy(gameObject); //destrys falling cube

            // Call IncreaseScore function in PlayerController
            if (playerController != null)
            {
                playerController.IncreaseScore();
            }
        }

        if (other.CompareTag("Environment"))
        {
            //if player catches the falling cube, destroy the cube
            //Debug.Log("Player touched FallingCube. Destroying the cube.");
            Destroy(gameObject); //destrys falling cube
        }
    }
}
