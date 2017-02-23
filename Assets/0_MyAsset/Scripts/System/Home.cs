using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour {

    SceneTransition ST;
    Sound_Controller SC;

    [SerializeField]
    private Text HiScore;

    [SerializeField]
    private GameObject Lesson;

    // Use this for initialization
    void Start()
    {

        ST = GameObject.FindGameObjectWithTag("Save").GetComponent<SceneTransition>();
        SC = GameObject.FindGameObjectWithTag("Save").GetComponent<Sound_Controller>();
        HiScore.text = Variable.Score.ToString();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Stage1()
    {
        if(ConfigPara.start_number == 1)//起動一回目だからチュートリアルを見せる
        {
            Lesson.SetActive(true);
        }
        else
        {
            PlayerPara.start_position = new Vector2(0, 0);
            SC.ButtonSE();
            ST.SceneSet("Main");
        }
        
    }

    public void Lesson_Button()
    {
        Lesson.SetActive(false);
        PlayerPara.start_position = new Vector2(0, 0);
        SC.ButtonSE();
        ST.SceneSet("Main");
        ConfigPara.start_number++;
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
