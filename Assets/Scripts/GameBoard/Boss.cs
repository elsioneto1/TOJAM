using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    private int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DealDamage(int damageAmount)
    {
        if ( health - damageAmount > 0 )
            health -= damageAmount;

        else
            health = 0;

        Debug.Log( "Boss has taken " + damageAmount + " damage and still has " + health + "health");
    }
}
