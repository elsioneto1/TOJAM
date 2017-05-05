using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_6 : Dice {


    public Vector3 forceRight;
    public Vector3 forceForward;
    public Vector3 finalForceVector;
    Rigidbody rBody;
    float forceX = 10;
    float forceY = 10;

    public float appliedForce = 10;
    Vector3[] vectors = new Vector3[6];
    Vector3[] parsedVectors = new Vector3[4];

    bool forceApplied = false;
    bool POSSESSED = true;
    Vector3 input = Vector3.zero;
    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody>();
	}


    float elapsedTime;

	// Update is called once per frame
	void Update ()
    {
        if (POSSESSED)
        {

            input = new Vector3(InputParser.GetHorizontal(), 0, InputParser.GetVertical());
            elapsedTime += Time.deltaTime;

            if (elapsedTime > .2f && input.magnitude > 0.3f)
            {
                elapsedTime = 0;
                vectors[0] = transform.right;
                vectors[1] = -transform.right;
                vectors[2] = transform.up;
                vectors[3] = -transform.up;
                vectors[4] = transform.forward;
                vectors[5] = -transform.forward;


                // it'll be our right vector and then we'll rotate to it
                Vector3 chosenVector = vectors[0];
                int chosen = -1;
                for (int i = 0; i < vectors.Length; i++)
                {
                    // FUCK Y
                    vectors[i].y = 0;
                    vectors[i].Normalize();

                    if (Vector3.Dot(Vector3.right, vectors[i]) > Vector3.Dot(Vector3.right, chosenVector))
                    {
                        chosen = i;
                        chosenVector = vectors[i];
                    }
                    //  Debug.DrawRay(transform.position,vectors[i],Color.red);


                }
                Debug.Log(chosen);
                Debug.DrawRay(transform.position, Vector3.right, Color.green);
                Debug.DrawRay(transform.position, chosenVector, Color.red);
                float angle = Mathf.Atan2(chosenVector.z - Vector3.right.z, chosenVector.x - Vector3.right.x);
                //  Debug.Log(angle);

                parsedVectors[0] = forceRight * forceX;
                parsedVectors[1] = MathOperations.RotateVectorY(chosenVector, (-90 * Mathf.PI) / 180) * forceY;
                forceRight = chosenVector * forceX;
                forceForward = MathOperations.RotateVectorY(chosenVector, (-90 * Mathf.PI) / 180) * forceY;
                parsedVectors[2]= -forceRight;
                parsedVectors[3] = -forceForward;

                finalForceVector = parsedVectors[0];
                for (int i = 0; i < parsedVectors.Length; i++)
                {
                    if ( Vector3.Dot(parsedVectors[i].normalized, input ) > Vector3.Dot(finalForceVector.normalized, input))
                    {
                        finalForceVector = parsedVectors[i];
                    }
                }
                finalForceVector = finalForceVector.normalized * appliedForce;
                // force = MathOperations.RotateVectorY(force,(-90 * Mathf.PI )/ 180) * forceX ;
                // force *= 0.5f;
            }
        }
    }

    public void FixedUpdate()
    {

        rBody.AddForce(finalForceVector);
        // reset after the force is Applied
        finalForceVector = Vector3.zero;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, forceRight, Color.blue);
        Debug.DrawRay(transform.position, forceForward, Color.blue);
        Debug.DrawRay(transform.position, input * 3, Color.red);
        Debug.DrawRay(transform.position, finalForceVector, Color.green);

    }

}
