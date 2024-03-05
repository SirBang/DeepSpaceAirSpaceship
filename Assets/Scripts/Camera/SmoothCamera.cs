using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target; // The target the camera should follow (your airplane)
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target. Adjust as needed.

    void LateUpdate()
    {
        if (target == null)
        {
            FindAirplane();
            return;
        }

        // Calculate the new position based on the offset and the target's orientation
        Vector3 newPosition = target.position + target.forward * offset.z + target.up * offset.y + target.right * offset.x;
        transform.position = newPosition;

        // Make the camera look at the target
        transform.LookAt(target);
    }

    void FindAirplane()
    {
        // Attempt to find the airplane by tag if not manually assigned
        GameObject airplane = GameObject.FindGameObjectWithTag("Player");
        if (airplane != null)
        {
            target = airplane.transform;
        }
    }
}