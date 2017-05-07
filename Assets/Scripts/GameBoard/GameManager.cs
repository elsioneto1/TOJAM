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
                instance.activeHero.CallDialogue( EnumHolder.DialogueType.Starter );
                break;

            case EnumHolder.GameState.Rolling:
             
                break;

            case EnumHolder.GameState.DamageBoss:
                //Check Number of Hits on Dice
                int hits = Board.instance.EvaluateResults();
                GetBaseObject( "Boss" ).GetComponent<Boss>().DealDamage(hits);

                instance.activeHero.CallDialogue( EnumHolder.DialogueType.Hit );
                break;

            case EnumHolder.GameState.DamageHeroes:
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

                instance.activeHero.CallDialogue( EnumHolder.DialogueType.Damage);
                break;

            case EnumHolder.GameState.GameOver:
                break;
        }
    }

    IEnumerator Transition()
    {
        GetBaseObject( "TurnPaper" ).GetComponent<TurnPaper>().SetText( true );
        yield return new WaitForSeconds( 1 );
        GetBaseObject( "TurnPaper" ).GetComponent<TurnPaper>().SetText( false );

    }
}
