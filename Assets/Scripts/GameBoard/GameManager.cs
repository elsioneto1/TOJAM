using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static EnumHolder.GameState currentGameState = EnumHolder.GameState.None;

    public List<GameObject> baseObjects = new List<GameObject>();

    public static GameManager instance;
    

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

        switch (nextState)
        {
            case EnumHolder.GameState.Handing:
                Board.instance.Clear();
                break;

            case EnumHolder.GameState.Initiating:
                break;

            case EnumHolder.GameState.Rolling:
                break;

            case EnumHolder.GameState.DamageBoss:
                //Check Number of Hits on Dice
                Board.instance.EvaluateResults();
                GetBaseObject( "Boss" ).GetComponent<Boss>().DealDamage( 10 );
                break;

            case EnumHolder.GameState.DamageHeroes:
                break;

            case EnumHolder.GameState.GameOver:
                break;
        }
    }
}
