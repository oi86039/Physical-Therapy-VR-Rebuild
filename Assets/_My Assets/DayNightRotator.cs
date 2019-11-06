using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightRotator : MonoBehaviour
{

    public float sunSpeed;
    public float skyBoxSpeed;

    // Update is called once per frame
    void Update()
    {
        //Rotate Sun
        transform.Rotate(new Vector3(1,0,0), sunSpeed);

        //Rotate Skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);
    }
}
