using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData")]
public class Wave : ScriptableObject {

   
    public List<Dice.DiceType> diceList = new List<Dice.DiceType>();

}
