using UnityEngine;
using System.Collections;

public class Linear : MonoBehaviour {

    public float time = 1;//動き終わるまでの予定時間
    public Vector3 offset = new Vector3(1,0,0);//移動させる方向

    private Vector3 endPosition;//到達地点
    private Vector3 startPosition;//現在地点

    private float elapsedTime = 0;//経過時間
    private bool isPositive = true;//正方向に動いてる

    void Start()
    {
        startPosition = transform.position;//スタートは今いる位置

        endPosition = transform.position + offset;//今いる位置からoffsetの方向に動く
    }

    void Update()
    {

        elapsedTime += Time.deltaTime;//動きが変化してからの経過時間

        //動き終わる予定時間に経過時間が達したら
        if (elapsedTime > time)
        {
            transform.position = endPosition;//強制的に到達地点に
            Reverse();//動き終わったら反転させる
        }

        var rate = elapsedTime / time;

        //Lerp( 開始点 , 終了点 , 0～1の比率)の位置に動かす
        transform.position = Vector3.Lerp(startPosition, endPosition, rate);

    }

    //移動方向を反転
    void Reverse()
    {
        elapsedTime = 0;//経過時間を戻す
        startPosition = transform.position;//スタートは今いる位置

        //移動方向を逆転
        if (isPositive)
        {
            endPosition = startPosition - offset;
        }
        else
        {
            endPosition = startPosition + offset;
        }

        isPositive = !isPositive;//フラグを反転
    }

}
