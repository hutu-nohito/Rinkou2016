using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

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
    public static int start_number = 1;

}

public class SaveLoad : MonoBehaviour {

    //使いまわすパラメタ//
    //資料：StreamWriter クラス (System.IO)
    //http://msdn.microsoft.com/ja-jp/library/system.io.streamwriter(v=vs.110).aspx

    //セーブ&ロード

    public string folder;    //保存先のパス
    private string[] SaveData = new string[1];//保存したい一個一個の要素
                                              //public TextAsset _Text;//保存用テキストファイル
    private static bool flag_exist = false;
    void Awake()
    {
        //このObjectはずっと残るので、とりあえずここで初期化しとく。順番は固定。
        SaveData[0] = "";

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

    void Start()
    {
        folder = Application.persistentDataPath;


        string deviceLanguage = Application.systemLanguage.ToString();
        if (deviceLanguage == "Japanese")
        {
            ConfigPara.language = ConfigPara.Language.Japanese;
        }
        else
        {
            ConfigPara.language = ConfigPara.Language.English;
        }

    }
    
    void Update()
    {
        //debug
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Save();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}

    }

    public void Save()
    {
        //セーブを呼び出した段階でのパラメタを格納
        SaveData[0] = Variable.Score.ToString();//ハイスコア

        SaveText(folder, @"\savedata.txt", SaveData);
        Debug.Log(folder);
    }

    public void Load()
    {
        SaveData = LoadText(folder, @"\savedata.txt");

        //SetDay(float.Parse(SaveData[0]));
        Variable.Score = Int32.Parse(SaveData[0]);
        
    }

    //テキストファイルとしてセーブ,上書き
    //ファイルを作るパス、作るファイルの名前、書きこむ文字列
    public void SaveText(string fileFolder, string filename, string[] dataStr)
    {
        using (StreamWriter w = new StreamWriter(fileFolder + filename))//たぶんパスを作ってそこに書き込んでる、あったら作らずに書き込む
        {
            foreach (var item in dataStr)//foreachはitemにいったんdataStrを格納してから処理をしてる気がする
            {
                w.WriteLine(item);
            }
        }
    }

    //ローダー
    public string[] LoadText(string fileFolder, string filename)
    {
        List<string> strList = new List<string>();
        string line = "";
        using (StreamReader sr = new StreamReader(fileFolder + filename))
        {
            while ((line = sr.ReadLine()) != null)
            {
                strList.Add(line);
            }
        }
        return strList.ToArray();
    }
}
