using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParser : MonoBehaviour
{


    public static float GetHorizontal(PlayerControl.PlayerType pType)
    {
        float returnValue = 0;

        returnValue = Input.GetAxis("Horizontal" + pType.ToString());

        return returnValue;

    }


    public static float GetVertical(PlayerControl.PlayerType pType)
    {

        float returnValue = 0;

        returnValue = Input.GetAxis("Vertical" + pType.ToString());

        return returnValue;


    }

    public static bool GetPossession(PlayerControl.PlayerType pType)
    {
        bool returnValue = false;

        if ( pType == PlayerControl.PlayerType.P1)
        {
            if ( Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            returnValue = true;
        }
        if (pType == PlayerControl.PlayerType.P2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            returnValue = true;
        }

        return returnValue;
    }

    public static bool GetUnpossession(PlayerControl.PlayerType pType)
    {
        bool returnValue = false;

        if (pType == PlayerControl.PlayerType.P1)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                returnValue = true;
        }
        if (pType == PlayerControl.PlayerType.P2)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                returnValue = true;
        }

        return returnValue;
    }



}
