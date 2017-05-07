using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    float InputX;
    float InputY;
    SpriteRenderer renderer;
    public float forwardAngleTranslate = 10;
   // public

    public float speed;
    Vector3 translate;

    public GameObject ObjectReference;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        InputX = InputParser.GetHorizontal();
        InputY = InputParser.GetVertical();
        // input gatherer
        translate = new Vector3(InputX, 0, InputY);
    }

    private void FixedUpdate()
    {

        

        // move the fockn character
        transform.position += (translate * Time.fixedDeltaTime * speed);

        if (translate != Vector3.zero)
        {
            if (translate.x < -0.1f)
            {
                renderer.flipX = true;  
            }
            else if ( translate.x > 0.1f)
            {
                renderer.flipX = false;
            }
        }
      //  transform.forward = ( translate.normalized);

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, translate, Color.blue);
    }

}
