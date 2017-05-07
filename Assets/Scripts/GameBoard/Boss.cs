using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    private int maxHealth = 20;
    public int health = 20;
    public GameObject bossCard;
    public GameObject textObject;
    public Text bossText;
    public ParticleSystem particle;
    public int damageTakenPerHit = 10;
    public GameObject cutPrefab;

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

        iTween.PunchScale( bossCard, new Vector3( 0.5f,0.5f ), 0.45f );

        //consertar
      //  iTween.PunchScale( textObject, new Vector3( 0.5f, 0.5f ), 0.45f );

        bossText.text = health.ToString();

        GameObject cut = Instantiate( cutPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        cut.transform.SetParent( transform );
        cut.transform.position = transform.GetChild( 0 ).GetComponent<RectTransform>().position;
        cut.transform.Rotate( new Vector3( 0, 0, Random.Range( -180, 180 ) ) );

        particle.Play();
        Debug.Log( "Boss has taken " + totalDamage + " damage and still has " + health + "health");
    }
}
