using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnPaper : MonoBehaviour {

    public Text paperText;

	public void SetText(bool isExiting)
    {
        if(isExiting)
         iTween.MoveTo( gameObject, new Vector3( transform.position.x,transform.position.y - 150 ), 1f );

        else
        {
            iTween.MoveTo( gameObject, new Vector3( transform.position.x, transform.position.y + 150 ), 1f );

            switch (GameManager.currentGameState)
            {
                case EnumHolder.GameState.Initiating:
                    paperText.text = "Rolling Initiative";
                    break;

                case EnumHolder.GameState.Handing:
                    paperText.text = "Throwing";
                    break;

                case EnumHolder.GameState.Rolling:
                    paperText.text = "Rolling Dice";
                    break;

                case EnumHolder.GameState.Combat:
                    paperText.text = "Damage Resolution";
                    break;
                //case EnumHolder.GameState.DamageBoss:
                //    paperText.text = "The Hero Attacks";
                //    break;

                //case EnumHolder.GameState.DamageHeroes:
                //    paperText.text = "The GOAT Attacks";
                //    break;


                case EnumHolder.GameState.GameOver:
                    paperText.text = "Gamve Over";
                    break;


            }

        }
            
    }
}
