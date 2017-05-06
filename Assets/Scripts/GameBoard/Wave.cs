using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData")]
public class Wave : ScriptableObject {

    public int waveTime;
    public List<EnumHolder.DiceType> diceList = new List<EnumHolder.DiceType>();

}
