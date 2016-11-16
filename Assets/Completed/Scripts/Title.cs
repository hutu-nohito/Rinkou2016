using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Button_Start()
    {
        SceneManager.LoadScene("Main");
    }
}
