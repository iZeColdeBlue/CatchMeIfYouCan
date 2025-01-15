using UnityEngine;

public class FallingCube : MonoBehaviour
{
    private float fallSpeed = 3f; //make this random so Objects fall at different speeds
    //would be cool, if they would spin


    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -1f) // if object is below -1f(ground) destroy it
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
{
    if (CompareTag("FallingCube"))
    {
        //if player catches the falling cube, destroy the cube
        Debug.Log("Player touched FallingCube. Destroying the cube.");
        Destroy(gameObject); //destrys falling cube
    }

}
   
}
