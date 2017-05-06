using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    protected Rigidbody rBody ;

    protected Vector3[] vectors = new Vector3[6];
    protected Vector3[] parsedVectors;//= new Vector3[4];
    protected float possesingTime = 0.2f;
    public bool POSSESSED = false;
    protected Vector3 forceRight;
    protected Vector3 forceForward;
    protected Vector3 finalForceVector;
    protected float possetionDuration = 0;
    protected Vector3 input = Vector3.zero;
    protected float elapsedTime;


    // kinda random
    protected float forceX = 10;
    protected float forceY = 10;

    [HideInInspector]
    public PossesRadius whosPossessed;


    public EnumHolder.DiceType myDiceType;
    public EnumHolder.HeroType myHeroType;
    public float appliedForce = 10;

    public enum DiceResult { Skull, Sword };

    


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
    public  virtual void Start () {
        Board.instance.activeDices.Add(this);   
        rBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public virtual void OnStartPossesion(PossesRadius player)
    {
        whosPossessed = player;
    }

    public virtual void OnEndPossesion()
    {
        POSSESSED = false;
        Vector3 newPlayerPosition = transform.position;
        if (whosPossessed != null)
        {
            newPlayerPosition.y = whosPossessed.transform.position.y;
            whosPossessed.transform.position = newPlayerPosition;
            whosPossessed.PossessionExit();
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

