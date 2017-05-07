using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {


    public enum PlayerType { P1, P2 };
    public PlayerType playerType;

    float InputX;
    float InputY;
    SpriteRenderer myRenderer;
    public GameObject shadow; 
    public float forwardAngleTranslate = 10;
    public float boundXLeft;
    public float boundXRight;
    public float boundYUp;
    public float boundYDown;

    // public

    public float speed;
    Vector3 translate;

    public GameObject ObjectReference;

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<SpriteRenderer>();

        myRenderer.material.renderQueue = 4000;

	}
	
	// Update is called once per frame
	void Update () {

        InputX = InputParser.GetHorizontal(playerType);
        InputY = InputParser.GetVertical(playerType);
        // input gatherer
        translate = new Vector3(InputX, 0, InputY);
    }

    private void FixedUpdate()
    {

        

        // move the fockn character
        transform.position += (translate * Time.fixedDeltaTime * speed);


        if (translate != Vector3.zero)
        {
            if (myRenderer != null)
            {
                if (translate.x < -0.1f)
                {
                    myRenderer.flipX = true;
                }
                else if (translate.x > 0.1f)
                {
                    myRenderer.flipX = false;
                }
            }
        }

        Vector3 correctedPosition = transform.position;
        if (correctedPosition.x < boundXLeft)
            correctedPosition.x = boundXLeft;
        if (correctedPosition.x > boundXRight)
            correctedPosition.x = boundXRight;

        if (correctedPosition.z < boundYDown)
            correctedPosition.z = boundYDown;
        if (correctedPosition.z > boundYUp)
            correctedPosition.z = boundYUp;
        transform.position = correctedPosition;

        //  transform.forward = ( translate.normalized);

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, translate, Color.blue);
    }

}
