using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {


    #region hand
    public enum HandType
    {
        Mage,
        Warrior,
        Ranger,
    }

    public HandType currentHand;
    public GameObject handPrefab;

    private List<HandType> currentPlayerOrder = new List<HandType>();

    private int currentHero = 0;
    #endregion

    #region diceSettings
    public GameObject d6Prefab;
    public GameObject d8Prefab;
    public GameObject d12Prefab;

    int timeInBetweenRolls;
    #endregion

    public List<Wave> waveObjectList = new List<Wave>();
    private int currentWave;


    void Start()
    {
        currentPlayerOrder.Add(HandType.Mage);
        currentPlayerOrder.Add(HandType.Warrior);
        currentPlayerOrder.Add(HandType.Ranger);

        CreateRound();

    }

    void CreateRound()
    {
        Shuffle(currentPlayerOrder);

        StartCoroutine("CreateHand");
    }

    IEnumerator CreateHand()
    {
        while(currentHero < 3)
        { 
            yield return new WaitForSeconds(timeInBetweenRolls);
            GameObject hand = Instantiate(handPrefab,transform.position, Quaternion.identity) as GameObject;
            hand.GetComponent<Hand>().SetHand(currentPlayerOrder[currentHero]);
            currentHero++;

        }

        currentHero = 0;
    }

    void CreateRoll()
    {
        List<Dice.DiceType> tempDiceList = waveObjectList[currentWave].diceList; 
        for (int i = 0; i < tempDiceList.Count; i++)
        {
            switch (tempDiceList[i])
            {
                case Dice.DiceType.D6:
                    Instantiate(d6Prefab, transform.position, Quaternion.identity);
                    break;

                case Dice.DiceType.D8:
                    Instantiate(d8Prefab, transform.position, Quaternion.identity);
                    break;

                case Dice.DiceType.D12:
                    Instantiate(d12Prefab, transform.position, Quaternion.identity);
                    break;
            }
        }
        currentWave++;
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


