﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
        TextUtility.SetTextObject(countText, timeText, winText, loseText, gameoverText);

        //Initialize count to zero.
        Variable.count = 0;
        TextUtility.SetText(TextUtility.TextName.count, "宝石　" + Variable.count.ToString());
        Variable.time = limitTime;
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

        //時間管理
        if(Variable.playstate == Utility.PlayState.isPaly)
        {
            Variable.time -= Time.deltaTime;

            if(Variable.time < 1)
            {
                Variable.time = 0;
                Variable.playstate = Utility.PlayState.Failed;
            }

            TextUtility.SetText(TextUtility.TextName.time, ("残り時間　" + ((int)Variable.time).ToString()));//小数点以下を切り上げて表示
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
            Variable.playstate = TextUtility.PlayState.Clear;
        }
        */

    }

    //スタート時の処理
    void StartState()
    {
        TextUtility.SetText(TextUtility.TextName.lose, "タップでスタート！");
        if (Input.GetMouseButtonDown(0))
        {
            Variable.playstate = Utility.PlayState.isPaly;
            Time.timeScale = 1;//動かす
            TextUtility.SetText(TextUtility.TextName.lose, "");
        }
        //TextUtility.SetText(TextUtility.TextName.lose, "スペースキーを押してね！");
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Variable.playstate = TextUtility.PlayState.isPaly;
        //    Time.timeScale = 1;//動かす
        //    TextUtility.SetText(TextUtility.TextName.lose, "");
        //}
    }

    //クリア時の処理
    void ClearState()
    {
        TextUtility.SetText(TextUtility.TextName.win, "You Win!");
        Invoke("Reset",3.5f);
    }

    //失敗時の処理
    void FailedState()
    {
        TextUtility.SetText(TextUtility.TextName.lose, "You lose");
        Invoke("Reset", 3.5f);
    }

    //戻す
    void Reset()
    {
        SceneManager.LoadScene("Title");
        Variable.playstate = Utility.PlayState.Start;
    }

}