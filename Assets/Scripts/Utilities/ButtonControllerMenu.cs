using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControllerMenu : MonoBehaviour {
    float InputX;
    public PlayerControl.PlayerType playerType;
    GameObject currentSelected;

    public GameObject play;

    public GameObject quit;
    // Use this for initialization
	
    void Start()
    {
        currentSelected = play;
    }

	// Update is called once per frame
	void Update () {
        InputX = InputParser.GetHorizontal( playerType );

        if( InputX < -0.25f)
        {
            currentSelected = play;
            iTween.ScaleTo( play, new Vector3( 1, 1, 1 ), 0.5f );
            iTween.ScaleTo( quit, new Vector3( 0.75f, 0.75f, 0.75f ), 0.5f );
        }

        else if ( InputX > 0.25f )
        {
            currentSelected = quit;
            iTween.ScaleTo( play, new Vector3( 0.75f, 0.75f, 0.75f ), 0.5f );
            iTween.ScaleTo( quit, new Vector3( 1, 1, 1 ), 0.5f );
        }

        if ( Input.GetAxis( "Fire" + playerType.ToString()) == 1 )
        {
            if ( currentSelected == play )
                SceneManager.LoadScene( "Level" );

            else
                Application.Quit();
        }

    }
}
