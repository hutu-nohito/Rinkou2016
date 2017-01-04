using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

    /******************************************************************************/
    /** @brief Cityの管理
    * @date 2016/11/14
    * @author 石川
    * @param[in] m_fringe 干渉縞の計算結果を格納
    * @param[in]
    */
    /******************************************************************************/
    /* 更新履歴
    *   アイテムを降らせる
    *   イベント管理
    */
    /******************************************************************************/

    [SerializeField]
    private GameObject[] PowerupItems;

    // Use this for initialization
    void Start () {

        StartCoroutine(ItemDrop());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ItemDrop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int num = Random.Range(0,3);
            Vector2 pos = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            Instantiate(PowerupItems[num],pos, Quaternion.identity);
        }
        
    }
}
