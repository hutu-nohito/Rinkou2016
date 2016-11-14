using UnityEngine;
using System.Collections;

/******************************************************************************/
/** @brief セーブロードとシーン間での変数の受け渡し
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

//プレイヤーがよく使う変数
public static class PlayerPara
{
    public static float player_speed = 10;
}

//設定用変数
public static class ConfigPara
{
    public static int BGM_vol = 1;
}

public class SaveLoad : MonoBehaviour {   

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
