using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

 
    [Header("Hand")]
    #region Hand

    public EnumHolder.HeroType currentHand;
    public GameObject handPrefab;

    private List<EnumHolder.HeroType> currentPlayerOrder = new List<EnumHolder.HeroType>();

    private int currentHero = 0;
    #endregion

    [Header( "Dice Prefabs" )]
    #region Dice
    public GameObject d6Prefab;
    public GameObject d8Prefab;
    public GameObject d12Prefab;

    #endregion

    [Header( "Action Times" )]
    #region actionTimes
    public float timeInitiating;
    public float timeHanding;
    public float timeInBetweenRolls;
    public float timeDamagingBoss;
    public float timeDamaginHeroes;
    #endregion

    [Header( "WaveData" )]
    #region waves
    public List<Wave> waveList = new List<Wave>();
    private int currentWave;
    #endregion

    [Header( "Round" )]
    #region round
    private int currentRound;
    #endregion
    void Start()
    {
        currentPlayerOrder.Add( EnumHolder.HeroType.Mage);
        currentPlayerOrder.Add( EnumHolder.HeroType.Warrior);
        currentPlayerOrder.Add( EnumHolder.HeroType.Ranger);

        CreateRound();

    }

    void CreateRound()      
    {
        if(currentRound <= waveList.Count/3)
        {
            GameManager.ChangeState( EnumHolder.GameState.Initiating );
            Shuffle( currentPlayerOrder );
            StartCoroutine("CreateHand");
        }
    }

    IEnumerator CreateHand()
    {
        yield return new WaitForSeconds( timeInitiating );

        while (currentHero < 3)
        {
            GameManager.ChangeState( EnumHolder.GameState.Handing );

            GameObject hand = Instantiate(handPrefab,transform.position, Quaternion.identity) as GameObject;
            hand.GetComponent<Hand>().SetHand(currentPlayerOrder[currentHero]);
            currentHero++;

            yield return new WaitForSeconds( timeHanding );
            CreateRoll();
            yield return new WaitForSeconds( timeInBetweenRolls );
            DamageBoss();
            yield return new WaitForSeconds( timeDamagingBoss );
        }

        DamageHeroes();
        yield return new WaitForSeconds( timeDamaginHeroes );
        currentRound++;
        currentHero = 0;
        CreateRound();
       
    }

    void CreateRoll()
    {
        GameManager.ChangeState( EnumHolder.GameState.Rolling );
        List<EnumHolder.DiceType> tempDiceList = waveList[currentWave].diceList; 
        for (int i = 0; i < tempDiceList.Count; i++)
        {
            switch (tempDiceList[i])
            {
                case EnumHolder.DiceType.D6:
                    //Criar o d6
                    Instantiate(d6Prefab, transform.position, Quaternion.identity);
                    break;

                case EnumHolder.DiceType.D8:
                    //Criar o d8
                    Instantiate( d8Prefab, transform.position, Quaternion.identity);
                    break;

                case EnumHolder.DiceType.D12:
                    //criar o d12
                    Instantiate(d12Prefab, transform.position, Quaternion.identity);
                    break;
            }
        }

         
    }

    void DamageBoss()
    {
        //Chamar Dano do Boss
        GameManager.ChangeState( EnumHolder.GameState.DamageBoss);
        currentWave++;
    }

    void DamageHeroes()
    {
        //Chamar Dano nos Herois
        GameManager.ChangeState( EnumHolder.GameState.DamageHeroes );

        if (currentWave == waveList.Count)
            EndGame();
    }

    
    void EndGame()
    {
        StopAllCoroutines();
        Debug.Log( "There are no more rounds" );
    }


    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


}


