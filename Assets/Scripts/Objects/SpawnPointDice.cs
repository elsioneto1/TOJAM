using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointDice : MonoBehaviour {

    public Dice holdingDice = null;

 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( holdingDice != null)
        {
            
            holdingDice.transform.position = transform.position;
        }
	}


}
