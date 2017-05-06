using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    EnumHolder.HeroType myHandType;


    public void SetHand(EnumHolder.HeroType handType)
    {
        myHandType = handType;

        switch(myHandType)
        {
            case EnumHolder.HeroType.Mage:
                break;

            case EnumHolder.HeroType.Ranger:
                break;

            case EnumHolder.HeroType.Warrior:
                break;
        }
    }
}
