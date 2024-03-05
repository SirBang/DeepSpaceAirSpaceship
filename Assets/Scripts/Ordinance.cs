using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordinance : MonoBehaviour
{
    // Public fields for ordinance properties
    public string ordinanceName = "Laser"; // The name of the ordinance
    public float muzzleVelocity = 300f; // The speed at which the ordinance is fired
    public float armorDamage = 100f; // The damage dealt to armor upon impact
    public float shieldDamage = 50f; // The damage dealt to shields upon impact
    public GameObject explosion; // The explosion effect prefab to instantiate upon impact

    // Called when the ordinance collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        Explode(); // Trigger the explosion effect
    }

    // Handles the explosion effect and destruction of the ordinance object
    private void Explode()
    {
        // Instantiate the explosion effect at the ordinance's current position and with no rotation
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Destroy the ordinance object after a short delay to allow the explosion effect to initiate
        Destroy(gameObject, .05f);
    }
}
