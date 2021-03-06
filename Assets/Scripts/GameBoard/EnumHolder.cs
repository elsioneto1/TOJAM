﻿using UnityEngine;
using System.Collections;

public class EnumHolder : MonoBehaviour {

    public enum HeroType
    {
        Mage,
        Warrior,
        Ranger,
    }
    public enum DiceType
    {
        D6,
        D8,
        D10
    }

    public enum GameState
    {
        None,
        Initiating,
        Handing,
        Rolling,
        Combat,
        GameOver
    }

    public enum DialogueType
    {
        Starter,
        Hit,
        Roll,
        Curse,
        Damage,
        Death
    }
}
