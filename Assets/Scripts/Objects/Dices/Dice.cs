using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    public EnumHolder.DiceType myDiceType;
    public EnumHolder.HeroType myHeroType;

    public enum DiceResult { Skull,Sword};

    protected float possesingTime = 0.2f;
    protected bool POSSESSED = false;
    public float appliedForce = 10;
    protected Vector3 forceRight;
    protected Vector3 forceForward;
    protected Vector3 finalForceVector;

    [HideInInspector]
    public PossesRadius whosPossessed;

    public Dictionary<string, DiceResult> results = new Dictionary<string, DiceResult>();


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
        newPlayerPosition.y = whosPossessed.transform.position.y;
        whosPossessed.transform.position = newPlayerPosition;
        whosPossessed.PossessionExit();
        whosPossessed = null;
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

