using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathOperations  {


    public static Vector3 RotateVectorY(Vector3 v, float rads)
    {

        Vector3 returnVector = v;

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Mathf.Cos(rads), 0, Mathf.Sin(rads), 0));
        matrix.SetRow(1, new Vector4(0, 1, 0, 0));
        matrix.SetRow(2, new Vector4(-Mathf.Sin(rads), 0, Mathf.Cos(rads), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));


        return matrix.MultiplyVector(v);

    }

    public static Vector3 RotateVectorX(Vector3 v, float rads)
    {

        Vector3 returnVector = v;

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(1, 0, 0, 0));
        matrix.SetRow(1, new Vector4(0, Mathf.Cos(rads), -Mathf.Sin(rads), 0));
        matrix.SetRow(2, new Vector4(0,Mathf.Sin(rads), Mathf.Cos(rads), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 0));


        return matrix.MultiplyVector(v);

    }


}


