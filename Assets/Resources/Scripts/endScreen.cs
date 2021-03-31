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
            endText.text = "You've destroyed all the pins and beaten all the levels! You Win! You are the master of Breakdown Bowling!";
            gameManager.level = 1;
        }
        else if (gameManager.winGame == false)
        {
            endText.text = loseText;
        }
    }
    public void replayGame()
    {
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
