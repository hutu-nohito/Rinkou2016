using UnityEngine;
using System.Collections;


public class Title : MonoBehaviour {

    SceneTransition ST;
    SaveLoad SL;
    Sound_Controller SC;

    // Use this for initialization
    void Start () {

        ST = GameObject.FindGameObjectWithTag("Save").GetComponent<SceneTransition>();
        SL = GameObject.FindGameObjectWithTag("Save").GetComponent<SaveLoad>();
        SC = GameObject.FindGameObjectWithTag("Save").GetComponent<Sound_Controller>();

        //if(ConfigPara.start_number != 0)
        //{
            
        //}
        //else
        //{
        //    SL.Save();//一回目の起動時にセーブファイルを作成
        //}

        SL.Load();//タイトルでロード

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Button_Start()
    {
        SC.ButtonSE();
        ST.SceneSet("Home");
        SL.Load();//タイトルでロード
    }

    public void Button_Language()
    {
        if(ConfigPara.language == ConfigPara.Language.Japanese)
        {
            ConfigPara.language = ConfigPara.Language.English;
        }
        else
        {
            ConfigPara.language = ConfigPara.Language.Japanese;
        }

        ST.SceneSet("Title");
        
    }
}
