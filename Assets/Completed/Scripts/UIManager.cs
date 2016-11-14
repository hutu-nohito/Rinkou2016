using UnityEngine;
using System.Collections;
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

/******************************************************************************/
/** @brief テキスト関連
* @date 2016/11/14
* @author 石川
* @param[in] m_fringe 干渉縞の計算結果を格納
* @param[in]
*/
/******************************************************************************/
/* 更新履歴
*   テキスト表示自体は好きなタイミングで出来るようにしときたい
*/
/******************************************************************************/


public class TextUtility
{
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

    public static void SetTextObject(Text count, Text time, Text win, Text lose, Text gameover)
    {
        countText = count;
        timeText = time;
        winText = win;
        loseText = lose;
        gameoverText = gameover;
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    public static void SetText(TextName textName, string textMessage)//表示するテキストオブジェクト、表示するメッセージ
    {

        switch (textName)
        {

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

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
