using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsABouncer : MonoBehaviour {

    IsTheBoard board;
    public static bool ROLLING = false;
	// Use this for initialization
	void Start () {
        board = GetComponent<IsTheBoard>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        Dice d = collision.gameObject.GetComponent<Dice>();
        if (d.POSSESSED || !ROLLING)
            return;

        if ( collision.gameObject.GetComponent<Dice>() != null)
        {

            
            if (board != null)
            {

                Vector3 reflection = d.rBody.velocity - 2 * Vector3.Dot(d.rBody.velocity, transform.forward) * -transform.forward;
                d.rBody.velocity = Vector3.zero;
                Vector3 force = reflection + (Vector3.up * 900);
                Vector3 forceTorque = reflection + (Vector3.up * 200);
                force.x *= 200;
                force.z *= 200;
                forceTorque.x *= 100;
                forceTorque.z *= 100;
                Debug.Log(force);

                d.rBody.AddForce(force);
                d.rBody.AddRelativeTorque(forceTorque, ForceMode.Impulse);

                
            }
            else
            {
                Debug.Log(name);
                Vector3 reflection = d.rBody.velocity - 2 * Vector3.Dot(d.rBody.velocity, transform.forward) * transform.forward;
                d.rBody.velocity = Vector3.zero;
                Vector3 force = reflection + (Vector3.up * 900);
                Vector3 forceTorque = reflection + (Vector3.up * 600);
                force.x *= 320;
                force.z *= 320;
                forceTorque.x *= 200;
                forceTorque.z *= 200;
                Debug.Log(force);

                d.rBody.AddForce(force);
                d.rBody.AddRelativeTorque(forceTorque, ForceMode.Impulse);
            }
        }
    }
}
