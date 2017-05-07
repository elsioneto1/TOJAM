using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Dice_6 : Dice
{


    
   

   
   
    bool forceApplied = false;
   
    [Header("Dice Inputs")]
    public DiceResult Face1;
    public DiceResult Face2;
    public DiceResult Face3;
    public DiceResult Face4;
    public DiceResult Face5;
    public DiceResult Face6;
    public Dictionary<string, DiceResult> results = new Dictionary<string, DiceResult>();


    // Use this for initialization
    public override void Start () {

        base.Start();
        results.Add("right", Face1);
        results.Add("right180", Face2);
        results.Add("up", Face3);
        results.Add("up180", Face4);
        results.Add("forward", Face5);
        results.Add("forward180", Face6);

        parsedVectors = new Vector3[4];
        
    }



	// Update is called once per frame
	public override void Update ()
    {
      //  Debug.Log(rBody.);
        base.Update();
        possetionDuration -= Time.deltaTime;

        if ( whosPossessed != null && possetionDuration < 0 && InputParser.GetUnpossession(whosPossessed.pControl.playerType) && POSSESSED)
        {
            OnEndPossesion();
        }
        if (whosPossessed != null && POSSESSED && !alreadyPossesed)
        {
           // Debug.Log(possetionDuration);
            input = new Vector3(InputParser.GetHorizontal(whosPossessed.pControl.playerType), 0, InputParser.GetVertical(whosPossessed.pControl.playerType));
            elapsedTime += Time.deltaTime;

            if (elapsedTime > .1f && input.magnitude > 0.3f)
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


                }
                
                float angle = Mathf.Atan2(chosenVector.z - Vector3.right.z, chosenVector.x - Vector3.right.x);


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
                //finalForceVector = MathOperations.RotateVectorX(finalForceVector, -30);
                // force = MathOperations.RotateVectorY(force,(-90 * Mathf.PI )/ 180) * forceX ;
                // force *= 0.5f;
            }
        }
    }


    


    public override void OnStartPossesion(PossesRadius player)
    {
        base.OnStartPossesion(player);
        possetionDuration = possesingTime;
        POSSESSED = true;
    }

    public override void OnEndPossesion()
    {
        base.OnEndPossesion();
    }

    public override DiceResult EvaluateResults()
    {
        
        base.EvaluateResults();
        DiceResult dr = DiceResult.Sword;

        float bestDot = Vector3.Dot(Vector3.up,-Vector3.up);

        string dicKey = "";
        if (Vector3.Dot(transform.right, Vector3.up) > bestDot)
        {
            dicKey = "right";
            bestDot = Vector3.Dot(transform.right, Vector3.up);
        }
        if (Vector3.Dot(-transform.right, Vector3.up) > bestDot)
        {
            dicKey = "right180";
            bestDot = Vector3.Dot(-transform.right, Vector3.up);
        }
        if (Vector3.Dot(transform.up, Vector3.up) > bestDot)
        {
            dicKey = "up";
            bestDot = Vector3.Dot(transform.up, Vector3.up);
        }
        if (Vector3.Dot(-transform.up, Vector3.up) > bestDot)
        {
            dicKey = "up180";
            bestDot = Vector3.Dot(-transform.up, Vector3.up);
        }
        if (Vector3.Dot(transform.forward, Vector3.up) > bestDot)
        {
            dicKey = "forward";
            bestDot = Vector3.Dot(transform.forward, Vector3.up);
        }
        if (Vector3.Dot(-transform.forward, Vector3.up) > bestDot)
        {
            dicKey = "forward180";
            bestDot = Vector3.Dot(-transform.forward, Vector3.up);
        }

        dr = results[dicKey];
        Debug.Log(dicKey);
        return dr;

    }

    public void FixedUpdate()
    {
        Debug.Log(finalForceVector);

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
        GUIStyle style = new GUIStyle();
        style.border = new RectOffset(0, 10, 0, 10);
        Handles.Label(transform.position + transform.right, Face1.ToString(), style);
        Handles.Label(transform.position - transform.right * 1.5f, Face2.ToString(), style);
        Handles.Label(transform.position + transform.up , Face3.ToString(), style);
        Handles.Label(transform.position - transform.up , Face4.ToString(), style);
        Handles.Label(transform.position + transform.forward * 1.5f, Face5.ToString(), style);
        Handles.Label(transform.position - transform.forward , Face6.ToString(), style);


    }

}
