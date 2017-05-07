using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsABouncer : MonoBehaviour {

    IsTheBoard board;
    public static bool ROLLING = false;


    [Header("Chão")]
    public float reflectionForceFloor = 900;
    public float reflectionForceRotationFloor = 200;
  
    public float forceIncrementerFloor = 200;
    public float forceIncrementerRotationFloor = 100;



    [Header("Parede")]
    public float reflectionForceWall = 900;
    public float reflectionForceRotationWall = 600;

    public float forceIncrementerPreservationWall = 320;
    public float rotationIncrementerFloorWall = 200;
    // Use this for initialization
    void Start() {
        board = GetComponent<IsTheBoard>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnCollisionEnter(Collision collision)
    {
      
        Dice d = collision.gameObject.GetComponent<Dice>();
        if (d.POSSESSED || d.alreadyPossesed || !ROLLING)
            return;
        
        if ( collision.gameObject.GetComponent<Dice>() != null)
        {


            if (board != null)
            {
                Vector3 reflection = d.rBody.velocity - 2 * Vector3.Dot(d.rBody.velocity, transform.forward) * -transform.forward;
                d.rBody.velocity = Vector3.zero;
                Vector3 force = reflection + (Vector3.up * reflectionForceFloor);
                Vector3 forceTorque = reflection + (Vector3.up * reflectionForceRotationFloor);
                force.x *= forceIncrementerFloor;
                force.z *= forceIncrementerFloor;
                forceTorque.x *= forceIncrementerRotationFloor;
                forceTorque.z *= forceIncrementerRotationFloor;


                d.rBody.AddForce(force);
                d.rBody.AddRelativeTorque(forceTorque, ForceMode.Impulse);

                
            }
            else
            {

                Vector3 reflection = d.rBody.velocity - 2 * Vector3.Dot(d.rBody.velocity, transform.forward) * transform.forward;
                d.rBody.velocity = Vector3.zero;
                Vector3 force = reflection + (Vector3.up * reflectionForceWall);
                Vector3 forceTorque = reflection + (Vector3.up * reflectionForceRotationWall);
                force.x *= forceIncrementerPreservationWall;
                force.z *= forceIncrementerPreservationWall;
                forceTorque.x *= rotationIncrementerFloorWall;
                forceTorque.z *= rotationIncrementerFloorWall;


                d.rBody.AddForce(force);
                d.rBody.AddRelativeTorque(forceTorque, ForceMode.Impulse);
            }
        }
    }
}
