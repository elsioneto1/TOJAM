using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public EnumHolder.DiceType myDiceType;
    public EnumHolder.HeroType myHeroType;

    public void SetColor(EnumHolder.HeroType heroType)
    {
        myHeroType = heroType;

        switch ( myHeroType )
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
