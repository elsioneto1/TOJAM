using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesRadius : MonoBehaviour {



    public List<Dice> dicesOnRadius = new List<Dice>();
    public List<Dice> diceOnRandiusAndInFrontOfCharacter = new List<Dice>();

    float possessionCooldown = 0.5f;
    float _elapsedPossessionCooldown;
    public Animator anim;
    public PlayerControl pControl;
    SpriteRenderer myRenderer;
    Vector3 input;
    // Use this for initialization
    void Start () {
        pControl = GetComponent<PlayerControl>();
        myRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        
        float inputX = InputParser.GetHorizontal(pControl.playerType);
        float inputY = InputParser.GetVertical(pControl.playerType);
        if (Mathf.Abs(inputX + inputY) > 0.2f)
        {
            input = new Vector3(inputX, 0, inputY);
            input.Normalize();
        }
        diceOnRandiusAndInFrontOfCharacter.Clear();
        if (pControl.enabled == false)
            return;

        if (GameManager.currentGameState == EnumHolder.GameState.Rolling)
        {
          //  Debug.Log(dicesOnRadius.Count);
            // select by the input and then by the distance
            for (int i = 0; i < dicesOnRadius.Count; i++)
            {


                if (Vector3.Dot((dicesOnRadius[i].transform.position - transform.position).normalized, input.normalized) > -1f)
                {
                    diceOnRandiusAndInFrontOfCharacter.Add(dicesOnRadius[i]);
                }
            }
            _elapsedPossessionCooldown -= Time.deltaTime;
            // our possessable die
            if (diceOnRandiusAndInFrontOfCharacter.Count > 0)
            {
                Dice possessable = diceOnRandiusAndInFrontOfCharacter[0];
                for (int i = 0; i < diceOnRandiusAndInFrontOfCharacter.Count; i++)
                {
                    if (Vector3.Distance(diceOnRandiusAndInFrontOfCharacter[i].transform.position, transform.position)
                        <
                        Vector3.Distance(possessable.transform.position, transform.position))
                    {
                        possessable = diceOnRandiusAndInFrontOfCharacter[i];
                    }
                }

                // if the input is given, possess the son of a gun
                if (possessable.diceState != Dice.DiceState.possessed && !possessable.alreadyPossesed)
                {
                    if (possessable != null && _elapsedPossessionCooldown < 0)
                    {
                        possessable.diceState = Dice.DiceState.selected;
                        if (InputParser.GetPossession(pControl.playerType   ))
                        {
                            possessable.rBody.velocity = possessable.rBody.velocity.normalized * 2;
                            possessable.OnStartPossesion(this);
                            pControl.enabled = false;

                            anim.SetTrigger("Possessing");
                            //OnPossess();
                        }
                    }
                }
            }
        }
        else if (GameManager.currentGameState != EnumHolder.GameState.Initiating)
        {
            if (dicesOnRadius.Count > 0)
            {
                dicesOnRadius.Clear();
            }
            if (diceOnRandiusAndInFrontOfCharacter.Count > 0)
            {
                diceOnRandiusAndInFrontOfCharacter.Clear();
            }
        }
	}

    public void OnTriggerEnter(Collider other)
    {

        Dice d = other.gameObject.GetComponent<Dice>();

        if ( d != null)
        {
            dicesOnRadius.Add(d);
        }


    }

    public void OnTriggerExit(Collider other)
    {

        Dice d = other.gameObject.GetComponent<Dice>();


        if (d != null)
        {
            dicesOnRadius.Remove(d);
        }


    }

    public void OnPossess()
    {
        pControl.enabled = false;
        myRenderer.enabled = false;
    }


    public void PossessionExit()
    {
        pControl.enabled = true;
        myRenderer.enabled = true;
        _elapsedPossessionCooldown = possessionCooldown; 

    }


}
