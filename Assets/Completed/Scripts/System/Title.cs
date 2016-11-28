using UnityEngine;
using System.Collections;


public class Title : MonoBehaviour {

    SceneTransition ST;
    Sound_Controller SC;

    // Use this for initialization
    void Start () {

        ST = GameObject.FindGameObjectWithTag("Save").GetComponent<SceneTransition>();
        SC = GameObject.FindGameObjectWithTag("Save").GetComponent<Sound_Controller>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Button_Start()
    {
        SC.ButtonSE();
        ST.SceneSet("Home");
    }
}
