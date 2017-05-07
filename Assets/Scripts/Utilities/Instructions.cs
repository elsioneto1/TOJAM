using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Instructions : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetAxis( "FireP1" ) == 1 || Input.GetAxis( "FireP2" ) == 1 )
        {
            SceneManager.LoadScene( "Level" );

        }
    }
}