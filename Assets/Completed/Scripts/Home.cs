using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Stage1()
    {
        PlayerPara.start_position = new Vector2(0,0);
        SceneManager.LoadScene("Main");
    }

    public void Stage2()
    {
        PlayerPara.start_position = new Vector2(0, 100);
        SceneManager.LoadScene("Main");
    }
}
