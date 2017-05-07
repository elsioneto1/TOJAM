using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine( "Wobble" );
	}
	
    IEnumerator Wobble()
    {
        while (true)
        {
           
            iTween.PunchRotation( gameObject, new Vector3( 0, 0, 5 ), Random.Range(3,5));
            yield return new WaitForSeconds( Random.Range( 6,9 ));
        }
    }
}
