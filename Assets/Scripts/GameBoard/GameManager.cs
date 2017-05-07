using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static EnumHolder.GameState currentGameState = EnumHolder.GameState.None;

    public List<GameObject> baseObjects = new List<GameObject>();

    public static GameManager instance;

    public Hero activeHero;
    

    void Awake()
    {
        instance = this;
    }

    public static GameObject GetBaseObject(string objectName)
    {

        // DEUS OTIMIZACAO TA VENDO ISSO AE
        foreach (GameObject go in instance.baseObjects)
        {
            if ( go.name == objectName )
                return go;
        }

        throw new System.Exception( "String errada ou GameObject nao existe!" );
    }

    public static void ChangeState(EnumHolder.GameState nextState)
    {
        currentGameState = nextState;
        Debug.Log( currentGameState );

        instance.StartCoroutine( "Transition" );

        switch (nextState)
        {
            case EnumHolder.GameState.Handing:
                Board.instance.Clear();
                break;

            case EnumHolder.GameState.Initiating:
                instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Starter );
                break;

            case EnumHolder.GameState.Rolling:
                instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Roll );
                break;

            case EnumHolder.GameState.Combat:
                int hits = Board.instance.EvaluateResults();
                instance.StartCoroutine( "HitBoss", hits );
                instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Hit );

                int randomHero = Random.Range( 0, 4 );
                    switch(randomHero)
                    {
                        case 0:
                            GetBaseObject( "Mage" ).GetComponent<Hero>().DealDamage(1);
                            break;

                        case 1:
                            GetBaseObject( "Warrior" ).GetComponent<Hero>().DealDamage( 1 );
                            break;

                        case 2:
                            GetBaseObject( "Ranger" ).GetComponent<Hero>().DealDamage( 1 );
                            break;
                    }
                    instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Damage );
                break;


            case EnumHolder.GameState.GameOver:
                break;

                //case EnumHolder.GameState.DamageBoss:
                //    //Check Number of Hits on Dice
                //    int hits = Board.instance.EvaluateResults();

                //    instance.StartCoroutine( "HitBoss", hits );

                //    instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Hit );
                //    break;

                //case EnumHolder.GameState.DamageHeroes:
                //    int randomHero = Random.Range( 0, 4 );
                //    switch(randomHero)
                //    {
                //        case 0:
                //            GetBaseObject( "Mage" ).GetComponent<Hero>().DealDamage(1);
                //            break;

                //        case 1:
                //            GetBaseObject( "Warrior" ).GetComponent<Hero>().DealDamage( 1 );
                //            break;

                //        case 2:
                //            GetBaseObject( "Ranger" ).GetComponent<Hero>().DealDamage( 1 );
                //            break;
                //    }
                //    instance.StartCoroutine( "Dialogue", EnumHolder.DialogueType.Damage );
                //    break;

        }
    }

    IEnumerator Dialogue(EnumHolder.DialogueType dialogueType)
    {
        yield return new WaitForSeconds( 2f );
        instance.activeHero.CallDialogue( dialogueType );
    }

    IEnumerator HitBoss(int hitNumber)
    {
        int counter = 0;

        while( counter < hitNumber)
        {
            GetBaseObject( "Boss" ).GetComponent<Boss>().DealDamage( 1 );
            counter++;
            yield return new WaitForSeconds( 0.5f );
        }
        
    }

    IEnumerator Transition()
    {
        GetBaseObject( "TurnPaper" ).GetComponent<TurnPaper>().SetText( true );
        yield return new WaitForSeconds( 1 );
        GetBaseObject( "TurnPaper" ).GetComponent<TurnPaper>().SetText( false );

    }
}
