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

            pinHealth -= gameManager.powerRating * collision.relativeVelocity.magnitude;

        } 

        if (pinHealth <= 0)
        {
            RemovePin();
        }
    }
    void RemovePin()
    {
        Destroy(this.gameObject);
        gameManager.pinsLeft--;
        if (gameManager.pinsLeft <= 0)
        {
                switch (gameManager.level)
                {
                    case 1:
                        SceneManager.LoadSceneAsync("Resources/Scenes/Level2", LoadSceneMode.Single);
                        break;
                    case 2:
                        SceneManager.LoadSceneAsync("Resources/Scenes/Level3", LoadSceneMode.Single);
                        break;
                    case 3:
                        SceneManager.LoadSceneAsync("Resources/Scenes/Level4", LoadSceneMode.Single);
                        break;
                    case 4:
                        gameManager.winGame = true;
                        SceneManager.LoadSceneAsync("Resources/Scenes/End", LoadSceneMode.Single);
                        break;
                    default:
                        break;
                }
        }
    }
}
