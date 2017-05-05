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

}
