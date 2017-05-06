using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesRadius : MonoBehaviour {

    public List<Dice> dicesOnRadius = new List<Dice>();
    public List<Dice> diceOnRandiusAndInFrontOfCharacter = new List<Dice>();

    float possessionCooldown = 0.2f;
    float _elapsedPossessionCooldown;

    PlayerControl pControl;
    MeshRenderer renderer;
	// Use this for initialization
	void Start () {
        pControl = GetComponent<PlayerControl>();
        renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        
        float inputX = InputParser.GetHorizontal();
        float inputY = InputParser.GetVertical();
        Vector3 input = new Vector3(inputX,0,inputY);
        diceOnRandiusAndInFrontOfCharacter.Clear();
        if (GameManager.currentGameState == EnumHolder.GameState.Rolling)
        {
            // select by the input and then by the distance
            for (int i = 0; i < dicesOnRadius.Count; i++)
            {
                if (Vector3.Dot((dicesOnRadius[i].transform.position - transform.position).normalized, input.normalized) > 0.8f)
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
                if (possessable != null && _elapsedPossessionCooldown < 0)
                {
                    if (InputParser.GetPossession())
                    {
                        possessable.OnStartPossesion(this);
                        OnPossess();
                    }
                }
            }
        }
        else
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
        renderer.enabled = false;
    }


    public void PossessionExit()
    {
        pControl.enabled = true;
        renderer.enabled = true;
        _elapsedPossessionCooldown = possessionCooldown; 

    }


}
