using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    private int maxHealth = 100;
    private int health = 100;
    public GameObject bossCard;
    public Text bossText;
    public ParticleSystem particle;
    public int damageTakenPerHit = 10;

	void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int numberOfHits)
    {
        int totalDamage = numberOfHits * damageTakenPerHit;
        if ( health - totalDamage > 0 )
            health -= totalDamage;

        else
            health = 0;

        iTween.PunchScale( bossCard, new Vector3( 0.5f,0.5f ), 1f );

        bossText.text = health.ToString();

       
        particle.Play();
        Debug.Log( "Boss has taken " + totalDamage + " damage and still has " + health + "health");
    }
}
