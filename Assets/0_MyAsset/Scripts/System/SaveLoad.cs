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
*   設定はこれに保持
*/
/******************************************************************************/

//設定用変数
public static class ConfigPara
{
    public static int BGM_vol = 1;
    public enum Language
    {
        Japanese,
        English
    }
    public static Language language = Language.English;

}

public class SaveLoad : MonoBehaviour {

    private static bool flag_exist = false;
    void Awake()
    {

        if (!flag_exist)
        {

            DontDestroyOnLoad(this.gameObject);
            flag_exist = true;

        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
