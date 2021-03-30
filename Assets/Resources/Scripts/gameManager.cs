using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    static public bool winGame = false;
    public Camera mainCam;
    public Camera sideCam;
    public Camera backCam;
    public GameObject sideWall;

    static public int pinsLeft = 8;
    public Text pinsLeftText;
    public Text infoText;
    static public float powerRating;
    public Slider powerSlider;
    public Text throwingPowerText;
    public Button throwButton;
    public Image reticle;
    public Image selectedBall;

    // true = up, false = down
    bool powerBarClimb = true;

    static public int lightBalls = 3;
    static public int midBalls = 4;
    static public int heavyBalls = 3;
    public Text lightBallsText;
    public Text midBallsText;
    public Text heavyBallsText;
    public Button lightButton;
    public Button midButton;
    public Button heavyButton;
    public Button changeCameraButton;
    public Text clickInfoText;
    public Text controlInfoText;

    public GameObject lightBallObject;
    public GameObject midBallObject;
    public GameObject heavyBallObject;
    public GameObject[] ballsList;


    // camera angles
    float yaw = 0.0f;
    float pitch = 0.0f;

    // what ball is equipped
    static int whichBall = 0;

    // last ball thrown
    bool lastBallThrown = false;

    // how many balls on the field
    GameObject[] ballsOnField;

    // which level we are on
    static public int level = 1;

    // start box clicked?
    public GameObject startBox;
    public bool startButtonClicked = false;


    // Start is called before the first frame update
    void Start()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        string thisSceneName = thisScene.name;
        switch (thisSceneName)
        {
            case "Level1":
                level = 1;
                lightBalls = 3;
                midBalls = 4;
                heavyBalls = 3;
                break;
            case "Level2":
                level = 2;
                lightBalls = 1;
                midBalls = 2;
                heavyBalls = 2;
                break;
            case "Level3":
                level = 3;
                lightBalls = 2;
                midBalls = 3;
                heavyBalls = 4;
                break;
            case "Level4":
                level = 4;
                lightBalls = 1;
                midBalls = 2;
                heavyBalls = 3;
                break;
            default:
                break;
        }
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pin");
        pinsLeft = pins.Length;
        sideCam.enabled = true;
        mainCam.enabled = false;
        backCam.enabled = false;
        sideWall.GetComponent<Renderer>().enabled = false;
        powerSlider.gameObject.SetActive(true);
        throwButton.interactable = false;
        infoText.text = "Sideview (change to backview)";
        reticle.enabled = false;
        lastBallThrown = false;
        ballsList = new GameObject[3];
        ballsList[0] = lightBallObject;
        ballsList[1] = midBallObject;
        ballsList[2] = heavyBallObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // find all balls
        ballsOnField = GameObject.FindGameObjectsWithTag("ball");

        // find out number of pins left and if 0, go to next level
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pin");
        pinsLeft = pins.Length;
        if (pinsLeft <= 0)
        {
            switch (level)
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

        // display UI

        pinsLeftText.text = "Pins Left: " + pinsLeft;
        pinsLeftText.transform.position = new Vector3(Screen.width / 7, Screen.height - Screen.height / 12, 0);
        if (powerBarClimb)
        {
            powerRating = powerRating + 0.03f;
            if (powerRating >= 1)
            {
                powerBarClimb = false;
            }
        }
        if (powerBarClimb == false)
        {
            powerRating = powerRating - 0.03f;
            if (powerRating <= 0)
            {
                powerBarClimb = true;
            }
        }
        powerSlider.value = powerRating;
        powerSlider.transform.position = new Vector3(Screen.width - Screen.width / 8, Screen.height / 4, 0);
        throwButton.transform.position = new Vector3(Screen.width - Screen.width / 3, Screen.height / 5, 0);
        infoText.transform.position = new Vector3(Screen.width - Screen.width / 3.5f, Screen.height - Screen.height / 7, 0);
        clickInfoText.transform.position = new Vector3(Screen.width / 4, Screen.height / 50, 0);
        changeCameraButton.transform.position = new Vector3(Screen.width - Screen.width / 8, Screen.height / 10, 0);
        // amount of balls
        lightBallsText.text = "Light x" + lightBalls.ToString();
        lightBallsText.transform.position = new Vector3(Screen.width / 10, Screen.height / 10, 0);
        midBallsText.text = "Middle x" + midBalls.ToString();
        midBallsText.transform.position = new Vector3(Screen.width / 3.3f, Screen.height / 10, 0);
        heavyBallsText.text = "Heavy x" + heavyBalls.ToString();
        heavyBallsText.transform.position = new Vector3(Screen.width / 2.05f, Screen.height / 10, 0);

        // WASD to move camera

        if (sideCam.enabled == false)
        {
            if (Input.GetKey("w"))
            {
                pitch -= 1;
            }
            if (Input.GetKey("a"))
            {
                yaw -= 1;
            }
            if (Input.GetKey("s"))
            {
                pitch += 1;
            }
            if (Input.GetKey("d"))
            {
                yaw += 1;
            }
        }
        yaw = Mathf.Clamp(yaw, -50f, 50f);
        pitch = Mathf.Clamp(pitch, -30f, 30f);
        mainCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // enable or disable ball selection button based on if we have any
        
        if (lightBalls > 0)
        {
            lightButton.interactable = true;
        } else
        {
            lightButton.interactable = false;
        }
        if (midBalls > 0)
        {
            midButton.interactable = true;
        }
        else
        {
            midButton.interactable = false;
        }
        if (heavyBalls > 0)
        {
            heavyButton.interactable = true;
        }
        else
        {
            heavyButton.interactable = false;
        }

        // display which ball is selected

        switch (whichBall)
        {
            case 0:
                selectedBall.transform.position = new Vector3(Screen.width / 10, Screen.height / 10, 0);
                break;
            case 1:
                selectedBall.transform.position = new Vector3(Screen.width / 3.3f, Screen.height / 10, 0);
                break;
            case 2:
                selectedBall.transform.position = new Vector3(Screen.width / 2.05f, Screen.height / 10, 0);
                break;
        }

        // check if run out of balls, then lose

        if (lightBalls <= 0 && midBalls <= 0 && heavyBalls <= 0)
        {
            lastBallThrown = true;
        }
        if (lastBallThrown == true)
        {

            if (ballsOnField.Length <= 0)
            {
                winGame = false;
                endScreen.loseText = "You did not destroy all the pins, you lose!";
                SceneManager.LoadSceneAsync("Resources/Scenes/End", LoadSceneMode.Single);
            }
        }
    }
    public void changeCamera()
    {
        if (startButtonClicked == true)
        {
            if (sideCam.enabled == true)
            {
                sideCam.enabled = false;
                backCam.enabled = true;
                sideWall.GetComponent<Renderer>().enabled = true;
                infoText.text = "Backview (change camera to throw)";
            }
            else if (sideCam.enabled == false && backCam.enabled == true)
            {
                backCam.enabled = false;
                mainCam.enabled = true;
                throwButton.interactable = true;
                infoText.text = "Use WASD to control Camera";
                reticle.enabled = true;
            }
            else if (sideCam.enabled == false && mainCam.enabled == true)
            {
                sideCam.enabled = true;
                mainCam.enabled = false;
                throwButton.interactable = false;
                infoText.text = "Sideview (change to backview)";
                reticle.enabled = false;
                sideWall.GetComponent<Renderer>().enabled = false;
            }
        }
    }
    public void chooseBall(int chosen)
    {
        if (startButtonClicked == true)
        {
            whichBall = chosen;
        }
    }
    public void throwBall()
    {
        if (startButtonClicked == true)
        {
            // Instantiate
            Vector3 cameraPosition = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
            switch (whichBall)
            {
                case 0:
                    if (lightBalls > 0)
                    {
                        lightBalls--;
                        GameObject newBall = Instantiate(ballsList[whichBall], cameraPosition, Quaternion.identity);
                    }
                    break;
                case 1:
                    if (midBalls > 0)
                    {
                        midBalls--;
                        GameObject newBall = Instantiate(ballsList[whichBall], cameraPosition, Quaternion.identity);
                    }
                    break;
                case 2:
                    if (heavyBalls > 0)
                    {
                        heavyBalls--;
                        GameObject newBall = Instantiate(ballsList[whichBall], cameraPosition, Quaternion.identity);
                    }
                    break;
            }
        }
    }
    public void startLevel()
    {
        startButtonClicked = true;
        Destroy(startBox);
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
