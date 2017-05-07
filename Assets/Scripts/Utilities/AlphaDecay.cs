using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaDecay : MonoBehaviour {

    private Text tComponent;
    float currentA = 255;
    public float alphaSpeed;
    public int decayTime = 0;
    bool isDecaying;

    void Awake()
    {
        tComponent = GetComponent<Text>();
    }
    // Use this for initialization
    void Start () {

        StartCoroutine( "StartDecay" );
     
	}
	
	// Update is called once per frame
	void Update () { 

        if(isDecaying)
        {
            currentA -= alphaSpeed;
            tComponent.color = new Color( tComponent.color.r, tComponent.color.g, tComponent.color.b, currentA / 255 );

            if ( currentA < 0 )
            {  
                currentA = 255f;
                isDecaying = false;
                tComponent.color = Color.black;
            }
                
        }

    }

    public void StartAlphaDecay()
    {
        currentA = 255f;
        isDecaying = false;
        tComponent.color = Color.black;
        StartCoroutine( "StartDecay" );
    }

    IEnumerator StartDecay()
    {
        yield return new WaitForSeconds( decayTime );
        isDecaying = true;
    }
}
