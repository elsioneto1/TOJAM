using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAsset : MonoBehaviour {

    Animator anim;
    public static HandAsset instance;
    public bool setColliderToTrigger;
    public Collider col;
    public Vector3[] forces;
    List<int> sortedIndexes = new List<int>();
    public SpawnPointDice[] spawnPoints;
    // Use this for initialization
    void Start () {
        instance = this;
        anim = GetComponent<Animator>();
        for (int i = 0; i < 6; i++)
        {
            sortedIndexes.Add(i);    
        }
        spawnPoints = GetComponentsInChildren<SpawnPointDice>();
	}
	
	// Update is called once per frame
	void Update () {
        col.enabled = setColliderToTrigger;
	}


    public void ThrowDices()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {

            if (spawnPoints[i].holdingDice != null)
            {
                int random = Random.Range(0, sortedIndexes.Count);
                sortedIndexes.RemoveAt(random);


                spawnPoints[i].holdingDice.rBody.useGravity = true;
                spawnPoints[i].holdingDice.rBody.isKinematic = false;

                spawnPoints[i].holdingDice.rBody.AddForce(forces[random]);
                spawnPoints[i].holdingDice.rBody.AddRelativeTorque(forces[random], ForceMode.Impulse);
                Debug.Log(forces[random]);
                spawnPoints[i].holdingDice = null;
            }
        }

        sortedIndexes.Clear();
        for (int i = 0; i < 6; i++)
        {
            sortedIndexes.Add(i);
        }
    }

    public void SortDicesOnHand(List<Dice> dices)
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].rBody.useGravity = false;
            dices[i].rBody.isKinematic = true;

            int random = Random.Range(0, sortedIndexes.Count);
            sortedIndexes.RemoveAt(random);
            spawnPoints[random].holdingDice =  dices[i];

        }

        anim.SetTrigger("Throw");

        sortedIndexes.Clear();
        for (int i = 0; i < 6; i++)
        {
            sortedIndexes.Add(i);
        }
    }

}
