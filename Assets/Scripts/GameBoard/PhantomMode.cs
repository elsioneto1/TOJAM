using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMode : MonoBehaviour {

    public static PhantomMode instance;

    public float countdownTimer;
    public SpriteRenderer spriteRenderer;

    public TextMesh tMesh;

    bool started = false;

    public Color c1;
    public Color c2;

	// Use this for initialization
	void Start () {

        instance = this;
        tMesh = GetComponent<TextMesh>();

	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            tMesh.text = ((int) countdownTimer).ToString();
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0)
            {
                started = false;
                StartCoroutine(FinishPhantomMode());
                GetComponent<MeshRenderer>().enabled = false;

            }
        }
	}


    public void StartPhantom(float timer)
    {
        StartCoroutine(StartPhantomMode());

        GetComponent<MeshRenderer>().enabled = true;
        countdownTimer = timer;
        started = true;
    }
    

    IEnumerator StartPhantomMode()
    {
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime * 2;
            spriteRenderer.color = Color.Lerp(c1,c2,elapsedTime);
        }
    }

    IEnumerator FinishPhantomMode()
    {
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime * 2;
            spriteRenderer.color = Color.Lerp(c2, c1, elapsedTime);
        }
    }

}
