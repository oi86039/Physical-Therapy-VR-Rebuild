using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRInput.Controller controller; //Which hand is this attached to?

    public GameObject model; //Model to move and rotate
    public float rotateSpeed;

    //Uses laser pointer script, credit to @Moaid_T4

    public LineRenderer laserLineRenderer;
    public Color color;
    public Color GrabColor;
    public float laserWidth = 0.1f;
    public float laserLength = 0f;
    public float laserMaxLength = 20f;

    private Vector3 start_model_transform; //Position where model starts in scene

    private Vector3 orig_model_transform; //Set whenever a grab is initiated
    private Vector3 orig_controller_transform; //Set whenever a grab is initiated
    public bool origSet; //Was the original transform already set? If it is, don't set orig_transform anymore
    public float xMoveMultiplier;
    public float zMoveMultiplier;

    public Outline outline;
    public bool canGrab;
    public bool isGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        //Set model start position
        start_model_transform = model.transform.position;

        //Add outline to model
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 4f;

        outline.enabled = false;

        // Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        //laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
    }

    // Update is called once per frame
    void Update()
    {
        //On point, raycast, have laser pointer, and be able to grab
        if (!OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, controller) && !isGrabbing)
        {
            laserLineRenderer.enabled = true;
            laserLineRenderer.startColor = color;
            laserLineRenderer.endColor = color;
            laserLength = Mathf.Lerp(laserLength, laserMaxLength, 0.08f);
            ShootLaserFromTargetPosition(transform.position, transform.forward, laserLength);
        }
        //If not pointing or are grabbing something, disable ability to grab
        else if (isGrabbing)
        {
            //laserLength = Mathf.Lerp(laserLength, 0, -0.08f);
            laserLineRenderer.startColor = GrabColor;
            laserLineRenderer.endColor = GrabColor;
            ShootLaserToModel(transform.position);
            canGrab = false;
        }

        //If not pointing or grabbing, disable laser and ability to grab
        else {
            laserLineRenderer.startColor = laserLineRenderer.endColor = color;
            laserLineRenderer.enabled = false;
            canGrab = false;
        }

        //If we are able to grab, or are currently grabbing something and we hold the trigger, Grab and move the model
        if ((canGrab || isGrabbing) && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.5f)
        {
            ShootLaserToModel(transform.position);
            GrabModel();
        }
        //If not grabbing or cannot grab
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) < 0.5f)
        {
            isGrabbing = false;
            origSet = false;
        }

    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        //If raycast hit something
        if (Physics.Raycast(ray, out raycastHit, length))
        {
            //Limit laser length
            endPosition = raycastHit.point;
            if (raycastHit.collider.gameObject.CompareTag("Model"))
            {
                outline.enabled = true;
                {
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 4f;
                    canGrab = true;
                }
            }
        }
        //If not raycast hit
        else
        {
            if (!isGrabbing) //Only disable outline if not grabbing
            {
                outline.enabled = false;
            }
            canGrab = false;
        }
        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }

    //Shoot laser to specified end point
    void ShootLaserToModel(Vector3 targetPosition)
    {
        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, model.transform.position + new Vector3(0,1.4f,0));
    }


    void GrabModel()
    {
        isGrabbing = true;
        canGrab = false;

        //Change outline color
        outline.enabled = true;
        outline.OutlineColor = new Color(1.00f, 0.65f, 0.00f); //Orange
        outline.OutlineWidth = 8f;
        canGrab = true;

        //Set starting transform of everything
        if (!origSet)
        {
            orig_model_transform = model.transform.position;
            orig_controller_transform = transform.position;
            origSet = true;
        }
        //Get distance and move with motion
        Vector3 distance = orig_controller_transform - transform.position;
        model.transform.position = orig_model_transform - new Vector3((distance.x) * xMoveMultiplier, 0, (distance.y - distance.z) * zMoveMultiplier);

        //Handle Joystick input and rotate with joystick
        Vector2 move = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        move = new Vector2(Mathf.Clamp(move.x, -1.0f, 1.0f), Mathf.Clamp(move.y, -1.0f, 1.0f));
        model.transform.Rotate(model.transform.up * -move.x * rotateSpeed);
    }

}
