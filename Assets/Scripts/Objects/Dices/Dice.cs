using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    protected float possesingTime = 0.2f;
    protected bool POSSESSED = false;
    public float appliedForce = 10;
    protected Vector3 forceRight;
    protected Vector3 forceForward;
    protected Vector3 finalForceVector;

    [HideInInspector]
    public PossesRadius whosPossessed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnStartPossesion(PossesRadius player)
    {
        whosPossessed = player;
    }

    public virtual void OnEndPossesion()
    {

    }


}

