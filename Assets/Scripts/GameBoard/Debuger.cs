using UnityEngine;
using System.Collections;

public class Debuger : MonoBehaviour {

    private TextMesh tmComponent;

    void Awake()
    {
        tmComponent = GetComponent<TextMesh>();
    }

	
	// Update is called once per frame
	void Update () {
        tmComponent.text = GameManager.currentGameState.ToString();
	}
}
