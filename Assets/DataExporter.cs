using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataExporter : MonoBehaviour
{

    string OutputFilename; //Name of file to export to w/ date and time
    public string OutputDirectory; //Name of directory to export to
    StreamWriter writer; //Streamwriter to write to file with

    public GameObject[] objectsToTrack; //List of Gameobjects to track the position of
    public float timer; //Amount of time before point is captured

    // Start is called before the first frame update
    void Start()
    {
        //Init dir if not given
        OutputDirectory = Application.streamingAssetsPath + "/Data/";

        //Create CSV
        OutputFilename = "XR Position Data " + DateTime.Now.ToString("yyyy_dd_M HH mm") + ".csv";

        //Make directory if not present
        if (!Directory.Exists(OutputDirectory))
        {
            Directory.CreateDirectory(OutputDirectory);
        }

        //Init writer
        writer = new StreamWriter(File.Create(OutputDirectory + "/" + OutputFilename));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
