using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayAlpha : MonoBehaviour {

    float currentA = 0;
    float alphaSpeed = 5;
    public bool isIncreasing = false;
    Image imageComponent;
    
    void Awake()
    {
        imageComponent = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {


            if ( currentA < 175 && isIncreasing )
                currentA += alphaSpeed;

            else if ( currentA > 0 && !isIncreasing )
                currentA -= alphaSpeed;

            imageComponent.color = new Color( imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, currentA / 255 );
        

    
	}
    
}
