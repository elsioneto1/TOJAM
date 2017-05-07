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

        if (Input.GetAxis("Fire" + pType.ToString()) == 1)
        {
            returnValue = true;
        }

        return returnValue;
    }

    public static bool GetUnpossession(PlayerControl.PlayerType pType)
    {
        bool returnValue = false;

        if (Input.GetAxis("Fire" + pType.ToString()) == 1)
        {
            returnValue = true;
        }

        return returnValue;
    }



}
