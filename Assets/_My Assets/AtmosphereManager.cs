using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereManager : MonoBehaviour
{

    public int num; //Number to decide which atmosphere to take

    Material skybox;
    //public Quaternion sunRotation;
    //  public bool sunOn; //Is the sun active or not?
    // public Transform sun;
    public AudioClip music;
    AudioSource audioSource;


    void Awake()
    {
        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Generate random number and pick properties based on it
        if (!gameManager.Instance.debugMode)
            num = Random.Range(1, 5);

        switch (num)
        {
            case 1: //Daybreak
                skybox = Resources.Load<Material>("CloudyCrown_Daybreak");
                // sun.gameObject.SetActive(false);

                break;
            case 2: //Midday
                skybox = Resources.Load<Material>("CloudyCrown_Midday");
                music = Resources.Load<AudioClip>("After_All");
                audioSource.volume = 0.06f;
                audioSource.pitch = 1;
                break;
            case 3: //Sunset
                skybox = Resources.Load<Material>("CloudyCrown_Sunset");
                // sun.gameObject.SetActive(false);
                //Affect light source too
                music = Resources.Load<AudioClip>("After_All");
                audioSource.volume = 0.186f;
                audioSource.pitch = 0.5f;
                break;
            case 4: //Evening
                skybox = Resources.Load<Material>("CloudyCrown_Evening");
                // sun.gameObject.SetActive(false);
                break;
            case 5: //Midnight
                skybox = Resources.Load<Material>("CloudyCrown_Midnight");
                // sun.gameObject.SetActive(false);
                music = Resources.Load<AudioClip>("Eddy");
                audioSource.volume = 0.186f;
                audioSource.pitch = 1;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Change skybox material
        RenderSettings.skybox = skybox;
        DynamicGI.UpdateEnvironment();

        //Assign music and play
        audioSource.clip = music;
        if (audioSource.clip)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("No song assigned to AtmosphereManager...");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //sun.rotation = sunRotation;
    }
}
