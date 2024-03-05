using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calculates a direction vector from a given angle in degrees, relative to a specified forward direction and rotation plane axis.
    /// </summary>
    /// <param name="angleInDegrees">The angle in degrees for which the direction is calculated.</param>
    /// <param name="transformForward">The forward direction from which the angle is measured.</param>
    /// <param name="rotationPlaneAxis">The axis around which the rotation is applied. This also defines the plane in which the direction is calculated.</param>
    /// <returns>A normalized direction vector calculated from the given angle in degrees.</returns>
    public static Vector3 GetDirectionFromAngleInDegrees(float angleInDegrees, Vector3 transformForward, Vector3 rotationPlaneAxis)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad; // Convert angle from degrees to radians
        // Calculate the direction vector by rotating the forward vector by the given angle around the rotation plane axis
        return (transformForward * Mathf.Cos(angleInRadians) + rotationPlaneAxis * Mathf.Sin(angleInRadians)).normalized;
    }
}
