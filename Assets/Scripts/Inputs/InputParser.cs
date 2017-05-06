using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParser : MonoBehaviour
{


    public static float GetHorizontal()
    {
        float returnValue = 0;

        returnValue = Input.GetAxis("Horizontal");

        return returnValue;

    }


    public static float GetVertical()
    {

        float returnValue = 0;

        returnValue = Input.GetAxis("Vertical");

        return returnValue;


    }

    public static bool GetPossession()
    {
        bool returnValue = false;

        if (Input.GetAxis("Fire1") == 1)
        {
            returnValue = true;
        }

        return returnValue;
    }

    public static bool GetUnpossession()
    {
        bool returnValue = false;

        if (Input.GetAxis("Fire1") == 1)
        {
            returnValue = true;
        }

        return returnValue;
    }



}
