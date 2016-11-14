using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

//よく使う変数
public static class Variable
{
    public static int count;              //Integer to store the number of pickups collected so far.
    public static float time;
    public static Utility.PlayState playstate = Utility.PlayState.Start;
}

//全体で使うもの
public class Utility
{
    //ゲームの状態
    public enum PlayState
    {
        Start,//ゲーム開始時
        isPaly,//ゲームプレイ中
        Clear,//ゲームクリア
        Failed//ゲーム失敗
    }

    //テキスト表示///////////////////////////////////////////////////////////////

    //テキストオブジェクト
    private static Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    private static Text timeText;
    private static Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.
    private static Text loseText;
    private static Text gameoverText;

    private static Text textObject;

    public enum TextName
    {
        count,//宝石取得数
        time,//ゲームタイム
        win,//ゲームクリア
        lose,//ゲーム失敗
        gameover//ゲームオーバー
    }

    public static void SetTextObject(Text count,Text time,Text win,Text lose,Text gameover)
    {
        countText = count;
        timeText = time;
        winText = win;
        loseText = lose;
        gameoverText = gameover;
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    public static void SetText(TextName textName,string textMessage)//表示するテキストオブジェクト、表示するメッセージ
    {
        
        switch(textName){

            case TextName.count:
                textObject = countText;
                break;
            case TextName.time:
                textObject = timeText;
                break;
            case TextName.win:
                textObject = winText;
                break;
            case TextName.lose:
                textObject = loseText;
                break;
            case TextName.gameover:
                textObject = gameoverText;
                break;
        }        

        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        textObject.text = textMessage;

        /*
        //Check if we've collected all 12 pickups. If we have...
        if (GameManager.playstate == GameManager.PlayState.Clear)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You win!";
        */
    }

}

public class GameManager : MonoBehaviour {

    /// ゲーム状態管理
    /// テキスト表示
    /// タイム管理
    /// 入力管理

    public float limitTime = 5;//タイムリミット

    //テキストオブジェクト
    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text timeText;
    public Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.
    public Text loseText;
    public Text gameoverText;    

    // Use this for initialization
    void Start () {

        //テキストオブジェクトをセットする
        Utility.SetTextObject(countText,timeText,winText,loseText,gameoverText);

        //Initialize count to zero.
        Variable.count = 0;
        Utility.SetText(Utility.TextName.count,"宝石　" + Variable.count.ToString());
        Variable.time = limitTime;
        Utility.SetText(Utility.TextName.time, "残り時間　" + ((int)Variable.time).ToString());

        //Initialze winText to a blank string since we haven't won yet at beginning.
        Utility.SetText(Utility.TextName.win, "");
        //Utility.SetText(Utility.TextName.lose, "");
        Utility.SetText(Utility.TextName.gameover, "");

        //最初に全オブジェクトを止めとく
        Time.timeScale = 0;

    }
	
	// Update is called once per frame
	void Update () {

        //時間管理
        if(Variable.playstate == Utility.PlayState.isPaly)
        {
            Variable.time -= Time.deltaTime;

            if(Variable.time < 1)
            {
                Variable.time = 0;
                Variable.playstate = Utility.PlayState.Failed;
            }

            Utility.SetText(Utility.TextName.time, ("残り時間　" + ((int)Variable.time).ToString()));//小数点以下を切り上げて表示
        }

        //状態管理
        switch (Variable.playstate)
        {
            case Utility.PlayState.Start:
                StartState();
                break;
            case Utility.PlayState.Clear:
                ClearState();
                break;
            case Utility.PlayState.Failed:
                FailedState();
                break;
        }

        //失敗条件
        
        //クリア条件
        /*
        if (Variable.count >= clearCountNum)
        {
            Variable.playstate = Utility.PlayState.Clear;
        }
        */

    }

    //スタート時の処理
    void StartState()
    {
        Utility.SetText(Utility.TextName.lose, "タップでスタート！");
        if (Input.GetMouseButtonDown(0))
        {
            Variable.playstate = Utility.PlayState.isPaly;
            Time.timeScale = 1;//動かす
            Utility.SetText(Utility.TextName.lose, "");
        }
        //Utility.SetText(Utility.TextName.lose, "スペースキーを押してね！");
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Variable.playstate = Utility.PlayState.isPaly;
        //    Time.timeScale = 1;//動かす
        //    Utility.SetText(Utility.TextName.lose, "");
        //}
    }

    //クリア時の処理
    void ClearState()
    {
        Utility.SetText(Utility.TextName.win, "You Win!");
        Invoke("Reset",3.5f);
    }

    //失敗時の処理
    void FailedState()
    {
        Utility.SetText(Utility.TextName.lose, "You lose");
        Invoke("Reset", 3.5f);
    }

    //戻す
    void Reset()
    {
        SceneManager.LoadScene("Title");
        Variable.playstate = Utility.PlayState.Start;
    }

}
