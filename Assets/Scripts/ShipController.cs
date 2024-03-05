using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Interface for handling input from the player or AI.
    IControllerInput playerInput;

    // The GameObject that acts as the pilot, providing input to the ship.
    public GameObject pilot;

    // Adjustable power levels for different types of movement.
    public float forwardThrustPower = 10f;
    public float yawSpeed = 10f;
    public float pitchSpeed = 10f;
    public float rollSpeed = 10f;

    // The maximum speed the ship can reach.
    public float maxVelocity = 500f;

    // Rigidbody component for physics calculations.
    Rigidbody myRigidbody;

    // Original drag value for the Rigidbody, used for toggling sliding effect.
    float originalDrag;

    private void Awake()
    {
        // Ensure there is a pilot assigned, defaulting to self if not.
        if (!pilot) pilot = gameObject;

        // Setup input handling if a pilot is present.
        if (pilot)
        {
            playerInput = pilot.GetComponent<IControllerInput>();
            // Subscribe to input events with corresponding movement methods.
            playerInput.ForwardEvent += ForwardThrust;
            playerInput.YawEvent += YawMovement;
            playerInput.PitchEvent += PitchMovement;
            playerInput.RollEvent += RollMovement;
            playerInput.VerticalStrafeEvent += VerticalStrafeMovement;
            playerInput.SideStrafeEvent += SideStrafeMovement;
            playerInput.SlideEvent += EnableSlide;
            playerInput.TurnEvent += TurnToTarget;
        }
        else
        {
            Debug.LogError("No pilot on", gameObject);
        }
    }

    void Start()
    {
        // Initialize Rigidbody and store original drag value.
        myRigidbody = GetComponent<Rigidbody>();
        originalDrag = myRigidbody.drag;
    }

    private void TurnToTarget(float x, float y, float z)
    {
        // Calculate desired heading based on input.
        Vector3 desiredHeading = new Vector3(x, y, z);

        // Determine the rotation needed to face the desired heading.
        Quaternion rotationGoal = Quaternion.LookRotation(desiredHeading);

        // Calculate rotation step based on yaw speed and frame time.
        float step = yawSpeed * Time.deltaTime;

        // Rotate towards the desired heading.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationGoal, step);
    }

    private void EnableSlide(float slide)
    {
        // Toggle drag on the Rigidbody to enable or disable sliding effect.
        if (slide > 0) myRigidbody.drag = 0; else myRigidbody.drag = originalDrag;
    }

    public void ForwardThrust(float thrust)
    {
        // Apply forward force based on input, adjusted by thrust power and frame time.
        myRigidbody.AddForce(gameObject.transform.forward * thrust * forwardThrustPower * Time.deltaTime);

        // Limit velocity to the maximum specified speed.
        if(myRigidbody.velocity.magnitude > maxVelocity)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxVelocity;
        }
    }

    public void SideStrafeMovement(float thrust)
    {
        // Apply side force for strafing, adjusted by thrust power and frame time.
        myRigidbody.AddForce(gameObject.transform.right * thrust * forwardThrustPower * Time.deltaTime);
    }

    public void VerticalStrafeMovement(float thrust)
    {
        // Apply upward/downward force for vertical strafing, adjusted by thrust power and frame time.
        myRigidbody.AddForce(gameObject.transform.up * thrust * forwardThrustPower * Time.deltaTime);
    }

    public void YawMovement(float yaw)
    {
        // Apply torque for yaw rotation, adjusted by yaw speed and frame time.
        myRigidbody.AddTorque(gameObject.transform.up * yaw * yawSpeed * Time.deltaTime);
    }

    public void PitchMovement(float pitch)
    {
        // Apply torque for pitch rotation, adjusted by pitch speed and frame time.
        myRigidbody.AddTorque(gameObject.transform.right * pitch  * pitchSpeed * Time.deltaTime);
    }

    public void RollMovement(float roll)
    {
        // Apply torque for roll rotation, adjusted by roll speed and frame time.
        myRigidbody.AddTorque(gameObject.transform.forward * roll * rollSpeed * Time.deltaTime);
    }
}