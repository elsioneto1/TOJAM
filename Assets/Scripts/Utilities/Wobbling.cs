using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbling : MonoBehaviour {

    public GameObject dialogueObject;

    void Start()
    {

    }
	IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds( 4 );
        iTween.ScaleTo( gameObject, new Vector3( 0, 0, 0 ), 1 );
        iTween.ScaleTo( dialogueObject, new Vector3( 0, 0, 0 ), 1 );
    }
    public void Grow()
    {
        iTween.ScaleTo( gameObject, new Vector3( 1, 1, 1 ), 1 );
        iTween.ScaleTo( dialogueObject, new Vector3( 1, 1, 1 ), 1 );
        iTween.PunchRotation( gameObject, new Vector3( 0, 0, 10 ), 5 );
        StartCoroutine( "ScaleDown" );
    }
}
