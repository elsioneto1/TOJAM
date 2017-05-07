using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject overlay;
    public GameObject baloon;
    bool isOn = true;
	// Use this for initialization
	void Start () {
        StartCoroutine( "Show" );
        overlay.GetComponent<OverlayAlpha>().isIncreasing = true;
    }

    // Update is called once per frame
    void Update()
    {

        if ( Input.GetAxis( "FireP1" ) == 1 || Input.GetAxis( "FireP2" ) == 1 )
        {

            StartCoroutine( "Hide" );
            isOn = false;
        }
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds( 1 );     
        iTween.ScaleTo( baloon, new Vector3( 1, 1, 1 ), 1 );
    }

    IEnumerator Hide()
    {
        iTween.ScaleTo( baloon, new Vector3( 0, 0, 0 ), 1 );
        overlay.GetComponent<OverlayAlpha>().isIncreasing = false;
        yield return new WaitForSeconds( 1 );
        GameManager.isPaused = false;
    }
}
