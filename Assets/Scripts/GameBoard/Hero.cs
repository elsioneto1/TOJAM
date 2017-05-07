using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour {
    public Text lifeText;
    public int maxHealth = 12;
    public int health = 4;

    public GameObject baloon;
    public GameObject dialogueText;

    public List<string> initiativeList = new List<string>();
    public List<string> rollList = new List<string>();
    public List<string> hitList = new List<string>();
    public List<string> cursingList = new List<string>();
    public List<string> damageList = new List<string>();
    public string deadDialogue;

    public bool isDead;

    public Sprite deadSprite;

    string currentDialogue;

    public GameObject cutPrefab;

    public void SetStarter( bool isActive )
    {
        if ( isActive )
            iTween.MoveTo( gameObject, new Vector3( transform.position.x, transform.position.y + 50 ), 1f );

        else
            iTween.MoveTo( gameObject, new Vector3( transform.position.x, transform.position.y - 50 ), 1f );
    }

    public void DealDamage(int damage)
    {
        if (health - damage > 0 )
            health -= damage;

        else
            health = 0;


        GameObject cut = Instantiate( cutPrefab, Vector3.zero, Quaternion.identity ) as GameObject;
        cut.transform.SetParent( transform );
        cut.transform.position = transform.GetChild( 0 ).GetComponent<RectTransform>().position;
        cut.transform.Rotate( new Vector3( 0, 0, Random.Range( -180, 180 ) ) );

        iTween.PunchRotation( gameObject, new Vector3( 0,0, 15 ), 0.45f );
        lifeText.text = health.ToString();
    }


    public void KillHero()
    {
        transform.GetChild( 0 ).GetComponent<Image>().sprite = deadSprite;
        isDead = true;
    }



    public void CallDialogue(EnumHolder.DialogueType dialogueType)
    {
        switch(dialogueType)
        {
            case EnumHolder.DialogueType.Starter:
                currentDialogue = initiativeList[ Random.Range( 0, initiativeList.Count ) ];
                break;

            case EnumHolder.DialogueType.Roll:
                currentDialogue = rollList[ Random.Range( 0, rollList.Count ) ];
                break;

            case EnumHolder.DialogueType.Hit:
                currentDialogue = hitList[ Random.Range( 0, hitList.Count ) ];
                break;

            case EnumHolder.DialogueType.Curse:
                currentDialogue = cursingList[ Random.Range( 0, cursingList.Count ) ];
                break;

            case EnumHolder.DialogueType.Damage:
                currentDialogue = damageList[ Random.Range( 0, damageList.Count ) ];
                break;


            case EnumHolder.DialogueType.Death:
                currentDialogue = deadDialogue;
                break;
        }

        baloon.GetComponent<Wobbling>().Grow();
        dialogueText.GetComponent<Text>().text = currentDialogue;
    }
}
