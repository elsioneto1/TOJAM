using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    static Board _instance;
    public static Board instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Board>();
            return _instance;
        }
    }

    [Header("Hand")]
    #region Hand

    public EnumHolder.HeroType currentHand;
    public GameObject handPrefab;

    private List<EnumHolder.HeroType> currentPlayerOrder = new List<EnumHolder.HeroType>();

    private int currentHero = 0;
    private EnumHolder.HeroType activeHero;
    #endregion

    [Header( "Dice Prefabs" )]
    #region Dice
    public GameObject d6Prefab;
    public GameObject d8Prefab;
    public GameObject d10Prefab;

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


    [HideInInspector]
    public List<Dice> activeDices = new List<Dice>();

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
            activeHero = currentPlayerOrder[ currentHero ];

            SetHeroActive(true);

            //Seta a mao
            GameObject hand = Instantiate(handPrefab,transform.position, Quaternion.identity) as GameObject;
            hand.GetComponent<Hand>().SetHand( activeHero );
   
            currentHero++;

            yield return new WaitForSeconds( timeHanding );
            CreateRoll();
            yield return new WaitForSeconds( waveList[currentWave].waveTime);
            Dice[] dices = FindObjectsOfType<Dice>();
            IsABouncer.ROLLING = false;
            bool allDicesAreSleeping = false;
            while(!allDicesAreSleeping)
            {
                bool b = true;
                for (int i = 0; i < dices.Length; i++)
                {
                    dices[i].rBody.mass = 10;
                    if (!dices[i].rBody.IsSleeping())
                    {
                        b = false;
                    }

                }
                allDicesAreSleeping = b;
                yield return new WaitForEndOfFrame();

            }

            DamageBoss();
            yield return new WaitForSeconds( timeDamagingBoss );

            SetHeroActive( false );
        }

        DamageHeroes();
        yield return new WaitForSeconds( timeDamaginHeroes );
        currentRound++;
        currentHero = 0;
        CreateRound();
       
    }

    void SetHeroActive(bool isActivating)
    {
        //Popa o Heroi na UI
        switch ( activeHero )
        {
            case EnumHolder.HeroType.Mage:
                GameManager.GetBaseObject( "Mage" ).GetComponent<Hero>().SetStarter( isActivating );
                break;

            case EnumHolder.HeroType.Warrior:
                GameManager.GetBaseObject( "Warrior" ).GetComponent<Hero>().SetStarter( isActivating );
                break;

            case EnumHolder.HeroType.Ranger:
                GameManager.GetBaseObject( "Ranger" ).GetComponent<Hero>().SetStarter( isActivating );
                break;
        }

    }


    void CreateRoll()
    {
        GameManager.ChangeState( EnumHolder.GameState.Rolling );
        IsABouncer.ROLLING = true;
        List<EnumHolder.DiceType> tempDiceList = waveList[currentWave].diceList;
        GameObject go;
        List<Dice> dices = new List<Dice>();
        for (int i = 0; i < tempDiceList.Count; i++)
        {
            switch (tempDiceList[i])
            {
                case EnumHolder.DiceType.D6:
                    //Criar o d6
                    go = (Instantiate(d6Prefab, transform.position, Quaternion.identity)) as GameObject;

                    go.transform.parent = Dice.SPAWN_BASE.transform;
                    if (go != null)
                    {
                        dices.Add(go.GetComponent<Dice>());
                    }
                    break;

                case EnumHolder.DiceType.D8:
                    //Criar o d8
                    go =  Instantiate( d8Prefab, transform.position, Quaternion.identity) as GameObject;
                    if (go != null)
                    {
                        dices.Add(go.GetComponent<Dice>());
                    }
                    break;

                case EnumHolder.DiceType.D10:
                    go = ( Instantiate( d10Prefab, transform.position, Quaternion.identity ) ) as GameObject;

                    go.transform.parent = Dice.SPAWN_BASE.transform;
                    if (go != null)
                    {
                        dices.Add(go.GetComponent<Dice>());
                    }
                    break;

              
            }
        }
        HandAsset.instance.SortDicesOnHand(dices);
         
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


    public void Clear()
    {
        for (int i = 0; i < activeDices.Count; i++)
        {
            activeDices[i].Clear();
        }
        activeDices.Clear();
    }


    public int EvaluateResults()
    {
        int totalHits = 0;
        // pega aqui o resultado de cada dado. Ele retorna a face 
        for (int i = 0; i < activeDices.Count; i++)
        {
            if(activeDices[i].EvaluateResults() == Dice.DiceResult.Sword)
            totalHits++;
        }

        return totalHits;
    }

}


