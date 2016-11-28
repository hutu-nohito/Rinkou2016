using UnityEngine;
using System.Collections;


public class Title : MonoBehaviour {

    SceneTransition ST;

	// Use this for initialization
	void Start () {

        ST = GameObject.FindGameObjectWithTag("Save").GetComponent<SceneTransition>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Button_Start()
    {
        ST.SceneSet("Home");
    }
}
