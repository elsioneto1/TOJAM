using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    Board.HandType myHandType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetHand(Board.HandType handType)
    {
        myHandType = handType;

        switch(myHandType)
        {
            case Board.HandType.Mage:
                break;

            case Board.HandType.Ranger:
                break;

            case Board.HandType.Warrior:
                break;
        }
    }
}
