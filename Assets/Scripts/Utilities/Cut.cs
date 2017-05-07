using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour {

    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if ( this.animator.GetCurrentAnimatorStateInfo( 0 ).IsName("Destroy"))
        {
            Destroy( gameObject );
        }

    }


}
