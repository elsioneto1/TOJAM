using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTravel : MonoBehaviour {

    public Vector3 targetPosition;
    public float awakingTime = 1;
    public float travelingTime = 1;

	// Use this for initialization
	void Start () {
        iTween.MoveTo( gameObject, new Vector3( transform.position.x, transform.position.y + 100 ), 0.5f );
        //iTween.ScaleTo( gameObject, new Vector3( 1, 1 ), 0.5f );
        iTween.ShakeScale( gameObject, new Vector3( 0.2f, 0.2f ), awakingTime );
        StartCoroutine( "StartMoving" );
	}

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds( awakingTime );
        iTween.MoveTo( gameObject, targetPosition, travelingTime );
        iTween.ScaleTo( gameObject, new Vector3( 0.25f, 0.25f ), travelingTime );
        yield return new WaitForSeconds( travelingTime );
        Destroy( gameObject );
    }
    
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}
