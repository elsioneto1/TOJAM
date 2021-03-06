﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {


    public enum DiceState { normal, selected, possessed };
    public DiceState diceState;

    Rigidbody _rBody;
    public Rigidbody rBody
    {
        get
        {
            if (_rBody == null)
            {
                _rBody = GetComponent<Rigidbody>();
            }
            return _rBody;
        }
    }

    protected Vector3[] vectors = new Vector3[6];
    protected Vector3[] parsedVectors;//= new Vector3[4];
    protected float possesingTime = 0.5f;
    public bool POSSESSED = false;
    protected Vector3 forceRight;
    protected Vector3 forceForward;
    protected Vector3 finalForceVector;
    protected float possetionDuration = 0;
    protected Vector3 input = Vector3.zero;
    protected float elapsedTime;


    Material newMaterial;
    // kinda random
    protected float forceX = 10;
    protected float forceY = 10;

    [HideInInspector]
    public PossesRadius whosPossessed;
    public bool alreadyPossesed = false;

    public EnumHolder.DiceType myDiceType;
    public EnumHolder.HeroType myHeroType;
    public float appliedForce = 10;

    public enum DiceResult { Skull, Sword };


    float animationCounter = 0; // 0 to 360
    float animationValue = 0; // 0 to 1
    Color minColor;
    Color maxColor;
    Color currentColor;

    [Header("Color Selection")]
    public Color MaxColor_MayPosses;
    public Color MinColor_MayPosses;

    public Color MaxColor_Selected;
    public Color MinColor_Selected;

    public Color MaxColor_Possessed_P1;
    public Color MinColor_Possessed_P1;

    public Color MaxColor_Possessed_P2;
    public Color MinColor_Possessed_P2;


    static GameObject _spawnBase;
    public static GameObject SPAWN_BASE
    {
        get
        {
            if (_spawnBase == null)
            {
                _spawnBase = GameObject.Find("DiceHolder");
            }
            return _spawnBase;
        }
    }

    // Use this for initialization
    public virtual void Start() {

        Board.instance.activeDices.Add(this);
        MeshRenderer mr = GetComponent<MeshRenderer>();
        newMaterial = new Material(mr.material);
        mr.material = newMaterial;
        newMaterial.SetColor("_EmissionColor", Color.black);
        //BlinkPossessed(whosPossessed.GetComponent<PlayerControl>());
    }

    public void SetNormalColor()
    {

        minColor = Color.black;
        maxColor = Color.black;


    }

    public void InterpolateAnimationValue()
    {
        animationCounter += 2;
        if (animationCounter > 360)
            animationCounter = 0;
        animationValue = Mathf.Cos((animationCounter * Mathf.PI) / 180) * Mathf.Sin((animationCounter * Mathf.PI) / 180);


        currentColor = Color.Lerp(minColor, maxColor, Mathf.Abs(animationValue));


        newMaterial.SetColor("_EmissionColor", currentColor);
    }

    public void BlinkMayPossess()
    {
        minColor = MinColor_MayPosses;
        maxColor = MaxColor_MayPosses;

    }

    public void BlinkPossessed(PlayerControl pc)
    {
        //Debug.Log("aa");
        if (pc.playerType == PlayerControl.PlayerType.P1)
        {
            minColor = MinColor_Possessed_P1;
            maxColor = MaxColor_Possessed_P1;
        }
        else
        {
            minColor = MinColor_Possessed_P2;
            maxColor = MaxColor_Possessed_P2;
        }
    }

    public void BlinkSelected()
    {
        minColor = MinColor_Selected;
        maxColor = MaxColor_Selected;
    }


    // Update is called once per frame
    public virtual void Update() {




        if (diceState == DiceState.normal)
        {
            BlinkMayPossess();
        }
        else if (diceState == DiceState.possessed)
        {
            if (whosPossessed != null)
                BlinkPossessed(whosPossessed.GetComponent<PlayerControl>());
        }
        else if (diceState == DiceState.selected)
        {
            BlinkSelected();
        }
        InterpolateAnimationValue();
        if (diceState != DiceState.possessed)
            diceState = DiceState.normal;
    }

    public virtual void OnStartPossesion(PossesRadius player)
    {
        // lock the interaction if already possesed
       // if (alreadyPossesed)
        //    return;


        player.GetComponent<GhostSprites>().HideTrail();
        player.GetComponent<GhostSprites>().killSwitch = true;
        player.GetComponent<PlayerControl>().shadow.SetActive(false);

        rBody.angularVelocity = Vector3.zero;
        rBody.Sleep();
        whosPossessed = player;
        diceState = DiceState.possessed;
    }

    public void JumpGambiarra()
    {
        // GAMBIARRA EXTREMA

            transform.position += Vector3.up * 0.1f;
        // </GAMBIARRA EXTREMA>
    }

    public virtual void OnEndPossesion()
    {
        //  GetComponent<GhostSprites>().killSwitch = false;
      

        if (IsABouncer.ROLLING)
        {
            JumpGambiarra();
            //rBody.mass = 15;
        }
        POSSESSED = false;
        //rBody.velocity = rBody.velocity * 0.5f;
        alreadyPossesed = true;
       
     
        diceState = DiceState.normal;
        Vector3 newPlayerPosition = transform.position;
        //Debug.Log(whosPossessed);
        if (whosPossessed != null)
        {
            whosPossessed.GetComponent<GhostSprites>().killSwitch = false;
            whosPossessed.GetComponent<PlayerControl>().shadow.SetActive(true);
            whosPossessed.GetComponent<GhostSprites>().ShowTrail(whosPossessed.transform.position);
            newPlayerPosition.y = whosPossessed.transform.position.y;
            whosPossessed.transform.position = newPlayerPosition;
            whosPossessed.PossessionExit();
            whosPossessed.anim.Play("IdleAndMoving");
            whosPossessed = null;
        }
    }

    public void Clear()
    {
      
        Destroy(gameObject);
    }

    public virtual DiceResult EvaluateResults()
    {
        DiceResult dr = DiceResult.Skull;
        if (whosPossessed != null)
        {
            OnEndPossesion();
        }

        return dr;
    }

    public void SetColor(EnumHolder.HeroType heroType)
    {
        myHeroType = heroType;

        switch (myHeroType)
        {
            case EnumHolder.HeroType.Mage:
                //Blue Dice
                break;

            case EnumHolder.HeroType.Ranger:
                //Green Type
                break;

            case EnumHolder.HeroType.Warrior:
                //Red Type
                break;
        }
    }

}

