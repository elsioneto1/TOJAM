using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {

    private Text tComponent;
    float currentR = 255;
    float currentA = 255;
    public float lerpingSpeed;
    public float alphaSpeed;
    public Color baseColor1;
    public Color baseColor2;

    void Awake()
    {
        tComponent = GetComponent<Text>();
    }

	
	// Update is called once per frame
	void Update ()
    {
        currentR -= lerpingSpeed;
        currentA -= alphaSpeed;
        if ( currentR < 0 || currentR > 255 )
            lerpingSpeed *= -1;
        tComponent.color = new Color( currentR/255, (currentR/3)/255, ( currentR / 3 ) / 255, currentA / 255);

        transform.Translate( new Vector3( 0, 1 ) );

        if ( currentA < 0 )
            Destroy( gameObject );
    }
}
