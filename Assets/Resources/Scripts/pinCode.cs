using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pinCode : MonoBehaviour
{
    
    public float pinHealth = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if it rotated too much, destroy it
        if (this.transform.rotation.x >= 60 || this.transform.rotation.y >= 60 || this.transform.rotation.z >= 60)
        {
            Debug.Log("rotate");
            RemovePin();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // if touch borders destroy it
        if (collision.gameObject.tag == "border")
        {
            RemovePin();
        }
        if (collision.relativeVelocity.magnitude > 2)
        {

            pinHealth -= (gameManager.powerRating * collision.relativeVelocity.magnitude * 2);

        } 

        if (pinHealth <= 0)
        {
            RemovePin();
        }
    }
    void RemovePin()
    {
        Destroy(this.gameObject);
    }
}
