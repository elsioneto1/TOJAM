using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public List<GameObject> d6PrefabList = new List<GameObject>();
    public List<GameObject> d8PrefabList = new List<GameObject>();
    public List<GameObject> d10PrefabList = new List<GameObject>();

    private GameObject currentd6Prefab;
    private GameObject currentd8Prefab;
    private GameObject currentd10Prefab;
    // public GameObject hitPrefab;
    public GameObject swordFeedback;
    public GameObject skullFeedback;

    #endregion

    [Header( "Action Times" )]
    #region actionTimes
    public float timeInitiating;
    public float timeHanding;
    public float timeInBetweenRolls;
    public float timeCombat;
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


        CreateRound();
    }

    void CreateRound()
    {

        if (currentRound <= waveList.Count/3)
        {
            currentPlayerOrder.Clear();

            if(!GameManager.GetBaseObject("Mage").GetComponent<Hero>().isDead)
                currentPlayerOrder.Add( EnumHolder.HeroType.Mage );

            if ( !GameManager.GetBaseObject( "Warrior" ).GetComponent<Hero>().isDead )
            currentPlayerOrder.Add( EnumHolder.HeroType.Warrior );

            if ( !GameManager.GetBaseObject( "Ranger" ).GetComponent<Hero>().isDead )
                currentPlayerOrder.Add( EnumHolder.HeroType.Ranger );

            if ( currentPlayerOrder.Count != 0 )
            {

                    Shuffle( currentPlayerOrder );
                    Initiate();
            }

            else
            {           
               //Heroes Lost
               EndGame( false );
            }

        }
    }

    void Initiate()
    {
        if ( GameManager.GetBaseObject( "Boss" ).GetComponent<Boss>().health == 0 )
            EndGame( true );


        if ( currentHero != currentPlayerOrder.Count)
        {
            activeHero = currentPlayerOrder[ currentHero ];
            GameManager.ChangeState( EnumHolder.GameState.Initiating );
        }

        StartCoroutine( "CreateHand" );

    }
    
    IEnumerator CreateHand()
    {

        if ( currentHero < currentPlayerOrder.Count)
        {
            SetHeroActive( true );
            yield return new WaitForSeconds( timeInitiating );

            GameManager.ChangeState( EnumHolder.GameState.Handing );

            //Seta a mao
            GameObject hand = Instantiate(handPrefab,transform.position, Quaternion.identity) as GameObject;
            hand.GetComponent<Hand>().SetHand( activeHero );
   
            currentHero++;

            yield return new WaitForSeconds( timeHanding );
            CreateRoll();
            yield return new WaitForSeconds( waveList[currentWave].waveTime);
            Dice[] dices = FindObjectsOfType<Dice>();
            IsABouncer.ROLLING = false;
            int maxFrames = 20;
            int frameCount = 0;
            bool allDicesAreSleeping = false;
            while(!allDicesAreSleeping)
            {
                bool b = true;
                for (int i = 0; i < dices.Length; i++)
                {

                    Vector3 velocity = dices[i].rBody.velocity;


                    if (!dices[i].rBody.IsSleeping())
                    {
                        b = false;
                    }
                    if (frameCount > maxFrames)
                    {
                        if (!dices[i].rBody.IsSleeping())
                        {
                            if ( velocity.magnitude > 1 )
                            {
                               // dices[i].rBody.velocity *= 08f;
                            }
                            else
                           // dices[i].rBody.Sleep();
                            
                           // Debug.Log("not sleeping");
                            b = false;
                        }
                    }
                    else
                    {
                        dices[i].rBody.mass = 20;
                        dices[i].rBody.angularVelocity = Vector3.zero;
                    }

                   
                   // Debug.Log(dices[i].rBody.velocity);
                    dices[i].OnEndPossesion();
                   

                }
                allDicesAreSleeping = b;
                yield return new WaitForEndOfFrame();
                frameCount++;
               

            }

            Combat();
            yield return new WaitForSeconds( timeCombat );

            SetHeroActive( false );

            yield return new WaitForSeconds( 1f );

            if (GameManager.isPaused && GameManager.instance.learning)
            {
                GameManager.instance.learning = false;
                StartCoroutine( "WaitInitiate" );
            }
                

            else
                Initiate();
        }

        else
        {
            //yield return new WaitForSeconds( timeCombat );
            currentRound++;
            currentHero = 0;
            CreateRound();
        } 
    }

    IEnumerator WaitInitiate()
    {
        
        while(GameManager.isPaused)
        {
            Debug.Log( "COROUTINE" );
            yield return new WaitForSeconds( 0.5f );
        }
        GameManager.instance.learning = false;
        GameManager.isPaused = false;
        Initiate();
        StopCoroutine( "WaitInitiate" );
    }
   
    void SetHeroActive(bool isActivating)
    {

        //Popa o Heroi na UI
        switch ( activeHero )
        {
            case EnumHolder.HeroType.Mage:
                GameManager.GetBaseObject( "Mage" ).GetComponent<Hero>().SetStarter( isActivating );
                GameManager.instance.activeHero = GameManager.GetBaseObject( "Mage" ).GetComponent<Hero>();
                currentd6Prefab = d6PrefabList[ 0 ];
                currentd10Prefab = d10PrefabList[ 0 ];
                break;

            case EnumHolder.HeroType.Warrior:
                GameManager.GetBaseObject( "Warrior" ).GetComponent<Hero>().SetStarter( isActivating );
                GameManager.instance.activeHero = GameManager.GetBaseObject( "Warrior" ).GetComponent<Hero>();
                currentd6Prefab = d6PrefabList[ 1 ];
                currentd10Prefab = d10PrefabList[ 1 ];
                break;

            case EnumHolder.HeroType.Ranger:
                GameManager.GetBaseObject( "Ranger" ).GetComponent<Hero>().SetStarter( isActivating );
                GameManager.instance.activeHero = GameManager.GetBaseObject( "Ranger" ).GetComponent<Hero>();
                currentd6Prefab = d6PrefabList[ 2 ];
                currentd10Prefab = d10PrefabList[ 2 ];
                break;
        }

    }



    void CreateRoll()
    {
        GameManager.ChangeState( EnumHolder.GameState.Rolling );
        IsABouncer.ROLLING = true;
        List<EnumHolder.DiceType> tempDiceList = waveList[currentWave].diceList;
        if (PhantomMode.instance)
            PhantomMode.instance.StartPhantom(waveList[currentWave].waveTime);
        GameObject go;
        List<Dice> dices = new List<Dice>();
        for (int i = 0; i < tempDiceList.Count; i++)
        {
            switch (tempDiceList[i])
            {
                case EnumHolder.DiceType.D6:
                    //Criar o d6
                    go = (Instantiate( currentd6Prefab, transform.position + Vector3.up * 1000, Quaternion.identity)) as GameObject;

                    go.transform.parent = Dice.SPAWN_BASE.transform;
                    if (go != null)
                    {
                        dices.Add(go.GetComponent<Dice>());
                    }
                    break;

                case EnumHolder.DiceType.D8:
                    //Criar o d8
                    go =  Instantiate( currentd8Prefab, transform.position + Vector3.up * 1000, Quaternion.identity) as GameObject;
                    if (go != null)
                    {
                        dices.Add(go.GetComponent<Dice>());
                    }
                    break;

                case EnumHolder.DiceType.D10:
                    go = ( Instantiate( currentd10Prefab, transform.position + Vector3.up * 1000, Quaternion.identity ) ) as GameObject;

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

    void Combat()
    {
        GameManager.ChangeState( EnumHolder.GameState.Combat );
        currentWave++;
    }

    //void DamageBoss()
    //{
    //    //Chamar Dano do Boss
    //    GameManager.ChangeState( EnumHolder.GameState.DamageBoss);
    //    currentWave++;
    //}

    //void DamageHeroes()
    //{
    //    //Chamar Dano nos Herois
    //    GameManager.ChangeState( EnumHolder.GameState.DamageHeroes );

    //    if (currentWave == waveList.Count)
    //        EndGame();
    //}

    
    void EndGame(bool heroesWon)
    {
        StopAllCoroutines();

        if (heroesWon)
        {
            SceneManager.LoadScene( "HeroWins" );
        }

        else
        {
            SceneManager.LoadScene( "GoatWins" );
        }
        
        
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


          //  GameObject hitFeedback = Instantiate( hitPrefab, activeDices[ i ].transform.position, Quaternion.identity ) as GameObject;

           //RectTransform CanvasRect = GameManager.GetBaseObject( "Canvas" ).GetComponent<RectTransform>();
            //Vector2 ViewportPosition = Camera.main.WorldToViewportPoint( hitFeedback.transform.position );
            //Vector2 WorldObject_ScreenPosition = new Vector2(
            //( ( ViewportPosition.x * CanvasRect.sizeDelta.x ) - ( CanvasRect.sizeDelta.x * 0.5f ) ),
            //( ( ViewportPosition.y * CanvasRect.sizeDelta.y ) - ( CanvasRect.sizeDelta.y * 0.5f ) ) );
            //WorldObject_ScreenPosition.x += 1920 * 0.5f;
            //WorldObject_ScreenPosition.y += 1080 * 0.5f;
            //now you can set the position of the ui element
            //hitFeedback.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;


           // hitFeedback.transform.SetParent(GameManager.GetBaseObject( "Canvas" ).transform);
            //hitFeedback.transform.position = hitFeedback.transform.GetComponent<Canvas>().worldCamera.WorldToViewportPoint( hitFeedback.transform.position );
          //  Vector3 newPoint = hitFeedback.transform.position;
          
           // hitFeedback.transform .position


        }

        return totalHits;
    }

    public void CallDamageEffects( bool isHeroes, Vector3 rectTransformPosition )
    {
        for ( int i = 0; i < activeDices.Count; i++ )
        {
            if ( activeDices[ i ].EvaluateResults() == Dice.DiceResult.Sword )
            {
                    if ( isHeroes )
                    {
                        GameObject feedbackIcon = Instantiate( swordFeedback, activeDices[ i ].transform.position, Quaternion.identity ) as GameObject;
                        RectTransform CanvasRect = GameManager.GetBaseObject( "Canvas" ).GetComponent<RectTransform>();
                        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint( feedbackIcon.transform.position );
                        Vector2 WorldObject_ScreenPosition = new Vector2(
                        ( ( ViewportPosition.x * CanvasRect.sizeDelta.x ) - ( CanvasRect.sizeDelta.x * 0.5f ) ),
                        ( ( ViewportPosition.y * CanvasRect.sizeDelta.y ) - ( CanvasRect.sizeDelta.y * 0.5f ) ) );
                        WorldObject_ScreenPosition.x += 1920 * 0.5f;
                        WorldObject_ScreenPosition.y += 1080 * 0.5f;
                    //now you can set the position of the ui element
                    feedbackIcon.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;


                    feedbackIcon.transform.SetParent( GameManager.GetBaseObject( "Canvas" ).transform );


                    feedbackIcon.GetComponent<DiceTravel>().SetTargetPosition( rectTransformPosition);
                }
         
            }

            else
            {
                if (!isHeroes )
                {
                    GameObject feedbackIcon = Instantiate( skullFeedback, activeDices[ i ].transform.position, Quaternion.identity ) as GameObject;
                    RectTransform CanvasRect = GameManager.GetBaseObject( "Canvas" ).GetComponent<RectTransform>();
                    Vector2 ViewportPosition = Camera.main.WorldToViewportPoint( feedbackIcon.transform.position );
                    Vector2 WorldObject_ScreenPosition = new Vector2(
                    ( ( ViewportPosition.x * CanvasRect.sizeDelta.x ) - ( CanvasRect.sizeDelta.x * 0.5f ) ),
                    ( ( ViewportPosition.y * CanvasRect.sizeDelta.y ) - ( CanvasRect.sizeDelta.y * 0.5f ) ) );
                    WorldObject_ScreenPosition.x += 1920 * 0.5f;
                    WorldObject_ScreenPosition.y += 1080 * 0.5f;
                    //now you can set the position of the ui element
                    feedbackIcon.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;


                    feedbackIcon.transform.SetParent( GameManager.GetBaseObject( "Canvas" ).transform );

                    feedbackIcon.GetComponent<DiceTravel>().SetTargetPosition( rectTransformPosition );
                }

            }
        }
    }
}


