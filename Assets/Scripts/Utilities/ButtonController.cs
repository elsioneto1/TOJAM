using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
    float InputY;
    public PlayerControl.PlayerType playerType;
    GameObject currentSelected;

    public GameObject replay;
    public GameObject backToMenu;
    // Use this for initialization
	
    void Start()
    {
        currentSelected = replay;
    }

	// Update is called once per frame
	void Update () {
        InputY = InputParser.GetVertical( playerType );

        if( InputY > 0.25f)
        {
            currentSelected = replay;
            iTween.ScaleTo( replay, new Vector3( 1, 1, 1 ), 0.5f );
            iTween.ScaleTo( backToMenu, new Vector3( 0.75f, 0.75f, 0.75f ), 0.5f );
        }

        else if ( InputY < -0.25f )
        {
            currentSelected = backToMenu;
            iTween.ScaleTo( replay, new Vector3( 0.75f, 0.75f, 0.75f ), 0.5f );
            iTween.ScaleTo( backToMenu, new Vector3( 1, 1, 1 ), 0.5f );
        }

        if ( Input.GetAxis( "Fire" + playerType.ToString() ) == 1 )
        {
            if(currentSelected == replay)
                SceneManager.LoadScene( "Level" );

            else
                SceneManager.LoadScene( "Menu" );
        }

    }
}
