using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZones : MonoBehaviour
{
    [SerializeField]
    private Vector3 localGravity = new Vector3(0, -9.81f, 0); //setting of the gravity for specific zones
    private Collider gravityZone;
    [SerializeField] PlayerController player;

    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        gravityZone = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        ApplyCustomGravity();
    }

    void ApplyCustomGravity()
    {
        Collider[] colliders = Physics.OverlapBox(gravityZone.bounds.center, gravityZone.bounds.extents, Quaternion.identity); //gets all the colliders in the gravity zone

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>(); //if the colliders have rigid bodys it applys the gravity to them
            if (rb != null)
            {
                rb.AddForce(localGravity, ForceMode.Force);
            }
            if (collider.gameObject.CompareTag("Player")) //sets the players localGravity 
            {
                player.gravity = localGravity;
            }
         
        }
    }



}


