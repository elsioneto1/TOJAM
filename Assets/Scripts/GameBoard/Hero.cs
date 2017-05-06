using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour {
    public Text lifeText;
    public int maxHealth = 12;
    public int health = 4;

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

        lifeText.text = health.ToString();
    }
}
