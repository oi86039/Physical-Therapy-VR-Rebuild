using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public bool debugMode; //Will alter some functions depending if true or false.

    public enum state { PLAY, PAUSED };

    public state gameState;

    private static gameManager instance;

    public static gameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<gameManager>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = state.PLAY;
    }

    // Update is called once per frame
    void Update() {

        if (OVRInput.GetDown(OVRInput.Button.Start)) {
            SceneManager.LoadScene(0);
        }
    }

    //Unpause the game
    void Play()
    {
        //Change State
        if (gameState == state.PAUSED)
        {
            gameState = state.PLAY;
            Debug.Log("PLAY");
        }

        //Set pause menu to inactive

        //Unpause all timers
        
        //Unpause all animations.
    }

    //Pause the game
    void Pause()
    {
        if (gameState == state.PLAY)
        {
            gameState = state.PLAY;
            Debug.Log("PAUSE");
        }

        //Set pause menu to active

        //Pause all timers

        //Pause all animations.
    }

}
