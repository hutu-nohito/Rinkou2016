using UnityEngine;
using System.Collections;
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

/******************************************************************************/
/** @brief システム全般(あまり大きくないので一括)
* @date 2016/11/14
* @author 石川
* @param[in] m_fringe 干渉縞の計算結果を格納
* @param[in]
*/
/******************************************************************************/
/* 更新履歴
*
*/
/******************************************************************************/

//ゲーム内でよく使う変数
public static class Variable
{
    public static int count;
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
        isPause,//ポーズ中
        Clear,//ゲームクリア
        Failed//ゲーム失敗
    }

    public static bool isCity = true;//パワーアップパートかどうか

}

public class GameManager : MonoBehaviour {

    /// ゲーム状態管理
    /// テキスト表示
    /// タイム管理
    /// 入力管理

    [SerializeField]
    private int jewel_rate = 10;//宝石のスコア

    //テキストオブジェクト
    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text timeText;
    public Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.
    public Text loseText;
    public Text gameoverText;
    //背景を暗くして文字を見やすくするためのもの(後で何とか)
    public GameObject TextBackGround;
    //戻る用のボタン(あとで何とかする)
    public GameObject LeftButton, RightButton;

    private GameObject Player;
    private GameObject Save;
    private SceneTransition ST;
    Sound_Controller SC;

    // Use this for initialization
    void Start () {

        Save = GameObject.FindGameObjectWithTag("Save");
        ST = Save.GetComponent<SceneTransition>();
        SC = GameObject.FindGameObjectWithTag("Save").GetComponent<Sound_Controller>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = PlayerPara.start_position;

        //テキストオブジェクトをセットする
        TextUtility.SetTextObject(countText, timeText, winText, loseText, gameoverText);

        //Initialize count to zero.
        Variable.count = 0;
        TextUtility.SetText(TextUtility.TextName.count, "宝石　" + Variable.count.ToString());
        Variable.time = CommonValue.limit_time;
        TextUtility.SetText(TextUtility.TextName.time, "残り時間　" + ((int)Variable.time).ToString());

        //Initialze winText to a blank string since we haven't won yet at beginning.
        TextUtility.SetText(TextUtility.TextName.win, "");
        //TextUtility.SetText(TextUtility.TextName.lose, "");
        TextUtility.SetText(TextUtility.TextName.gameover, "");

        //最初に全オブジェクトを止めとく
        Time.timeScale = 0;

    }
	
	// Update is called once per frame
	void Update () {

        //入力管理
        if (Input.GetKeyDown(KeyCode.Escape))//戻るボタン
        {

        }
        if (Input.GetKeyDown(KeyCode.Menu))//メニューボタン
        {

        }

        //時間管理
        if (Variable.playstate == Utility.PlayState.isPaly)
        {
            Variable.time -= Time.deltaTime;

            if(Variable.time < 1)
            {
                Variable.time = 0;

                if (Utility.isCity)
                {
                    StartCoroutine(EndCity());
                }
                else
                {
                    Variable.playstate = Utility.PlayState.Failed;
                }

            }

            TextUtility.SetText(TextUtility.TextName.time, ("残り時間　" + ((int)Variable.time).ToString()));//小数点以下を切り上げて表示
        }

        //状態管理
        switch (Variable.playstate)
        {
            case Utility.PlayState.Start:
                StartState();
                break;
            case Utility.PlayState.isPause:
                PauseState();
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
            Variable.playstate = TextUtility.PlayState.Clear;
        }
        */

    }

    //スタート時の処理
    void StartState()
    {

        if (!Utility.isCity)
        {
            TextUtility.SetText(TextUtility.TextName.win, "ゴールを目指せ！");
        }
        TextBackGround.SetActive(true);
        TextUtility.SetText(TextUtility.TextName.lose, "タップでスタート！");
        if (Input.GetMouseButtonDown(0))
        {
            TextBackGround.SetActive(false);
            Variable.playstate = Utility.PlayState.isPaly;
            Time.timeScale = 1;//動かす
            TextUtility.SetText(TextUtility.TextName.lose, "");
            TextUtility.SetText(TextUtility.TextName.win, "");
        }
    }

    void PauseState()
    {
        //プレイヤーを止める
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    //クリア時の処理
    void ClearState()
    {
        //プレイヤーを止める
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        //リザルト表示
        TextBackGround.SetActive(true);
        string result_text = "クリア!\n" + "スコア " + (int)(Variable.count * jewel_rate + Variable.time);
        TextUtility.SetText(TextUtility.TextName.win, result_text);

        //ボタン表示
        RightButton.SetActive(true);
        LeftButton.SetActive(true);

    }

    //失敗時の処理
    void FailedState()
    {
        //プレイヤーを止める
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        TextBackGround.SetActive(true);
        TextUtility.SetText(TextUtility.TextName.lose, "しっぱい・・・");

        //ボタン表示
        RightButton.SetActive(true);
        LeftButton.SetActive(true);

    }

    IEnumerator EndCity()
    {
        //暗くしてたら戻る
        if (TextBackGround.activeInHierarchy) { yield break; }

        Variable.playstate = Utility.PlayState.isPause;

        TextBackGround.SetActive(true);
        TextUtility.SetText(TextUtility.TextName.win, "timeup!");

        //ここでランダムでコロシアムを選ぶ
        int colosseumnum = Random.Range(2,5);
        PlayerPara.start_position = new Vector2(0, colosseumnum * 100);

        //コロシアムを管理するとこに送って場所を指定
        CommonValue.limit_time = 300;

        //パラメタ受け渡し(あとで何とか)
        Machine_Parameter MP = Save.GetComponent<Machine_Parameter>();
        Player P = Player.GetComponent<Player>();
        MP.acceleration = P.acceleration;
        MP.limmit_speed = P.limmit_speed;
        MP.mass = P.mass;
        MP.power = P.power;
        MP.friction = P.friction;

        yield return new WaitForSeconds(3);//タイムアップを見せる

        ST.SceneSet("Main");

        yield return new WaitForSeconds(ST.Fade_time);//とりあえずフェードタイムに合わせる

        Variable.playstate = Utility.PlayState.isPaly;
        Utility.isCity = false;
        TextBackGround.SetActive(false);
    }

    //戻す
    public void Reset()
    {
        Machine_Parameter MP = Save.GetComponent<Machine_Parameter>();
        MP.acceleration = 5;
        MP.limmit_speed = 25;
        MP.mass = 1;
        MP.power = 1;
        MP.friction = 2;
        CommonValue.limit_time = 30;
        Utility.isCity = true;

        SC.ButtonSE();
        ST.SceneSet("Home");
    }

    //リトライ
    public void Retry()
    {
        SC.ButtonSE();
        ST.SceneSet("Main");
    }

}
