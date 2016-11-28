using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

    SceneTransition ST;
    Sound_Controller SC;

    // Use this for initialization
    void Start()
    {

        ST = GameObject.FindGameObjectWithTag("Save").GetComponent<SceneTransition>();
        SC = GameObject.FindGameObjectWithTag("Save").GetComponent<Sound_Controller>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Stage1()
    {
        PlayerPara.start_position = new Vector2(0,0);
        SC.ButtonSE();
        ST.SceneSet("Main");
    }

    public void Stage2()
    {
        PlayerPara.start_position = new Vector2(0, 100);
        ST.SceneSet("Main");
    }

    public void Stage3()
    {
        PlayerPara.start_position = new Vector2(5, 300);
        ST.SceneSet("Main");
    }
}
