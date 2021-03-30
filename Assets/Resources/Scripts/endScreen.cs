using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endScreen : MonoBehaviour
{
    public Text endText;
    public Button replayButton;
    static public string loseText = "You did not destroy all the pins, you lose!";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.winGame == true)
        {
            endText.text = "All pins are down! You Win!";
        }
        else if (gameManager.winGame == false)
        {
            endText.text = loseText;
        }
    }
    public void replayGame()
    {
        gameManager.pinsLeft = 8;
        gameManager.heavyBalls = 3;
        gameManager.midBalls = 4;
        gameManager.lightBalls = 3;
        switch (gameManager.level)
        {
            case 1:
                SceneManager.LoadSceneAsync("Resources/Scenes/Level1", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadSceneAsync("Resources/Scenes/Level2", LoadSceneMode.Single);
                break;
            case 3:
                SceneManager.LoadSceneAsync("Resources/Scenes/Level3", LoadSceneMode.Single);
                break;
            case 4:
                SceneManager.LoadSceneAsync("Resources/Scenes/Level4", LoadSceneMode.Single);
                break;
            default:
                break;
        }

    }
}
