using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class antiPinCode : MonoBehaviour
{
    
    public float pinHealth = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // if touch borders destroy it
        if (collision.gameObject.tag == "border")
        {
            RemoveAntiPin();
        }
        if (collision.relativeVelocity.magnitude > 2)
        {

            pinHealth -= gameManager.powerRating * collision.relativeVelocity.magnitude;

        } 

        if (pinHealth <= 0)
        {
            RemoveAntiPin();
        }
    }
    void RemoveAntiPin()
    {
        Destroy(this.gameObject);
        gameManager.winGame = false;
        endScreen.loseText = "You destroyed a purple pin! You lose!";
        SceneManager.LoadSceneAsync("Resources/Scenes/End", LoadSceneMode.Single);
    }
}
