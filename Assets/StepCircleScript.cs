using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCircleScript : MonoBehaviour
{
    //Show a circle on the ground whenever/wherever the model touches the floor this script is attached to 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Model")) {
            
        }
    }
}
