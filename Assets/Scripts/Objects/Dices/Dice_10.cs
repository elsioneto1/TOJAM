using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Dice_10 : Dice {

    // Use this for initialization
    
    public GameObject[] angleReferences;

    [Header("Dice Inputs")]

    public DiceResult Face1;
    public DiceResult Face2;
    public DiceResult Face3;
    public DiceResult Face4;
    public DiceResult Face5;
    public DiceResult Face6;
    public DiceResult Face7;
    public DiceResult Face8;
    public DiceResult Face9;
    public DiceResult Face10;

    public Dictionary<GameObject, DiceResult> results = new Dictionary<GameObject, DiceResult>();


    public override void Start () {
        
        base.Start();
        parsedVectors = new Vector3[4];
        // hard coded BAGARAIO
        results.Add(angleReferences[0], Face1);
        results.Add(angleReferences[1], Face2);
        results.Add(angleReferences[2], Face3);
        results.Add(angleReferences[3], Face4);
        results.Add(angleReferences[4], Face5);
        results.Add(angleReferences[5], Face6);
        results.Add(angleReferences[6], Face7);
        results.Add(angleReferences[7], Face8);
        results.Add(angleReferences[8], Face9);
        results.Add(angleReferences[9], Face10);

    }
    private void OnDrawGizmos()
    {
        
        if (results.Count == 0)
        {
            results.Add(angleReferences[0], Face1);
            results.Add(angleReferences[1], Face2);
            results.Add(angleReferences[2], Face3);
            results.Add(angleReferences[3], Face4);
            results.Add(angleReferences[4], Face5);
            results.Add(angleReferences[5], Face6);
            results.Add(angleReferences[6], Face7);
            results.Add(angleReferences[7], Face8);
            results.Add(angleReferences[8], Face9);
            results.Add(angleReferences[9], Face10);
        }
        // v = MathOperations.RotateVectorY(v, (angleDebugY * Mathf.PI) / 180);
        for (int i = 0; i < angleReferences.Length; i++)
        {
            Debug.DrawRay(angleReferences[i].transform.position, angleReferences[i].transform.forward * 3, Color.blue);


            GUIStyle style = new GUIStyle();
            style.border = new RectOffset(0, 10, 0, 10);

            Handles.Label(transform.position + angleReferences[i].transform.forward * 1.8f, results[angleReferences[i]].ToString(), style);

        }



    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        possetionDuration -= Time.deltaTime;
        if (whosPossessed != null && possetionDuration < 0 && InputParser.GetUnpossession(whosPossessed.pControl.playerType) && POSSESSED)
        {
            OnEndPossesion();
        }
        if (whosPossessed != null && POSSESSED && ! alreadyPossesed)
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
                parsedVectors[2] = -forceRight;
                parsedVectors[3] = -forceForward;

                finalForceVector = parsedVectors[0];
                for (int i = 0; i < parsedVectors.Length; i++)
                {
                    if (Vector3.Dot(parsedVectors[i].normalized, input) > Vector3.Dot(finalForceVector.normalized, input))
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
        if (rBody != null)
        {
            rBody.AddForce(finalForceVector);
            // reset after the force is Applied
            finalForceVector = Vector3.zero;
        }
    }


      public override DiceResult EvaluateResults()
    {
        base.EvaluateResults();
        DiceResult dr = DiceResult.Sword;

        float bestDot = Vector3.Dot(Vector3.up, angleReferences[0].transform.forward);

        GameObject dicKey = angleReferences[0];
        for (int i = 0; i < angleReferences.Length; i++)
        {
            if ( Vector3.Dot(angleReferences[i].transform.forward, Vector3.up) > bestDot)
            {
                bestDot = Vector3.Dot(angleReferences[i].transform.forward, Vector3.up);
                dicKey = angleReferences[i];
            }
        }
        dr = results[dicKey];

      
        return dr;

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

}
